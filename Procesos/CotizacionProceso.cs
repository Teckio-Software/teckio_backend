using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class CotizacionProceso<T> where T : DbContext
    {
        private readonly ICotizacionService<T> _cotizacionService;
        private readonly IInsumoXCotizacionService<T> _insumoXCotizacionService;
        private readonly IInsumoService<T> _inumoService;
        private readonly IRequisicionService<T> _requisicionService;
        private readonly IInsumoXRequisicionService<T> _insumoXRequisicionService;
        private readonly IImpuestoInsumoCotizadoService<T> _impuestoInsumoCotizadoService;
        private readonly IContratistaService<T> _contratistaService;
        private readonly ActualizaEstatusSubProceso<T> _actualizaRequsicionEstatusInsumos;

        public CotizacionProceso(ICotizacionService<T> cotizacionService, IInsumoXCotizacionService<T> insumoXCotizacionService
            , IInsumoService<T> insumoService, IRequisicionService<T> requisicionService, IInsumoXRequisicionService<T> insumoXRequisicion,
            IImpuestoInsumoCotizadoService<T> impuestoInsumoCotizadoService,
            ActualizaEstatusSubProceso<T> actualizaRequsicionEstatusInsumos,
            IContratistaService<T> contratistaService)
        {
            _cotizacionService = cotizacionService;
            _insumoXCotizacionService = insumoXCotizacionService;
            _inumoService = insumoService;
            _requisicionService = requisicionService;
            _insumoXRequisicionService = insumoXRequisicion;
            _actualizaRequsicionEstatusInsumos = actualizaRequsicionEstatusInsumos;
            _impuestoInsumoCotizadoService = impuestoInsumoCotizadoService;
            _contratistaService = contratistaService;
        }
        public async Task<RespuestaDTO> CrearCotizacion(CotizacionCreacionDTO parametros){
            RespuestaDTO respuesta = new RespuestaDTO();
            CotizacionDTO cotizacionDTO = new CotizacionDTO();
            cotizacionDTO.IdProyecto = parametros.IdProyecto;
            cotizacionDTO.IdRequisicion = parametros.IdRequisicion;
            cotizacionDTO.IdContratista = 0;

            var cotizacion = await _cotizacionService.ObtenXIdRequision(parametros.IdRequisicion);
            var requisicion = await _requisicionService.ObtenXId(parametros.IdRequisicion);
            string numero = "";
            if (cotizacion.Count <= 0)
            {
                numero = requisicion.NoRequisicion + "_C_1";
            }
            else
            {
                numero = requisicion.NoRequisicion + "_C_" + (cotizacion.Count() + 1);
            }
            cotizacionDTO.NoCotizacion = numero.ToString();
            cotizacionDTO.Observaciones = "";
            cotizacionDTO.PersonaAutorizo = "";
            cotizacionDTO.PersonaCompra = "";
            cotizacionDTO.EstatusCotizacion = 1;
            cotizacionDTO.EstatusInsumosComprados = 1;

            var crearCotizacion = await _cotizacionService.CrearYObtener(cotizacionDTO);
            if (crearCotizacion.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creó la cotización";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "cotización creada";
            return respuesta;
        }

        public async Task<RespuestaDTO> CrearInsumoCotizado(InsumoXCotizacionCreacionDTO objeto) {
            var insumoXRequisicion = await _insumoXRequisicionService.ObtenXId(objeto.IdInsumoRequisicion);

            var respuesta = new RespuestaDTO();
            var insumoCotizado = new InsumoXCotizacionDTO();
            decimal importeSinIVa = objeto.Cantidad * objeto.PrecioUnitario;
            decimal importeTotal = importeSinIVa;
            if (objeto.PIVA > 0) {
                importeTotal += (importeSinIVa * objeto.PIVA / 100);
            }

            insumoCotizado.IdCotizacion = objeto.IdCotizacion;
            insumoCotizado.IdInsumoRequisicion = objeto.IdInsumoRequisicion;
            insumoCotizado.IdInsumo = objeto.IdInsumo;
            insumoCotizado.Cantidad = objeto.Cantidad;
            insumoCotizado.PrecioUnitario = objeto.PrecioUnitario;
            insumoCotizado.ImporteSinIva = importeSinIVa;
            insumoCotizado.ImporteTotal = importeTotal - objeto.Descuento;
            insumoCotizado.EstatusInsumoCotizacion = 1;
            insumoCotizado.Descuento = objeto.Descuento;

            var ICotizado = await _insumoXCotizacionService.CrearYObtener(insumoCotizado);
            if (ICotizado.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se guardo en insumo cotizado";
                return respuesta;
            }

            var insumo = await _inumoService.ObtenXId(objeto.IdInsumo);
            if (insumo.CostoUnitario < ICotizado.PrecioUnitario && insumo.CostoUnitario == 0)
            {
                insumo.CostoUnitario = ICotizado.PrecioUnitario;
                var actializaCosto = await _inumoService.Editar(insumo);
            }
            if (ICotizado.Id > 0 && objeto.PIVA > 0)
            {
                ImpuestoInsumoCotizadoCreacionDTO impuesto = new ImpuestoInsumoCotizadoCreacionDTO();
                impuesto.IdImpuesto = 1;
                impuesto.IdInsumoCotizado = ICotizado.Id;
                impuesto.Porcentaje = objeto.PIVA;
                impuesto.Importe = objeto.PIVA * ICotizado.ImporteSinIva / 100;
                var Impuesto = await _impuestoInsumoCotizadoService.Crear(impuesto);
            }

            await _actualizaRequsicionEstatusInsumos.ActualizaEstatusRequisicionInsumos(insumoXRequisicion.IdRequisicion);

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se guardo el insummo cotizado";
            return respuesta;
        }

        public async Task<RespuestaDTO> CrearCotizacion1(CotizacionCreacionDTO parametros) {
            RespuestaDTO respuesta = new RespuestaDTO();
            if (parametros.IdProyecto <= 0 || parametros.IdRequisicion <= 0 || parametros.IdContratista <= 0 || parametros.ListaInsumosCotizacion.Count() <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Capture todos los campos";
                return respuesta;
            }
            for (int i = 0; i < parametros.ListaInsumosCotizacion.Count(); i++) {
                decimal importe = parametros.ListaInsumosCotizacion[i].PrecioUnitario * parametros.ListaInsumosCotizacion[i].Cantidad;
                if (parametros.ListaInsumosCotizacion[i].IdInsumoRequisicion <= 0 || parametros.ListaInsumosCotizacion[i].Descripcion == "" ||
                    parametros.ListaInsumosCotizacion[i].Unidad == "" || parametros.ListaInsumosCotizacion[i].Cantidad <= 0 ||
                    parametros.ListaInsumosCotizacion[i].PrecioUnitario <= 0 || parametros.ListaInsumosCotizacion[i].Descuento > importe ||
                    parametros.ListaInsumosCotizacion[i].PIVA > 16 || parametros.ListaInsumosCotizacion[i].PIVA < 0) {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Capture todos los campos correctamente";
                    return respuesta;
                }
            }
            CotizacionDTO cotizacionDTO = new CotizacionDTO();
            cotizacionDTO.IdProyecto = parametros.IdProyecto;
            cotizacionDTO.IdRequisicion = parametros.IdRequisicion;
            cotizacionDTO.IdContratista = parametros.IdContratista;

            var cotizacion = await _cotizacionService.ObtenXIdRequision(parametros.IdRequisicion);
            var requisicion = await _requisicionService.ObtenXId(parametros.IdRequisicion);
            string numero = "";
            if (cotizacion.Count <= 0) {
                numero = requisicion.NoRequisicion + "_C_1";
            }
            else
            {
                numero = requisicion.NoRequisicion + "_C_" + (cotizacion.Count() + 1);
            }
            cotizacionDTO.NoCotizacion = numero.ToString();
            cotizacionDTO.Observaciones = parametros.Observaciones;
            cotizacionDTO.PersonaAutorizo = "";
            cotizacionDTO.PersonaCompra = "";
            cotizacionDTO.EstatusCotizacion = 1;
            cotizacionDTO.EstatusInsumosComprados = 1;

            var crearCotizacion = await _cotizacionService.CrearYObtener(cotizacionDTO);
            if (crearCotizacion.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creó la cotización";
                return respuesta;
            }

            var listaInsumos = await _inumoService.ObtenerInsumoXProyecto(parametros.IdProyecto);

            List<InsumoXCotizacionDTO> insumos = new List<InsumoXCotizacionDTO>();

            for (int i = 0; i < parametros.ListaInsumosCotizacion.Count; i++) {
                var objetoinsumo = listaInsumos.Where(z => z.Descripcion.ToLower() == parametros.ListaInsumosCotizacion[i].Descripcion.ToLower() && z.Unidad.ToLower() == parametros.ListaInsumosCotizacion[i].Unidad.ToLower()).ToList();
                InsumoDTO crearInsumo = new InsumoDTO();
                if (objetoinsumo.Count <= 0)
                {
                    crearInsumo = await _inumoService.CrearYObtener(new InsumoCreacionDTO()
                    {
                        Descripcion = parametros.ListaInsumosCotizacion[i].Descripcion,
                        Unidad = parametros.ListaInsumosCotizacion[i].Unidad,
                        IdProyecto = parametros.IdProyecto,
                        idFamiliaInsumo = null,
                        idTipoInsumo = 10004,
                        CostoUnitario = 0,
                        Codigo = Guid.NewGuid().ToString(),
                    });
                }
                else
                {
                    crearInsumo = objetoinsumo[0];
                }
                listaInsumos.Add(crearInsumo);

                decimal importeSinIVa = parametros.ListaInsumosCotizacion[i].Cantidad * parametros.ListaInsumosCotizacion[i].PrecioUnitario;
                insumos.Add(new InsumoXCotizacionDTO() { 
                    IdCotizacion = crearCotizacion.Id,
                    IdInsumoRequisicion = parametros.ListaInsumosCotizacion[i].IdInsumoRequisicion,
                    IdInsumo = crearInsumo.id,
                    Cantidad = parametros.ListaInsumosCotizacion[i].Cantidad,
                    PrecioUnitario = parametros.ListaInsumosCotizacion[i].PrecioUnitario,
                    ImporteSinIva = importeSinIVa,
                    ImporteTotal = importeSinIVa - parametros.ListaInsumosCotizacion[i].Descuento,
                    EstatusInsumoCotizacion = 1,
                    Descuento = parametros.ListaInsumosCotizacion[i].Descuento
                });
            }
            foreach (var insumo in insumos) {
                var ICotizado = await _insumoXCotizacionService.CrearYObtener(insumo);
                var insumoBuscar = listaInsumos.Where(z => z.id == ICotizado.IdInsumo).ToList();
                if (insumoBuscar[0].CostoUnitario < ICotizado.PrecioUnitario && insumoBuscar[0].CostoUnitario == 0) {
                    insumoBuscar[0].CostoUnitario = ICotizado.PrecioUnitario;
                    var actializaCosto = await _inumoService.Editar(insumoBuscar[0]);
                }
                var i = listaInsumos.Where(z => z.id == insumo.IdInsumo).ToList();
                var insumoparametro = parametros.ListaInsumosCotizacion.Where(z => z.Descripcion == i[0].Descripcion && z.Unidad == i[0].Unidad).ToList();
                if (ICotizado.Id > 0 && insumoparametro[0].PIVA > 0) {
                    ImpuestoInsumoCotizadoCreacionDTO impuesto = new ImpuestoInsumoCotizadoCreacionDTO();
                    impuesto.IdImpuesto = 1;
                    impuesto.IdInsumoCotizado = ICotizado.Id;
                    impuesto.Porcentaje = insumoparametro[0].PIVA;
                    impuesto.Importe = insumoparametro[0].PIVA * ICotizado.ImporteSinIva / 100;
                    var Impuesto = await _impuestoInsumoCotizadoService.Crear(impuesto);
                }

            }

            await _actualizaRequsicionEstatusInsumos.ActualizaEstatusRequisicionInsumos(crearCotizacion.IdRequisicion);

            respuesta.Estatus = true;
            respuesta.Descripcion = "Cotización creada";
            return respuesta;
        }

        public async Task<List<CotizacionDTO>> CotizacionesXIdRequisicion(int idRequisicion)
        {
            List<CotizacionDTO> listC = new List<CotizacionDTO>();
            var ICXR = await _cotizacionService.ObtenXIdRequision(idRequisicion);
            var contratistas = await _contratistaService.ObtenTodos();
            foreach (var c in ICXR)
            {
                var contratista = contratistas.Where(z => z.Id == c.IdContratista).ToList();
                
                string descripcionEstatus = "";
                switch (c.EstatusInsumosComprados)
                {
                    case 1:
                        descripcionEstatus = "Capturada";
                        break;
                    case 2:
                        descripcionEstatus = "Autorizada";
                        break;
                    case 3:
                        descripcionEstatus = "Comprada";
                        break;
                    case 4:
                        descripcionEstatus = "Cancelada";
                        break;
                    default:
                        descripcionEstatus = "";
                        break;
                }
                string cotizacionEstatus = "";
                switch (c.EstatusCotizacion)
                {
                    case 1:
                        cotizacionEstatus = "Capturada";
                        break;
                    case 2:
                        cotizacionEstatus = "Autorizada";
                        break;
                    case 3:
                        cotizacionEstatus = "Comprada";
                        break;
                    case 4:
                        cotizacionEstatus = "Cancelada";
                        break;
                    default:
                        descripcionEstatus = "";
                        break;
                }

                listC.Add(new CotizacionDTO()
                {
                    Id = c.Id,
                    NoCotizacion = c.NoCotizacion,
                    Observaciones = c.Observaciones,
                    FechaRegistro = c.FechaRegistro,
                    EstatusCotizacion = c.EstatusCotizacion,
                    EstatusCotizacionDescripcion = cotizacionEstatus,
                    EstatusInsumosCompradosDescripcion = descripcionEstatus,
                    IdContratista = c.IdContratista,
                    RepresentanteLegal = contratista.Count() == 0 ? "" : contratista[0].RepresentanteLegal,
                });
            }

            return listC;
        }
        public async Task<List<ListaInsumoXCotizacionDTO>> ListarInsumosCotizadosXIdRequisicion(int idRequisicion, int idProyecto)
        {
            List<ListaInsumoXCotizacionDTO> listIC = new List<ListaInsumoXCotizacionDTO>();
            var insumosXrequisicion = await _insumoXRequisicionService.ObtenXIdRequisicion(idRequisicion);
            var insumos = await _inumoService.ObtenXIdProyecto(idProyecto);
            var IC = await _insumoXCotizacionService.ObtenTodos();
            List<InsumoXCotizacionDTO> nuevaLista = new List<InsumoXCotizacionDTO>();
            foreach (var x in insumosXrequisicion) {
                var ICfiltro = IC.Where(z => z.IdInsumoRequisicion == x.Id).ToList();
                nuevaLista.AddRange(ICfiltro);
            }
            
            for (int i = 0; i < nuevaLista.Count(); i++)
            {
                var insumoXrequisicion = insumosXrequisicion.Where(z => z.Id == nuevaLista[i].IdInsumoRequisicion).ToList();
                var insumoR = insumos.Where(z => z.id == insumoXrequisicion[0].IdInsumo).ToList();
                var insumoC = insumos.Where(z => z.id == nuevaLista[i].IdInsumo).ToList();

                string descripcionEstatus = "";
                switch (nuevaLista[i].EstatusInsumoCotizacion)
                {
                    case 1:
                        descripcionEstatus = "Capturada";
                        break;
                    case 2:
                        descripcionEstatus = "Autorizada";
                        break;
                    case 3:
                        descripcionEstatus = "Comprada";
                        break;
                    case 4:
                        descripcionEstatus = "Cancelada";
                        break;
                    default:
                        descripcionEstatus = "";
                        break;
                }

                listIC.Add(new ListaInsumoXCotizacionDTO() {
                    Id = nuevaLista[i].Id,
                    IdCotizacion = nuevaLista[i].IdCotizacion,
                    IdInsumoRequisicion = nuevaLista[i].IdInsumoRequisicion,
                    Descripcion = insumoC[0].Descripcion,
                    UnidadCotizada = insumoC[0].Unidad,
                    CantidadCotizada = nuevaLista[i].Cantidad,
                    Unidad = insumoR[0].Unidad,
                    Cantidad = insumoXrequisicion[0].Cantidad,
                    PrecioUnitario = nuevaLista[i].PrecioUnitario,
                    ImporteTotal = nuevaLista[i].ImporteTotal,
                    ImporteSinIva = nuevaLista[i].ImporteSinIva,
                    EstatusInsumoCotizacion = nuevaLista[i].EstatusInsumoCotizacion,
                    EstatusInsumoCotizacionDescripcion = descripcionEstatus,
                    Descuento = nuevaLista[i].Descuento
                });
            }
            
            return listIC;
        }
        public async Task<List<ListaInsumoXCotizacionDTO>> ListarInsumosXCotizacion(int idCotizacion) {
            List<ListaInsumoXCotizacionDTO> listIC = new List<ListaInsumoXCotizacionDTO>();
            var cotizacion = await _cotizacionService.ObtenXId(idCotizacion);
            var idRequisicion = cotizacion.IdRequisicion;
            var idProyecto = cotizacion.IdProyecto;
            var IC = await _insumoXCotizacionService.ObtenXIdCotizacion(idCotizacion);
            var insumosXrequisicion = await _insumoXRequisicionService.ObtenXIdRequisicion(idRequisicion);
            var insumos = await _inumoService.ObtenXIdProyecto(idProyecto);

            for (int i = 0; i < IC.Count(); i++)
            {
                var insumoXrequisicion = insumosXrequisicion.Where(z => z.Id == IC[i].IdInsumoRequisicion).ToList();
                var insumoR = insumos.Where(z => z.id == insumoXrequisicion[0].IdInsumo).ToList();
                var insumoC = insumos.Where(z => z.id == IC[i].IdInsumo).ToList();

                string descripcionEstatus = "";
                switch (IC[i].EstatusInsumoCotizacion)
                {
                    case 1:
                        descripcionEstatus = "Capturada";
                        break;
                    case 2:
                        descripcionEstatus = "Autorizada";
                        break;
                    case 3:
                        descripcionEstatus = "Comprada";
                        break;
                    case 4:
                        descripcionEstatus = "Cancelada";
                        break;
                    default:
                        descripcionEstatus = "";
                        break;

                }

                listIC.Add(new ListaInsumoXCotizacionDTO()
                {
                    Id = IC[i].Id,
                    IdCotizacion = IC[i].IdCotizacion,
                    NoCotizacion = cotizacion.NoCotizacion,
                    IdInsumoRequisicion = IC[i].IdInsumoRequisicion,
                    IdInsumo = insumoC[0].id,
                    Descripcion = insumoC[0].Descripcion,
                    UnidadCotizada = insumoC[0].Unidad,
                    CantidadCotizada = IC[i].Cantidad,
                    Unidad = insumoR[0].Unidad,
                    Cantidad = IC[i].Cantidad,
                    PrecioUnitario = IC[i].PrecioUnitario,
                    ImporteTotal = IC[i].ImporteTotal,
                    ImporteSinIva = IC[i].ImporteSinIva,
                    EstatusInsumoCotizacion = IC[i].EstatusInsumoCotizacion,
                    EstatusInsumoCotizacionDescripcion = descripcionEstatus,
                    Descuento = IC[i].Descuento
                });
            }

            return listIC;
        }

        public async Task<List<ListaInsumoXCotizacionDTO>> ListarInsumosXCotizacionNoComprados(int idCotizacion) {
            var insumosXCotizacion = await ListarInsumosXCotizacion(idCotizacion);
            var insumosNoComprados = insumosXCotizacion.Where(z => z.EstatusInsumoCotizacion == 2).ToList();
            return insumosNoComprados;
        }

        public async Task<RespuestaDTO> AutorizarTodos(int idCotizacion, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var usuarioNombre = claims.Where(z => z.Type == "username").ToList();
            var insumosXidCotizacion = await _insumoXCotizacionService.ObtenXIdCotizacion(idCotizacion);
            var insumosXcotizacionCapturados = insumosXidCotizacion.Where(z => z.EstatusInsumoCotizacion == 1).ToList();

            foreach(var element in insumosXcotizacionCapturados)
            {
                var AutorizarInsumo = await _insumoXCotizacionService.ActualizarEstatusAutorizar(element.Id);
                if (!AutorizarInsumo.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se autorizo el insumo";
                    return respuesta;   
                }
            }
            var cotizacion = await _cotizacionService.ObtenXId(idCotizacion);
            cotizacion.PersonaAutorizo = usuarioNombre[0].Value;
            var actualizaPersonaAutorizo = await _cotizacionService.Editar(cotizacion);
            await _actualizaRequsicionEstatusInsumos.ActualizaEstatusRequisicionInsumos(cotizacion.IdRequisicion);
            await ActualizarEstatusCotizacion(cotizacion.Id);
            respuesta.Estatus = true;
            respuesta.Descripcion = "Insumos autorizados";
            return respuesta;

        }

        public async Task<RespuestaDTO> ActualizarInsumosCotizados(CotizacionObjetoRequisicionDTO Cotizacion)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            int numero = 0;
            foreach (var insumoXcotizacion in Cotizacion.InsumoXCotizacion) {
                var resultado = await ActualizarInsumoXCotizacion(insumoXcotizacion);
                if (resultado.Estatus) { 
                    numero++;
                }
            }

            if (numero == 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "los insumos no cuentan con permiso para ser actualizados";
                return respuesta;
            }
            if(numero < Cotizacion.InsumoXCotizacion.Count() && numero > 0)
            {
                respuesta.Estatus = true;
                respuesta.Descripcion = "Algunos insumos no cuentan con permiso para ser actualizados";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se han actualizado los insumos";
            return respuesta;

        }

        public async Task<RespuestaDTO> AutorizarXIdInsumoCotizado(int idInusmoCotizado)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var insumoXCotizacion = await _insumoXCotizacionService.ObtenXId(idInusmoCotizado);
            if(insumoXCotizacion.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se encontró el insumo cotizado";
                return respuesta;
            }
            if (insumoXCotizacion.EstatusInsumoCotizacion == 3)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El insumo ya está comprado";
                return respuesta;
            }
            if (insumoXCotizacion.EstatusInsumoCotizacion == 2)
            {

                respuesta.Estatus = true;
                respuesta.Descripcion = "El inusmo ya está autorizado";
                return respuesta;
            }
            else
            {
                var AutorizarInsumo = await _insumoXCotizacionService.ActualizarEstatusAutorizar(idInusmoCotizado);
                if (!AutorizarInsumo.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se autorizo el insumo";
                    return respuesta;
                }
                var cotizacion = await _cotizacionService.ObtenXId(insumoXCotizacion.IdCotizacion);
                await ActualizarEstatusCotizacion(cotizacion.Id);
                await _actualizaRequsicionEstatusInsumos.ActualizaEstatusRequisicionInsumos(cotizacion.IdRequisicion);
                respuesta.Estatus = true;
                respuesta.Descripcion = "Insumo autorizado";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> AutorizarInsumosCotizadosSeleccionados(List<ListaInsumoXCotizacionDTO> seleccionados)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            if(seleccionados.Count <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se seleccionaron insumos cotizados";
                return respuesta;
            }
            foreach(var item in seleccionados)
            {
                var verificacion = await _insumoXCotizacionService.ObtenXId(item.Id);
                if (verificacion.EstatusInsumoCotizacion != 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se autorizaron los insumos cotizados";
                    return respuesta;
                }
            }
            foreach (var item in seleccionados)
            {
                var AutorizarInsumo = await _insumoXCotizacionService.ActualizarEstatusAutorizar(item.Id);
                if (!AutorizarInsumo.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se autorizaron los insumos cotizados";
                    return respuesta;
                }
            }
            var cotizacion = await _cotizacionService.ObtenXId(seleccionados[0].IdCotizacion);
            await _actualizaRequsicionEstatusInsumos.ActualizaEstatusRequisicionInsumos(cotizacion.IdRequisicion);
            respuesta.Estatus = true;
            respuesta.Descripcion = "Los insumos han sido autorizados";
            await ActualizarEstatusCotizacion(cotizacion.Id);
            return respuesta;
        }

        public async Task ActualizarEstatusCotizacion(int idCotizacion) {
            var insumosXcotizacion = await _insumoXCotizacionService.ObtenXIdCotizacion(idCotizacion);
            var insumosCapturados = insumosXcotizacion.Where(z => z.EstatusInsumoCotizacion == 1).Count();
            var insumosAutorizados = insumosXcotizacion.Where(z => z.EstatusInsumoCotizacion == 2).Count();
            var insumosComprados = insumosXcotizacion.Where(z => z.EstatusInsumoCotizacion == 3).Count();
            var insumosCancelados = insumosXcotizacion.Where(z => z.EstatusInsumoCotizacion == 4).Count();
            var insumosTotales = insumosXcotizacion.Count() - insumosCancelados;

            if(insumosCancelados == insumosXcotizacion.Count() && insumosCancelados > 0) {
                var actualizaEstatusCotizacion = await _cotizacionService.ActualizarEstatusCotizacion(idCotizacion, 4);
            }
            if (insumosTotales == insumosComprados && insumosComprados > 0)
            {
                var actualizaEstatusCotizacion = await _cotizacionService.ActualizarEstatusCotizacion(idCotizacion, 3);
            }
            if (insumosAutorizados > 0 && insumosTotales > 0)
            {
                var actualizaEstatusCotizacion = await _cotizacionService.ActualizarEstatusCotizacion(idCotizacion, 2);
            }
            if (insumosCapturados > 0  && insumosAutorizados == 0)
            {
                var actualizaEstatusCotizacion = await _cotizacionService.ActualizarEstatusCotizacion(idCotizacion, 1);
            }
        }

        public async Task<RespuestaDTO> ActualizarInsumoXCotizacion(InsumosXCotizacionObjetoRequisicionDTO insumosXCotizacion) { 
            RespuestaDTO respuesta = new RespuestaDTO();
            var insumoXCotizacion = await _insumoXCotizacionService.ObtenXId(insumosXCotizacion.IdInsumoXCotizacion);
            if(insumoXCotizacion.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El insumo no se encontro";
                return respuesta;
            }
            if (insumoXCotizacion.EstatusInsumoCotizacion != 1) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El insumo no tiene permiso para ser actualizado";
                return respuesta;
            }

            insumoXCotizacion.Cantidad = insumosXCotizacion.Cantidad;
            insumoXCotizacion.PrecioUnitario = insumosXCotizacion.PrecioUnitario;
            insumoXCotizacion.Descuento = insumosXCotizacion.Descuento;
            insumoXCotizacion.ImporteSinIva = insumoXCotizacion.Cantidad * insumoXCotizacion.PrecioUnitario;
            var impuestos = await _impuestoInsumoCotizadoService.ObtenerXIdInsumoCotizado(insumoXCotizacion.Id);
            foreach (var impuesto in impuestos)
            {
                    impuesto.Importe = impuesto.Porcentaje * insumoXCotizacion.ImporteSinIva / 100;
                    var editaImpuesto = await _impuestoInsumoCotizadoService.Editar(impuesto);
            }
            impuestos = await _impuestoInsumoCotizadoService.ObtenerXIdInsumoCotizado(insumoXCotizacion.Id);
            decimal sumaImpuestos = impuestos.Sum(z => z.Importe);
            insumoXCotizacion.ImporteTotal = insumoXCotizacion.ImporteSinIva + sumaImpuestos - insumoXCotizacion.Descuento;

            var actualizaIC = await _insumoXCotizacionService.Editar(insumoXCotizacion);
            if (!actualizaIC.Estatus)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El insumo cotizado no ha sido actualizado";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "El insumo cotizado ha sido actualizado";
            return respuesta;

        }

        public async Task<RespuestaDTO> CrearImpuestosInsumoCotizado(List<ImpuestoInsumoCotizadoDTO> parametros, int idInsumoCotizado)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var insumoCotizado = await _insumoXCotizacionService.ObtenXId(idInsumoCotizado);
            if(insumoCotizado.EstatusInsumoCotizacion != 1)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El insumo cotizado no tiene permiso para ser editado";
            }
            var ImpuestosXIC = await _impuestoInsumoCotizadoService.ObtenerXIdInsumoCotizado(insumoCotizado.Id);
            foreach (var impuesto in parametros)
            {
                if (impuesto.Id > 0) {
                    impuesto.Importe = impuesto.Porcentaje * insumoCotizado.ImporteSinIva / 100;
                    var editaImpuesto = await _impuestoInsumoCotizadoService.Editar(impuesto);
                }
                else
                {
                    var impuestoEncontrado = ImpuestosXIC.Where(z => z.IdImpuesto == impuesto.IdImpuesto).ToList();
                    if (impuestoEncontrado.Count() > 0) {
                        impuestoEncontrado[0].Porcentaje = impuesto.Porcentaje;
                        impuestoEncontrado[0].Importe = impuesto.Porcentaje * insumoCotizado.ImporteSinIva / 100;
                        var editaImpuesto = await _impuestoInsumoCotizadoService.Editar(impuestoEncontrado[0]);
                    }
                    else {
                        ImpuestoInsumoCotizadoCreacionDTO impuestoCrear = new ImpuestoInsumoCotizadoCreacionDTO();
                        impuestoCrear.IdImpuesto = impuesto.IdImpuesto;
                        impuestoCrear.IdInsumoCotizado = insumoCotizado.Id;
                        impuestoCrear.Porcentaje = impuesto.Porcentaje;
                        impuestoCrear.Importe = impuesto.Porcentaje * insumoCotizado.ImporteSinIva / 100;
                        var creaImpuesto = await _impuestoInsumoCotizadoService.Crear(impuestoCrear);
                    }
                }
            }
            var impuestos = await _impuestoInsumoCotizadoService.ObtenerXIdInsumoCotizado(idInsumoCotizado);
            decimal sumaImpuestos = impuestos.Sum(z => z.Importe);

            insumoCotizado.ImporteTotal = insumoCotizado.ImporteSinIva + sumaImpuestos - insumoCotizado.Descuento;

            var editaInsumoCotizado =  await _insumoXCotizacionService.Editar(insumoCotizado);
            if (!editaInsumoCotizado.Estatus) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se actualizo el insumo cotizado";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se actualizo el insumo cotizado";
            return respuesta;
        }

        public async Task<RespuestaDTO> EliminarImpuestoInsumoCotizado(int IdInsumoCotizado, int idTipoImpuesto)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var ImpuestosXIC = await _impuestoInsumoCotizadoService.ObtenerXIdInsumoCotizado(IdInsumoCotizado);
            var impuestoEncontrado = ImpuestosXIC.Where(z => z.IdImpuesto == idTipoImpuesto).ToList();
            if (impuestoEncontrado.Count() <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se encontro el impuesto";
                return respuesta;
            }
            var elimnarObjeto = await _impuestoInsumoCotizadoService.Eliminar(impuestoEncontrado[0]);
            var insumoCotizado = await _insumoXCotizacionService.ObtenXId(IdInsumoCotizado);
            if (elimnarObjeto.Estatus)
            {
                ImpuestosXIC = await _impuestoInsumoCotizadoService.ObtenerXIdInsumoCotizado(IdInsumoCotizado);
                if (ImpuestosXIC.Count() > 0) {
                    decimal sumaImpuestos = ImpuestosXIC.Sum(z => z.Importe);
                    insumoCotizado.ImporteTotal = insumoCotizado.ImporteSinIva + sumaImpuestos - insumoCotizado.Descuento;
                    var editaInsumoCotizado = await _insumoXCotizacionService.Editar(insumoCotizado);
                }
                else
                {
                    insumoCotizado.ImporteTotal = insumoCotizado.ImporteSinIva - insumoCotizado.Descuento;
                    var editaInsumoCotizado = await _insumoXCotizacionService.Editar(insumoCotizado);
                }
            }
            else
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se elimino el impuesto";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se elimino el impuesto";
            return respuesta;
        }


        public async Task<RespuestaDTO> RemoverAutorizacion(int IdInsumoCotizado) {
            var insumoCotizado = await _insumoXCotizacionService.ObtenXId(IdInsumoCotizado);
            var removerAutorizacion = await _insumoXCotizacionService.ActualizarEstatusRemoverAutorizar(insumoCotizado.Id);
            await ActualizarEstatusCotizacion(insumoCotizado.IdCotizacion);
            return removerAutorizacion;
        }

    }
}
