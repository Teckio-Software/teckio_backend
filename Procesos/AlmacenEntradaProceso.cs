using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class AlmacenEntradaProceso<T> where T : DbContext
    {
            private readonly IAlmacenEntradaService<T> _almacenEntradaService;
            private readonly IInsumoXAlmacenEntradaService<T> _insumoXAlmacenEntradaService;
            private readonly IOrdenCompraService<T> _ordenCompraService;
            private readonly IInsumoXOrdenCompraService<T> _insumoXOrdenCompraService;
            private readonly OrdenCompraProceso<T> _ordenCompraProceso;
            private readonly ActualizaEstatusSubProceso<T> _actualizaRequsicionEstatusInsumos;
            private readonly IAlmacenService<T> _almacenService;
            private readonly IContratistaService<T> _contratistaService;
            private readonly IInsumoService<T> _insumoService;
            private readonly IAlmacenExistenciaInsumoService<T> _almacenExistenciaInsumoService;
            private readonly IInsumoXAlmacenSalidaService<T> _insumoXAlmacenSalidaService;
            private readonly IAlmacenSalidaService<T> _almacenSalidaService;
            
            public AlmacenEntradaProceso(
                IAlmacenEntradaService<T> almacenEntradaService,
                IInsumoXAlmacenEntradaService<T> insumoXAlmacenEntradaService,
                IOrdenCompraService<T> ordenCompraService,
                IInsumoXOrdenCompraService<T> insumoXOrdenCompraService,
                OrdenCompraProceso<T> ordenCompraProceso,
                ActualizaEstatusSubProceso<T> actualizaRequsicionEstatusInsumos,
                IAlmacenService<T> almacenService,
                IContratistaService<T> contratistaService,
                IInsumoService<T> insumoService,
                IAlmacenExistenciaInsumoService<T> almacenExistenciaInsumoService,
                IInsumoXAlmacenSalidaService<T> insumoXAlmacenSalidaService,
                IAlmacenSalidaService<T> almacenSalidaService
                )
            {
                _almacenEntradaService = almacenEntradaService;
                _insumoXAlmacenEntradaService = insumoXAlmacenEntradaService;
                _ordenCompraService = ordenCompraService;
                _insumoXOrdenCompraService = insumoXOrdenCompraService;
                _ordenCompraProceso = ordenCompraProceso;
                _actualizaRequsicionEstatusInsumos = actualizaRequsicionEstatusInsumos;
                _almacenService = almacenService;
                _contratistaService = contratistaService;
                _insumoService = insumoService;
                _almacenExistenciaInsumoService = almacenExistenciaInsumoService;
                _insumoXAlmacenSalidaService = insumoXAlmacenSalidaService;
                _almacenSalidaService = almacenSalidaService;
            }
        public async Task<RespuestaDTO> CrearAlmacenEntrada(AlmacenEntradaCreacionDTO parametros, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var usuarioNombre = claims.Where(z => z.Type == "username").ToList();

            AlmacenEntradaDTO almacenEntradaDTO = new AlmacenEntradaDTO();
            almacenEntradaDTO.IdAlmacen = parametros.IdAlmacen;
            almacenEntradaDTO.IdContratista = 0;
            string NumeroAlmacenEntrada = "";
            var almacen = await _almacenService.ObtenXId(parametros.IdAlmacen);
            var almacenentrada = await ObtenXIdProyecto(Convert.ToInt32(almacen.IdProyecto));

            if (almacenentrada.Count() <= 0)
            {
                NumeroAlmacenEntrada = "EM_1";
            }
            else
            {
                NumeroAlmacenEntrada = "EM_" + (almacenentrada.Count() + 1).ToString();
            }
            almacenEntradaDTO.NoEntrada = NumeroAlmacenEntrada;
            almacenEntradaDTO.FechaRegistro = DateTime.Now;
            almacenEntradaDTO.PersonaRegistra = usuarioNombre[0].Value;
            almacenEntradaDTO.Observaciones = "";
            almacenEntradaDTO.Estatus = 1;

            var almacenEntradaCreacion = await _almacenEntradaService.CrearYObtener(almacenEntradaDTO);
            if (almacenEntradaCreacion.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creó la entrdada de material";
                return respuesta;
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se creó la entrdada de material";
            return respuesta;
        }

        public async Task<RespuestaDTO> EditarAlmacenEntrada(AlmacenEntradaDTO parametros) {
            RespuestaDTO respuesta = new RespuestaDTO();

            if (parametros.Estatus == 1) {
                var Almacen = await _almacenService.ObtenXId(parametros.IdAlmacen);
                var insumosXRecibir = await _ordenCompraProceso.InsumosXOrdenCompraXIdContratista(Convert.ToInt32(parametros.IdContratista), Convert.ToInt32(Almacen.IdProyecto));
                if (insumosXRecibir.Count() <= 0) {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No hay materiales por recibir de este Proveedor";
                    return respuesta;
                }
            }

            var editarEntrada = await _almacenEntradaService.Editar(parametros);
            if (!editarEntrada.Estatus) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se actualizó la entrada";
                return respuesta;
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se creó la entrdada de material";
            return respuesta;
        }

        public async Task<RespuestaDTO> CrearInsumoEntradaAlmacen(AlmacenEntradaInsumoCreacionDTO insumoEntradaAlmacen) {
            RespuestaDTO respuesta = new RespuestaDTO();
            var AlmacenEntrada = await _almacenEntradaService.ObtenXId(insumoEntradaAlmacen.IdAlmacenEntrada);
            var almacen = await _almacenService.ObtenXId(AlmacenEntrada.IdAlmacen);
            var ordencompra = await _ordenCompraService.ObtenXId(insumoEntradaAlmacen.IdOrdenCompra);

            var InsumoEntradaAlmacen = new AlmacenEntradaInsumoDTO();
            InsumoEntradaAlmacen.IdProyecto = Convert.ToInt32(almacen.IdProyecto);
            InsumoEntradaAlmacen.IdRequisicion = ordencompra.IdRequisicion;
            InsumoEntradaAlmacen.IdOrdenCompra = insumoEntradaAlmacen.IdOrdenCompra;
            InsumoEntradaAlmacen.IdAlmacenEntrada = insumoEntradaAlmacen.IdAlmacenEntrada;
            InsumoEntradaAlmacen.IdInsumo = insumoEntradaAlmacen.IdInsumo;
            InsumoEntradaAlmacen.CantidadPorRecibir = insumoEntradaAlmacen.CantidadPorRecibir;
            InsumoEntradaAlmacen.CantidadRecibida = insumoEntradaAlmacen.CantidadRecibIda;
            InsumoEntradaAlmacen.Estatus = 1;

            var existenciaEntrada = new AlmacenExistenciaInsumoCreacionDTO();
            existenciaEntrada.IdInsumo = insumoEntradaAlmacen.IdInsumo;
            existenciaEntrada.IdProyecto = Convert.ToInt32(almacen.IdProyecto);
            existenciaEntrada.IdAlmacen = AlmacenEntrada.IdAlmacen;
            existenciaEntrada.CantidadInsumosAumenta = insumoEntradaAlmacen.CantidadRecibIda;
            existenciaEntrada.CantidadInsumosRetira = 0;
            existenciaEntrada.EsNoConsumible = false;
            existenciaEntrada.FechaRegistro = DateTime.Now;

            var crearInsumoEntrada = await _insumoXAlmacenEntradaService.CrearYObtener(InsumoEntradaAlmacen);
            if (crearInsumoEntrada.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creó el isnumo en entrada de material";
                return respuesta;
            }

            var crearExisteciaEntrada = await _almacenExistenciaInsumoService.CreaExistenciaInsumoEntrada(existenciaEntrada);

            var actualizatIXOC = await _insumoXOrdenCompraService.ActualizarCantidadRecibida(crearInsumoEntrada.IdInsumo, Convert.ToInt32(crearInsumoEntrada.IdOrdenCompra), crearInsumoEntrada.CantidadRecibida);
            await _ordenCompraProceso.ActualizaOrdenCompraSurtidos(Convert.ToInt32(crearInsumoEntrada.IdOrdenCompra));
            await _actualizaRequsicionEstatusInsumos.ActualizaEstatusRequisicionInsumos(Convert.ToInt32(crearInsumoEntrada.IdRequisicion));

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se creó el isnumo en entrada de material";
            return respuesta;
        }

        public async Task<RespuestaDTO> CrearInsumoAjusteAlmacen(AlmacenEntradaInsumoCreacionDTO insumoEntradaAlmacen)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var AlmacenEntrada = await _almacenEntradaService.ObtenXId(insumoEntradaAlmacen.IdAlmacenEntrada);
            var insummoAlmacenentrada = await _insumoXAlmacenEntradaService.ObtenXIdAlmacenEntrada(AlmacenEntrada.Id);
            var almacen = await _almacenService.ObtenXId(AlmacenEntrada.IdAlmacen);
            var insumos = await _insumoService.ObtenXIdProyecto(Convert.ToInt32(almacen.IdProyecto));

            var objetoinsumo = insumos.Where(z => z.Descripcion.ToLower() == insumoEntradaAlmacen.Descripcion.ToLower() && z.Unidad.ToLower() == insumoEntradaAlmacen.Unidad.ToLower()).ToList();
            InsumoDTO crearInsumo = new InsumoDTO();
            if (objetoinsumo.Count <= 0)
            {
                crearInsumo = await _insumoService.CrearYObtener(new InsumoCreacionDTO()
                {
                    Descripcion = insumoEntradaAlmacen.Descripcion,
                    Unidad = insumoEntradaAlmacen.Unidad,
                    IdProyecto = Convert.ToInt32(almacen.IdProyecto),
                    idFamiliaInsumo = null,
                    idTipoInsumo = insumoEntradaAlmacen.IdTipoInsumo,
                    CostoUnitario = 0,
                    Codigo = AlmacenEntrada.NoEntrada.ToString() + "_" + (insummoAlmacenentrada.Count() + 1),
                });
            }
            else
            {
                crearInsumo = objetoinsumo[0];
            }

            var InsumoEntradaAlmacen = new AlmacenEntradaInsumoDTO();
            InsumoEntradaAlmacen.IdProyecto = Convert.ToInt32(almacen.IdProyecto);
            InsumoEntradaAlmacen.IdRequisicion = 0;
            InsumoEntradaAlmacen.IdOrdenCompra = 0;
            InsumoEntradaAlmacen.IdAlmacenEntrada = insumoEntradaAlmacen.IdAlmacenEntrada;
            InsumoEntradaAlmacen.IdInsumo = crearInsumo.id;
            InsumoEntradaAlmacen.CantidadPorRecibir = 0;
            InsumoEntradaAlmacen.CantidadRecibida = insumoEntradaAlmacen.CantidadRecibIda;
            InsumoEntradaAlmacen.Estatus = 1;

            var existenciaEntrada = new AlmacenExistenciaInsumoCreacionDTO();
            existenciaEntrada.IdInsumo = crearInsumo.id;
            existenciaEntrada.IdProyecto = Convert.ToInt32(almacen.IdProyecto);
            existenciaEntrada.IdAlmacen = AlmacenEntrada.IdAlmacen;
            existenciaEntrada.CantidadInsumosAumenta = insumoEntradaAlmacen.CantidadRecibIda;
            existenciaEntrada.CantidadInsumosRetira = 0;
            existenciaEntrada.EsNoConsumible = false;
            existenciaEntrada.FechaRegistro = DateTime.Now;

            var crearInsumoEntrada = await _insumoXAlmacenEntradaService.CrearYObtener(InsumoEntradaAlmacen);
            if (crearInsumoEntrada.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creó el isnumo en ajuste de material";
                return respuesta;
            }
            var crearExisteciaEntrada = await _almacenExistenciaInsumoService.CreaExistenciaInsumoEntrada(existenciaEntrada);

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se creó el isnumo en ajuste de material";
            return respuesta;
        }


            public async Task<RespuestaDTO> CrearAlmacenEntrada1(AlmacenEntradaCreacionDTO parametros, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var usuarioNombre = claims.Where(z => z.Type == "username").ToList();
            if (parametros.IdAlmacen <= 0 || parametros.IdContratista <= 0 || string.IsNullOrEmpty(parametros.Observaciones) ||
                parametros.ListaInsumosEnAlmacenEntrada.Count() <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Procure llenar los compos";
                return respuesta;
            }
            foreach (var item in parametros.ListaInsumosEnAlmacenEntrada)
            {
                if (item.CantidadRecibIda <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Capture las cantidades correctas";
                    return respuesta;
                }
            }

            AlmacenEntradaDTO almacenEntradaDTO = new AlmacenEntradaDTO();
            almacenEntradaDTO.IdAlmacen = parametros.IdAlmacen;
            almacenEntradaDTO.IdContratista = parametros.IdContratista;
            string NumeroAlmacenEntrada = "";
            var almacen = await _almacenService.ObtenXId(parametros.IdAlmacen);
            var almacenentrada = await ObtenXIdProyecto(Convert.ToInt32(almacen.IdProyecto));
             
            if (almacenentrada.Count() <= 0) {
                NumeroAlmacenEntrada = "EM_1";
            }
            else
            {
                NumeroAlmacenEntrada = "EM_" + (almacenentrada.Count() + 1).ToString();
            }
            almacenEntradaDTO.NoEntrada = NumeroAlmacenEntrada;
            almacenEntradaDTO.FechaRegistro = DateTime.Now;
            almacenEntradaDTO.PersonaRegistra = usuarioNombre[0].Value;
            almacenEntradaDTO.Observaciones = parametros.Observaciones;
            almacenEntradaDTO.Estatus = 1;

            var almacenEntradaCreacion = await _almacenEntradaService.CrearYObtener(almacenEntradaDTO);
            if (almacenEntradaCreacion.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creó la entrdada de material";
                return respuesta;
            }

            List<AlmacenEntradaInsumoDTO> listaInsumos = new List<AlmacenEntradaInsumoDTO>();
            List<AlmacenExistenciaInsumoCreacionDTO> listaExistenciaEntrada = new List<AlmacenExistenciaInsumoCreacionDTO>();
            var ordencompra = await _ordenCompraService.ObtenXId(parametros.ListaInsumosEnAlmacenEntrada[0].IdOrdenCompra);
            foreach(var ixem in parametros.ListaInsumosEnAlmacenEntrada)
            {
                listaInsumos.Add(new AlmacenEntradaInsumoDTO() { 
                    IdProyecto = Convert.ToInt32(almacen.IdProyecto),
                    IdRequisicion = ordencompra.IdRequisicion,
                    IdOrdenCompra = ixem.IdOrdenCompra,
                    IdAlmacenEntrada = almacenEntradaCreacion.Id,
                    IdInsumo = ixem.IdInsumo,
                    CantidadPorRecibir = ixem.CantidadPorRecibir,
                    CantidadRecibida = ixem.CantidadRecibIda,
                    Estatus = 1
                });

                listaExistenciaEntrada.Add(new AlmacenExistenciaInsumoCreacionDTO() { 
                    IdInsumo = ixem.IdInsumo,
                    IdProyecto = Convert.ToInt32(almacen.IdProyecto), 
                    IdAlmacen = parametros.IdAlmacen,
                    CantidadInsumosAumenta = ixem.CantidadRecibIda,
                    CantidadInsumosRetira = 0,
                    EsNoConsumible = false,
                    FechaRegistro = DateTime.Now,
                });

            }

            var insumosEntrdaMaterial = await _insumoXAlmacenEntradaService.CrearLista(listaInsumos);
            if (!insumosEntrdaMaterial)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrió un problema";
                return respuesta;
            }
            var insumosEntradaExistencia = await _almacenExistenciaInsumoService.CrearLista(listaExistenciaEntrada);
            foreach(var ixoc in listaInsumos) { 
                var actualizatIXOC = await _insumoXOrdenCompraService.ActualizarCantidadRecibida(ixoc.IdInsumo, Convert.ToInt32(ixoc.IdOrdenCompra), ixoc.CantidadRecibida);
                await _ordenCompraProceso.ActualizaOrdenCompraSurtidos(Convert.ToInt32(ixoc.IdOrdenCompra));
                await _actualizaRequsicionEstatusInsumos.ActualizaEstatusRequisicionInsumos(Convert.ToInt32(ixoc.IdRequisicion));
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Entrada de almacen creada";
            return respuesta;

            }

        public async Task<RespuestaDTO> CrearAjusteEntradaAlmacen(AlmacenEntradaCreacionDTO parametros, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var usuarioNombre = claims.Where(z => z.Type == "username").ToList();

            AlmacenEntradaDTO almacenEntradaDTO = new AlmacenEntradaDTO();
            almacenEntradaDTO.IdAlmacen = parametros.IdAlmacen;
            almacenEntradaDTO.IdContratista = 0;
            string NumeroAlmacenEntrada = "";
            var almacen = await _almacenService.ObtenXId(parametros.IdAlmacen);
            var almacenentrada = await ObtenXIdProyecto(Convert.ToInt32(almacen.IdProyecto));
            if (almacenentrada.Count() <= 0)
            {
                NumeroAlmacenEntrada = "EM_1";
            }
            else
            {
                NumeroAlmacenEntrada = "EM_" + (almacenentrada.Count() + 1).ToString();
            }
            almacenEntradaDTO.NoEntrada = NumeroAlmacenEntrada;
            almacenEntradaDTO.FechaRegistro = DateTime.Now;
            almacenEntradaDTO.PersonaRegistra = usuarioNombre[0].Value;
            almacenEntradaDTO.Observaciones = parametros.Observaciones;
            almacenEntradaDTO.Estatus = 2;

            var almacenEntradaCreacion = await _almacenEntradaService.CrearYObtener(almacenEntradaDTO);
            if (almacenEntradaCreacion.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creó la entrdada de material";
                return respuesta;
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Entrada de almacen creada";
            return respuesta;
        }

        public async Task<RespuestaDTO> CrearAjusteEntradaAlmacen1(AlmacenEntradaCreacionDTO parametros, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var usuarioNombre = claims.Where(z => z.Type == "username").ToList();
            if (parametros.IdAlmacen <= 0 || string.IsNullOrEmpty(parametros.Observaciones) ||
                parametros.ListaInsumosEnAlmacenEntrada.Count() <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Procure llenar los compos";
                return respuesta;
            }
            foreach (var item in parametros.ListaInsumosEnAlmacenEntrada)
            {
                if (item.CantidadRecibIda <= 0 || string.IsNullOrEmpty(item.Descripcion) || string.IsNullOrEmpty(item.Unidad) || item.IdTipoInsumo <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Capture correctamente los datos";
                    return respuesta;
                }
            }

            AlmacenEntradaDTO almacenEntradaDTO = new AlmacenEntradaDTO();
            almacenEntradaDTO.IdAlmacen = parametros.IdAlmacen;
            almacenEntradaDTO.IdContratista = 0;
            string NumeroAlmacenEntrada = "";
            var almacen = await _almacenService.ObtenXId(parametros.IdAlmacen);
            var almacenentrada = await ObtenXIdProyecto(Convert.ToInt32( almacen.IdProyecto));
            if (almacenentrada.Count() <= 0)
            {
                NumeroAlmacenEntrada = "EM_1";
            }
            else
            {
                NumeroAlmacenEntrada = "EM_" + (almacenentrada.Count() + 1).ToString();
            }
            almacenEntradaDTO.NoEntrada = NumeroAlmacenEntrada;
            almacenEntradaDTO.FechaRegistro = DateTime.Now;
            almacenEntradaDTO.PersonaRegistra = usuarioNombre[0].Value;
            almacenEntradaDTO.Observaciones = parametros.Observaciones;
            almacenEntradaDTO.Estatus = 2;

            var almacenEntradaCreacion = await _almacenEntradaService.CrearYObtener(almacenEntradaDTO);
            if (almacenEntradaCreacion.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creó la entrdada de material.";
                return respuesta;
            }

            List<AlmacenEntradaInsumoDTO> listaInsumos = new List<AlmacenEntradaInsumoDTO>();
            List<AlmacenExistenciaInsumoCreacionDTO> listaExistenciaEntrada = new List<AlmacenExistenciaInsumoCreacionDTO>();
            var insumos = new List<InsumoDTO>();
            if (almacen.IdProyecto != null)
            {
                insumos = await _insumoService.ObtenXIdProyecto(Convert.ToInt32(almacen.IdProyecto));
            }
            else
            {
                var listaIds = parametros.ListaInsumosEnAlmacenEntrada.Select(p => p.IdInsumo);
                insumos = await _insumoService.ObtenerTodos();
                insumos = insumos.Where(i => listaIds.Contains(i.id)).ToList();
            }
            for (int i = 0; i < parametros.ListaInsumosEnAlmacenEntrada.Count(); i++)
            {
                var objetoinsumo = insumos.Where(z => z.Descripcion.ToLower() == parametros.ListaInsumosEnAlmacenEntrada[i].Descripcion.ToLower() && z.Unidad.ToLower() == parametros.ListaInsumosEnAlmacenEntrada[i].Unidad.ToLower()).ToList();
                InsumoDTO crearInsumo = new InsumoDTO();
                if (objetoinsumo.Count <= 0)
                {
                    crearInsumo = await _insumoService.CrearYObtener(new InsumoCreacionDTO()
                    {
                        Descripcion = parametros.ListaInsumosEnAlmacenEntrada[i].Descripcion,
                        Unidad = parametros.ListaInsumosEnAlmacenEntrada[i].Unidad,
                        IdProyecto = Convert.ToInt32(almacen.IdProyecto),
                        idFamiliaInsumo = null,
                        idTipoInsumo = parametros.ListaInsumosEnAlmacenEntrada[i].IdTipoInsumo,
                        CostoUnitario = 0,
                        Codigo = almacenEntradaCreacion.NoEntrada.ToString()+"_"+(i+1),
                    });
                }
                else
                {
                    crearInsumo = objetoinsumo[0];
                }
                insumos.Add(crearInsumo);

                listaInsumos.Add(new AlmacenEntradaInsumoDTO()
                {
                    IdProyecto = (almacen.IdProyecto==null) ? null : Convert.ToInt32(almacen.IdProyecto),
                    IdRequisicion = 0,
                    IdOrdenCompra = 0,
                    IdAlmacenEntrada = almacenEntradaCreacion.Id,
                    IdInsumo = crearInsumo.id,
                    CantidadPorRecibir = 0,
                    CantidadRecibida = parametros.ListaInsumosEnAlmacenEntrada[i].CantidadRecibIda,
                    Estatus = 1
                });

                listaExistenciaEntrada.Add(new AlmacenExistenciaInsumoCreacionDTO()
                {
                    IdInsumo = crearInsumo.id,
                    IdProyecto = Convert.ToInt32(almacen.IdProyecto),
                    IdAlmacen = parametros.IdAlmacen,
                    CantidadInsumosAumenta = parametros.ListaInsumosEnAlmacenEntrada[i].CantidadRecibIda,
                    CantidadInsumosRetira = 0,
                    EsNoConsumible = false,
                    FechaRegistro = DateTime.Now,
                });

            }

            var insumosEntrdaMaterial = await _insumoXAlmacenEntradaService.CrearLista(listaInsumos);
            if (!insumosEntrdaMaterial)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrió un problema.";
                return respuesta;
            }
            var insumosEntradaExistencia = await _almacenExistenciaInsumoService.CrearLista(listaExistenciaEntrada);

            respuesta.Estatus = true;
            respuesta.Descripcion = "Entrada de almacen creada.";
            return respuesta;

        }

        public async Task<RespuestaDTO> CrearDevolucionEntradaAlmacen(AlmacenEntradaDevolucionCreacionDTO parametros, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var usuarioNombre = claims.Where(z => z.Type == "username").ToList();
            if (parametros.listaInsumosPrestamo.Count() <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Procure llenar los compos";
                return respuesta;
            }
            foreach (var item in parametros.listaInsumosPrestamo)
            {
                if (item.CantidadPorSalir <= 0 || string.IsNullOrEmpty(item.Descripcioninsumo) || string.IsNullOrEmpty(item.Unidadinsumo))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Capture correctamente los datos";
                    return respuesta;
                }
            }

            var almacenSalida = await _almacenSalidaService.ObtenXId(parametros.listaInsumosPrestamo[0].IdAlmacenSalida);

            AlmacenEntradaDTO almacenEntradaDTO = new AlmacenEntradaDTO();
            almacenEntradaDTO.IdAlmacen = almacenSalida.IdAlmacen;
            almacenEntradaDTO.IdContratista = 0;
            string NumeroAlmacenEntrada = "";
            var almacen = await _almacenService.ObtenXId(almacenSalida.IdAlmacen);
            var almacenentrada = await ObtenXIdProyecto(Convert.ToInt32(almacen.IdProyecto));
            if (almacenentrada.Count() <= 0)
            {
                NumeroAlmacenEntrada = "EM_1";
            }
            else
            {
                NumeroAlmacenEntrada = "EM_" + (almacenentrada.Count() + 1).ToString();
            }
            almacenEntradaDTO.NoEntrada = NumeroAlmacenEntrada;
            almacenEntradaDTO.FechaRegistro = DateTime.Now;
            almacenEntradaDTO.PersonaRegistra = usuarioNombre[0].Value;
            almacenEntradaDTO.Observaciones = "Devolucion de prestamos";
            almacenEntradaDTO.Estatus = 3;

            var almacenEntradaCreacion = await _almacenEntradaService.CrearYObtener(almacenEntradaDTO);
            if (almacenEntradaCreacion.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creó la entrdada de material";
                return respuesta;
            }

            List<AlmacenEntradaInsumoDTO> listaInsumos = new List<AlmacenEntradaInsumoDTO>();
            List<AlmacenExistenciaInsumoCreacionDTO> listaExistenciaEntrada = new List<AlmacenExistenciaInsumoCreacionDTO>();
            for (int i = 0; i < parametros.listaInsumosPrestamo.Count(); i++)
            {
                parametros.listaInsumosPrestamo[i].PrestamoFinalizado = true;
                listaInsumos.Add(new AlmacenEntradaInsumoDTO()
                {
                    IdProyecto = Convert.ToInt32(almacen.IdProyecto),
                    IdRequisicion = 0,
                    IdOrdenCompra = 0,
                    IdAlmacenEntrada = almacenEntradaCreacion.Id,
                    IdInsumo = parametros.listaInsumosPrestamo[i].IdInsumo,
                    CantidadPorRecibir = 0,
                    CantidadRecibida = parametros.listaInsumosPrestamo[i].CantidadPorSalir,
                    Estatus = 1
                });

                listaExistenciaEntrada.Add(new AlmacenExistenciaInsumoCreacionDTO()
                {
                    IdInsumo = parametros.listaInsumosPrestamo[i].IdInsumo,
                    IdProyecto = Convert.ToInt32(almacen.IdProyecto),
                    IdAlmacen = almacen.Id,
                    CantidadInsumosAumenta = parametros.listaInsumosPrestamo[i].CantidadPorSalir,
                    CantidadInsumosRetira = 0,
                    EsNoConsumible = false,
                    FechaRegistro = DateTime.Now,
                });

            }

            var insumosEntrdaMaterial = await _insumoXAlmacenEntradaService.CrearLista(listaInsumos);
            if (!insumosEntrdaMaterial)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrió un problema";
                return respuesta;
            }
            var actualizarPrestamos = await _insumoXAlmacenSalidaService.EditarLista(parametros.listaInsumosPrestamo);
            if (!actualizarPrestamos)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Los prestamos no se marcaron como finalizados";
                return respuesta;
            }
            var insumosEntradaExistencia = await _almacenExistenciaInsumoService.CrearLista(listaExistenciaEntrada);

            respuesta.Estatus = true;
            respuesta.Descripcion = "Entrada de almacen creada";
            return respuesta;

        }

        public async Task<List<AlmacenEntradaDTO>> ObtenXIdProyecto(int idProyecto)
        {
            List<AlmacenEntradaDTO> lista = new List<AlmacenEntradaDTO>();
            var almacenes = await _almacenService.ObtenerXIdProyecto(idProyecto);
            var contratistas = await _contratistaService.ObtenTodos();
            foreach (var item in almacenes) {
                var entradasAlmacen = await _almacenEntradaService.ObtenXIdAlmacen(item.Id);
                foreach (var EA in entradasAlmacen)
                {
                    var contratista = contratistas.Where(z => z.Id == EA.IdContratista).ToList();
                    lista.Add(new AlmacenEntradaDTO()
                    {
                        Id = EA.Id,
                        IdAlmacen = item.Id,
                        NombreAlmacen = item.AlmacenNombre,
                        IdContratista = contratista.Count() > 0 ? contratista[0].Id : 0,
                        RepresentanteLegal = contratista.Count() > 0 ? contratista[0].RepresentanteLegal : EA.Estatus == 1 ? "" : "Sin Contratista",
                        NoEntrada = EA.NoEntrada,
                        FechaRegistro = EA.FechaRegistro,
                        PersonaRegistra = EA.PersonaRegistra,
                        Observaciones = EA.Observaciones,
                        Estatus = EA.Estatus == 0? 1 : EA.Estatus,
                    });
                }
            }
            return lista.OrderBy(z => z.Id).Reverse().ToList();
        }

        public async Task<List<AlmacenEntradaDTO>> ObtenXIdRequisicion(int idRequisicion) {
            var insumosEntradaAlmacen = await _insumoXAlmacenEntradaService.ObtenTodos();
            var insumosEntradaAlmacenXidRequisicion = insumosEntradaAlmacen.Where(z => z.IdRequisicion == idRequisicion);
            var insumosEntradaAlmacenAgrupado = insumosEntradaAlmacenXidRequisicion.GroupBy(z => z.IdAlmacenEntrada);
            var entradasAlmacen = await _almacenEntradaService.ObtenTodos();
            var almacenes =  await _almacenService.ObtenTodos();
            var contratistas = await _contratistaService.ObtenTodos();
            List<AlmacenEntradaDTO> lista = new List<AlmacenEntradaDTO>();
            foreach (var ieag in insumosEntradaAlmacenAgrupado) {
                var almacenEntradaGrupo = entradasAlmacen.Where(z => z.Id == ieag.Key);
                var almacenEntrada = almacenEntradaGrupo.First();
                var almacen = almacenes.Where(z => z.Id == almacenEntrada.IdAlmacen).ToList();
                var contratista = contratistas.Where(z => z.Id == almacenEntrada.IdContratista).ToList();
                lista.Add(new AlmacenEntradaDTO()
                {
                    Id = almacenEntrada.Id,
                    IdAlmacen = almacen[0].Id,
                    NombreAlmacen = almacen[0].AlmacenNombre,
                    IdContratista = contratista[0].Id,
                    RepresentanteLegal = contratista[0].RepresentanteLegal,
                    NoEntrada = almacenEntrada.NoEntrada,
                    FechaRegistro = almacenEntrada.FechaRegistro,
                    PersonaRegistra = almacenEntrada.PersonaRegistra,
                    Observaciones = almacenEntrada.Observaciones,
                    Estatus = almacenEntrada.Estatus,
                });
            }
            return lista.OrderBy(z => z.Id).Reverse().ToList();
        }

        public async Task<List<AlmacenEntradaDTO>> ObtenXIdOrdenCompra(int idOrdenCompra) {
            var insumosEntradaAlmacen = await _insumoXAlmacenEntradaService.ObtenTodos();
            var insumosEntradaAlmacenXidOrdenComprA = insumosEntradaAlmacen.Where(z => z.IdOrdenCompra == idOrdenCompra);
            var insumosEntradaAlmacenAgrupado = insumosEntradaAlmacenXidOrdenComprA.GroupBy(z => z.IdAlmacenEntrada);
            var entradasAlmacen = await _almacenEntradaService.ObtenTodos();
            var almacenes = await _almacenService.ObtenTodos();
            var contratistas = await _contratistaService.ObtenTodos();
            List<AlmacenEntradaDTO> lista = new List<AlmacenEntradaDTO>();
            foreach (var ieag in insumosEntradaAlmacenAgrupado)
            {
                var almacenEntradaGrupo = entradasAlmacen.Where(z => z.Id == ieag.Key);
                var almacenEntrada = almacenEntradaGrupo.First();
                var almacen = almacenes.Where(z => z.Id == almacenEntrada.IdAlmacen).ToList();
                var contratista = contratistas.Where(z => z.Id == almacenEntrada.IdContratista).ToList();
                lista.Add(new AlmacenEntradaDTO()
                {
                    Id = almacenEntrada.Id,
                    IdAlmacen = almacen[0].Id,
                    NombreAlmacen = almacen[0].AlmacenNombre,
                    IdContratista = contratista[0].Id,
                    RepresentanteLegal = contratista[0].RepresentanteLegal,
                    NoEntrada = almacenEntrada.NoEntrada,
                    FechaRegistro = almacenEntrada.FechaRegistro,
                    PersonaRegistra = almacenEntrada.PersonaRegistra,
                    Observaciones = almacenEntrada.Observaciones,
                    Estatus = almacenEntrada.Estatus,
                });
            }
            return lista.OrderBy(z => z.Id).Reverse().ToList();
        }

        public async Task<List<AlmacenEntradaInsumoDTO>> InsumosAlmacenEntradaObtenXIdProyceto(int idProyecto)
        {
            List<AlmacenEntradaInsumoDTO> lista = new List<AlmacenEntradaInsumoDTO>();
            var insumosEntradaAlmacen = await _insumoXAlmacenEntradaService.ObtenTodos();
            var IEA = insumosEntradaAlmacen.Where(z => z.IdProyecto == idProyecto).ToList();
            var insumos = await _insumoService.ObtenXIdProyecto((int)IEA[0].IdProyecto);
            foreach (var ieap in IEA)
            {
                var insumo = insumos.Where(z => z.id == ieap.IdInsumo).ToList();
                lista.Add(new AlmacenEntradaInsumoDTO()
                {
                    Id = ieap.Id,
                    IdAlmacenEntrada = ieap.IdAlmacenEntrada,
                    IdInsumo = ieap.IdInsumo,
                    IdOrdenCompra = ieap.IdOrdenCompra,
                    IdProyecto = ieap.IdProyecto,
                    IdRequisicion = ieap.IdRequisicion,
                    CodigoInsumo = insumo[0].Codigo,
                    CantidadPorRecibir = ieap.CantidadPorRecibir,
                    CantidadRecibida = ieap.CantidadRecibida,
                    DescripcionInsumo = insumo[0].Descripcion,
                    UnidadInsumo = insumo[0].Unidad,
                    Estatus = ieap.Estatus
                });
            }
            return lista;
        }

        public async Task<List<AlmacenEntradaInsumoDTO>> InsumosAlmacenEntradaObtenXIdRequisicion(int idRequisicion) {
            List<AlmacenEntradaInsumoDTO> lista = new List<AlmacenEntradaInsumoDTO>();
            var insumosEntradaAlmacen = await _insumoXAlmacenEntradaService.ObtenTodos();
            var insumosEntradaAlmacenXidRequisicion = insumosEntradaAlmacen.Where(z => z.IdRequisicion == idRequisicion).ToList();
            var insumos = await _insumoService.ObtenXIdProyecto((int)insumosEntradaAlmacenXidRequisicion[0].IdProyecto);
            foreach (var iear in insumosEntradaAlmacenXidRequisicion)
            {
                var insumo = insumos.Where(z => z.id == iear.IdInsumo).ToList();
                lista.Add(new AlmacenEntradaInsumoDTO() {
                    Id = iear.Id,
                    IdAlmacenEntrada = iear.IdAlmacenEntrada,
                    IdInsumo = iear.IdInsumo,
                    IdOrdenCompra = iear.IdOrdenCompra,
                    IdProyecto = iear.IdProyecto,
                    IdRequisicion = iear.IdRequisicion,
                    CodigoInsumo = insumo[0].Codigo,
                    CantidadPorRecibir = iear.CantidadPorRecibir,
                    CantidadRecibida = iear.CantidadRecibida,
                    DescripcionInsumo = insumo[0].Descripcion,
                    UnidadInsumo = insumo[0].Unidad,
                    Estatus = iear.Estatus
                });
            }
            return lista;
        }
        public async Task<List<AlmacenEntradaInsumoDTO>> IsumosAlmacenEntradaObtenXIdEntradaAlmacen(int idEntradaAlmacen) {
            List<AlmacenEntradaInsumoDTO> lista = new List<AlmacenEntradaInsumoDTO>();
            var insumosEntradaAlmacen = await _insumoXAlmacenEntradaService.ObtenTodos();
            var insumosEntradaAlmacenXidEntradaAlmacen = insumosEntradaAlmacen.Where(z => z.IdAlmacenEntrada == idEntradaAlmacen).ToList();
            if (insumosEntradaAlmacenXidEntradaAlmacen.Count() <= 0) { 
                return lista;
            }
            var insumos = await _insumoService.ObtenXIdProyecto((int)insumosEntradaAlmacenXidEntradaAlmacen[0].IdProyecto);
            foreach (var iear in insumosEntradaAlmacenXidEntradaAlmacen)
            {
                var insumo = insumos.Where(z => z.id == iear.IdInsumo).ToList();
                lista.Add(new AlmacenEntradaInsumoDTO()
                {
                    Id = iear.Id,
                    IdAlmacenEntrada = iear.IdAlmacenEntrada,
                    IdInsumo = iear.IdInsumo,
                    IdOrdenCompra = iear.IdOrdenCompra,
                    IdProyecto = iear.IdProyecto,
                    IdRequisicion = iear.IdRequisicion,
                    CodigoInsumo = insumo[0].Codigo,
                    CantidadPorRecibir = iear.CantidadPorRecibir,
                    CantidadRecibida = iear.CantidadRecibida,
                    DescripcionInsumo = insumo[0].Descripcion,
                    UnidadInsumo = insumo[0].Unidad,
                    Estatus = iear.Estatus
                });
            }
            return lista;
        }
        

        }
}
