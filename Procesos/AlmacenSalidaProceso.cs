using DocumentFormat.OpenXml.InkML;
using ERP_TECKIO;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO
{
    public class AlmacenSalidaProceso<T> where T : DbContext
    {
        private readonly IAlmacenExistenciaInsumoService<T> _existenciaInsumos;
        private readonly IInsumoService<T> _insumoService;
        private readonly IAlmacenService<T> _almacenService;
        private readonly IAlmacenSalidaService<T> _almacenSalidaService;
        private readonly IInsumoXAlmacenSalidaService<T> _inusmoXAlmacenSalidaService;
        private readonly IAlmacenExistenciaInsumoService<T> _almacenExistenciaInsumoService;
        private readonly ExistenciasProceso<T> _existenciasProceso;
        private readonly IAlmacenEntradaService<T> _almacenEntradaService;
        private readonly AlmacenEntradaProceso<T> _almacenEntradaProceso;

        public AlmacenSalidaProceso(
            IAlmacenExistenciaInsumoService<T> existenciaInsumos,
            IInsumoService<T> insumoService,
            IAlmacenService<T> almacenService,
            IAlmacenSalidaService<T> almacenSalidaService, 
            IInsumoXAlmacenSalidaService<T> insumoXAlmacenSalidaService,
            IAlmacenExistenciaInsumoService<T> almacenExistenciaInsumoService,
            ExistenciasProceso<T> existenciasProceso,
            IAlmacenEntradaService<T> almacenEntradaService,
            AlmacenEntradaProceso<T> almacenEntradaProceso
            ) {
            _existenciaInsumos = existenciaInsumos;
            _insumoService = insumoService;
            _almacenService = almacenService;
            _almacenSalidaService = almacenSalidaService;
            _inusmoXAlmacenSalidaService = insumoXAlmacenSalidaService;
            _almacenExistenciaInsumoService = almacenExistenciaInsumoService;
            _existenciasProceso = existenciasProceso;
            _almacenEntradaService = almacenEntradaService;
            _almacenEntradaProceso = almacenEntradaProceso;
        }
        public async Task<RespuestaDTO> CrearAlmacenSalida(AlmacenSalidaCreacionDTO parametros, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var usuarioNombre = claims.Where(z => z.Type == "username").ToList();
            if (parametros.IdAlmacen <= 0)
            {
                respuesta.Descripcion = "Llene los campos correctamente";
                respuesta.Estatus = false;
                return respuesta;
            }

            AlmacenSalidaDTO almacenSalidaDTO = new AlmacenSalidaDTO();
            string CodigoSalida = "";
            var almacen = await _almacenService.ObtenXId(parametros.IdAlmacen);
            var almacenSalidas = await ObtenXIdProyecto(Convert.ToInt32(almacen.IdProyecto));

            if (almacenSalidas.Count() > 0)
            {
                CodigoSalida = "SA_" + (almacenSalidas.Count() + 1).ToString();
            }
            else
            {
                CodigoSalida = "SA_1";
            }
            almacenSalidaDTO.CodigoSalida = CodigoSalida;
            almacenSalidaDTO.IdAlmacen = parametros.IdAlmacen;
            almacenSalidaDTO.FechaRegistro = DateTime.Now;
            almacenSalidaDTO.CodigoCreacion = Guid.NewGuid().ToString();
            almacenSalidaDTO.Observaciones = parametros.Observaciones;
            almacenSalidaDTO.Estatus = parametros.EsBaja == false ? 1 : 2;
            almacenSalidaDTO.PersonaSurtio = usuarioNombre[0].Value;
            almacenSalidaDTO.PersonaRecibio = parametros.EsBaja == false ? parametros.PersonaRecibio : usuarioNombre[0].Value;

            var almacenSalidaCreacion = await _almacenSalidaService.CrearYObtener(almacenSalidaDTO);
            if (almacenSalidaCreacion.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creó la salida de material";
                return respuesta;
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se creó la salida de material";
            return respuesta;
        }

        public async Task<RespuestaDTO> CrearInsumoSalidaAlmacen(AlmacenSalidaInsumoCreacionDTO insumoSalidaAlmacen) {
            RespuestaDTO respuesta = new RespuestaDTO();
            var SalidaAlmacen = await _almacenSalidaService.ObtenXId(insumoSalidaAlmacen.IdSalidaAlmacen);
            var almacen = await _almacenService.ObtenXId(SalidaAlmacen.IdAlmacen);

            var insumoSA = new AlmacenSalidaInsumoDTO();
            insumoSA.IdProyecto = Convert.ToInt32(almacen.IdProyecto);
            insumoSA.IdAlmacenSalida = SalidaAlmacen.Id;
            insumoSA.IdInsumo = insumoSalidaAlmacen.IdInsumo;
            insumoSA.CantidadPorSalir = insumoSalidaAlmacen.CantidadPorSalir;
            insumoSA.EstatusInsumo = 1;
            insumoSA.EsPrestamo = insumoSalidaAlmacen.EsPrestamo;
            insumoSA.PrestamoFinalizado = false;

            var existenciaSalida = new AlmacenExistenciaInsumoCreacionDTO();
            existenciaSalida.IdInsumo = insumoSalidaAlmacen.IdInsumo;
            existenciaSalida.IdProyecto = Convert.ToInt32(almacen.IdProyecto);
            existenciaSalida.IdAlmacen = almacen.Id;
            existenciaSalida.CantidadInsumosAumenta = 0;
            existenciaSalida.CantidadInsumosRetira = insumoSalidaAlmacen.CantidadPorSalir;
            existenciaSalida.EsNoConsumible = insumoSalidaAlmacen.EsPrestamo;
            existenciaSalida.FechaRegistro = DateTime.Now;

            var insumosSalidaMaterial = await _inusmoXAlmacenSalidaService.CrearYObtener(insumoSA);
            if (insumosSalidaMaterial.Id <= 0) {
                respuesta.Descripcion = "No se guardo la salida";
                respuesta.Estatus = false;
                return respuesta;
            }
            var insumosSalidaExistencia = await _almacenExistenciaInsumoService.CreaExistenciaInsumoEntrada(existenciaSalida);

            respuesta.Descripcion = "Salida exitosa";
            respuesta.Estatus = true;
            return respuesta;
        }

        public async Task<RespuestaDTO> CrearAlmacenSalida1(AlmacenSalidaCreacionDTO parametros, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var usuarioNombre = claims.Where(z => z.Type == "username").ToList();
            if (parametros.IdAlmacen <= 0 || string.IsNullOrEmpty(parametros.Observaciones) || string.IsNullOrEmpty(parametros.PersonaRecibio) ||
                parametros.ListaAlmacenSalidaInsumoCreacion.Count() <= 0) {
                respuesta.Descripcion = "Llene los campos correctamente";
                respuesta.Estatus = false;
                return respuesta;
            }
            foreach (var insumosSalida in parametros.ListaAlmacenSalidaInsumoCreacion) {
                if (insumosSalida.CantidadPorSalir <= 0) {
                    respuesta.Descripcion = "Llene las cantidades correctamente";
                    respuesta.Estatus = false;
                    return respuesta;
                }
            }
            AlmacenSalidaDTO almacenSalidaDTO = new AlmacenSalidaDTO();
            string CodigoSalida = "";
            var almacen = await _almacenService.ObtenXId(parametros.IdAlmacen);
            var almacenSalidas = await ObtenXIdProyecto(Convert.ToInt32(almacen.IdProyecto));

            if (almacenSalidas.Count() > 0) {
                CodigoSalida = "SA_" + (almacenSalidas.Count()+1).ToString();
            }
            else
            {
                CodigoSalida = "SA_1";
            }
            almacenSalidaDTO.CodigoSalida = CodigoSalida;
            almacenSalidaDTO.IdAlmacen = parametros.IdAlmacen;
            almacenSalidaDTO.FechaRegistro = DateTime.Now;
            almacenSalidaDTO.CodigoCreacion = Guid.NewGuid().ToString();
            almacenSalidaDTO.Observaciones = parametros.Observaciones;
            almacenSalidaDTO.Estatus = parametros.EsBaja == false ? 1 : 2;
            almacenSalidaDTO.PersonaSurtio = usuarioNombre[0].Value;
            almacenSalidaDTO.PersonaRecibio = parametros.PersonaRecibio;

            var almacenSalidaCreacion = await _almacenSalidaService.CrearYObtener(almacenSalidaDTO);
            if (almacenSalidaCreacion.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creó la salida de material";
                return respuesta;
            }

            List<AlmacenSalidaInsumoDTO> listaInsumos = new List<AlmacenSalidaInsumoDTO>();
            List<AlmacenExistenciaInsumoCreacionDTO> listaExistenciaSalida = new List<AlmacenExistenciaInsumoCreacionDTO>();
            foreach(var insumosSalida in parametros.ListaAlmacenSalidaInsumoCreacion)
            {
                listaInsumos.Add(new AlmacenSalidaInsumoDTO() {
                    IdProyecto = Convert.ToInt32(almacen.IdProyecto),
                    IdAlmacenSalida = almacenSalidaCreacion.Id,
                    IdInsumo = insumosSalida.IdInsumo,
                    CantidadPorSalir = insumosSalida.CantidadPorSalir,
                    EstatusInsumo = 1,
                    EsPrestamo = insumosSalida.EsPrestamo,
                    PrestamoFinalizado = false,
                });

                listaExistenciaSalida.Add(new AlmacenExistenciaInsumoCreacionDTO()
                {
                    IdInsumo = insumosSalida.IdInsumo,
                    IdProyecto = Convert.ToInt32(almacen.IdProyecto),
                    IdAlmacen = parametros.IdAlmacen,
                    CantidadInsumosAumenta = 0,
                    CantidadInsumosRetira = insumosSalida.CantidadPorSalir,
                    EsNoConsumible = insumosSalida.EsPrestamo,
                    FechaRegistro = DateTime.Now,
                });
            }

            var insumosSalidaMaterial = await _inusmoXAlmacenSalidaService.CrearLista(listaInsumos);
            if (!insumosSalidaMaterial)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrió un problema";
                return respuesta;
            }
            var insumosSalidaExistencia = await _almacenExistenciaInsumoService.CrearLista(listaExistenciaSalida);
            respuesta.Descripcion = "Salida de almacen registrada";
            respuesta.Estatus =  true;
            return respuesta;
        }

        public async Task<List<AlmacenSalidaDTO>> ObtenXIdProyecto(int idProyecto)
        {
            List<AlmacenSalidaDTO> lista = new List<AlmacenSalidaDTO>();
            var salidasAlmacen = await _almacenSalidaService.ObtenXIdProyecto(idProyecto);
            if (salidasAlmacen.Count() <= 0) { 
                return lista;
            }
            var almacenes = await _almacenService.ObtenerXIdProyecto(idProyecto);
            foreach (var item in salidasAlmacen) {
                var almacen = almacenes.Where(z => z.Id == item.IdAlmacen).ToList();
                lista.Add(new AlmacenSalidaDTO()
                {
                    Id = item.Id,
                    CodigoSalida = item.CodigoSalida,
                    IdAlmacen = item.IdAlmacen,
                    AlmacenNombre = almacen[0].AlmacenNombre,
                    FechaRegistro = item.FechaRegistro,
                    Observaciones = item.Observaciones,
                    PersonaRecibio = item.PersonaRecibio,
                    PersonaSurtio = item.PersonaSurtio,
                    EsBaja = item.Estatus == 2 ? true : false,
                    Estatus = item.Estatus
                });
            }
            return lista.OrderBy(z => z.Id).Reverse().ToList();
        }

        public async Task<List<AlmacenSalidaDTO>> ObtenXIdProyectoSalidasConPrestamos(int idProyecto) {
            var lista = await ObtenXIdProyecto(idProyecto);
            var salidasXOC = lista.Where(z => z.Estatus == 1);
            List<AlmacenSalidaDTO> nuevaLista = new List<AlmacenSalidaDTO>();
            foreach (var salida in salidasXOC) {
                var insumosSA = await _inusmoXAlmacenSalidaService.ObtenXIdAlmacenSalida(salida.Id);
                var existenPrestamos = insumosSA.Where(z => z.EsPrestamo == true);
                if (existenPrestamos.Count() > 0) {
                    nuevaLista.Add(salida);
                }
            }
            return nuevaLista;
        }

        public async Task<List<AlmacenSalidaInsumoDTO>> InsumosAlmacenSalidaObtenXIdProyceto(int idProyecto)
        {
            List<AlmacenSalidaInsumoDTO> lista = new List<AlmacenSalidaInsumoDTO>();
            var insumosSalidaAlmacen = await _inusmoXAlmacenSalidaService.ObtenTodos();
            var ISA = insumosSalidaAlmacen.Where(z => z.IdProyecto == idProyecto).ToList();
            var salidasAlmacen = await _almacenSalidaService.ObtenXIdProyecto(idProyecto);
            var insumos = await _insumoService.ObtenXIdProyecto(idProyecto);
            foreach (var isa in ISA)
            {
                var salidaAlmacen = salidasAlmacen.Where(z => z.Id == isa.IdAlmacenSalida).ToList();
                var insumo = insumos.Where(z => z.id == isa.IdInsumo).ToList();
                lista.Add(new AlmacenSalidaInsumoDTO()
                {
                    Id = isa.Id,
                    IdAlmacenSalida = isa.IdAlmacenSalida,
                    CodigoSalida = salidaAlmacen[0].CodigoSalida,
                    IdInsumo = isa.IdInsumo,
                    Descripcioninsumo = insumo[0].Descripcion,
                    Unidadinsumo = insumo[0].Unidad,
                    CantidadPorSalir = isa.CantidadPorSalir,
                    EsPrestamo = isa.EsPrestamo
                });
            }
            return lista;
        }

        public async Task<List<AlmacenSalidaInsumoDTO>> IsumosAlmacenSalidaObtenXIdSalidaAlmacen(int idSalidaAlmacen)
        {
            List<AlmacenSalidaInsumoDTO> lista = new List<AlmacenSalidaInsumoDTO>();
            var insumosSalidaAlmacen = await _inusmoXAlmacenSalidaService.ObtenXIdAlmacenSalida(idSalidaAlmacen);
            if (insumosSalidaAlmacen.Count() <= 0) {
                return lista;
            }
            var salidasAlmacen = await _almacenSalidaService.ObtenXIdProyecto((int)insumosSalidaAlmacen[0].IdProyecto);
            var insumos = await _insumoService.ObtenXIdProyecto((int)insumosSalidaAlmacen[0].IdProyecto);
            foreach (var isa in insumosSalidaAlmacen)
            {
                var salidaAlmacen = salidasAlmacen.Where(z => z.Id == isa.IdAlmacenSalida).ToList();
                var insumo = insumos.Where(z => z.id == isa.IdInsumo).ToList();
                lista.Add(new AlmacenSalidaInsumoDTO()
                {
                    Id = isa.Id,
                    IdAlmacenSalida = isa.IdAlmacenSalida,
                    CodigoSalida = salidaAlmacen[0].CodigoSalida,
                    IdInsumo = isa.IdInsumo,
                    Descripcioninsumo = insumo[0].Descripcion,
                    Unidadinsumo = insumo[0].Unidad,
                    CantidadPorSalir = isa.CantidadPorSalir,
                    EsPrestamo = isa.EsPrestamo,
                    PersonaRecibio = salidaAlmacen[0].PersonaRecibio,
                    IdProyecto = insumosSalidaAlmacen[0].IdProyecto,
                    PrestamoFinalizado = isa.PrestamoFinalizado, 
                    Seleccionado = false
                });
            }
            return lista;
        }

        public async Task<List<AlmacenSalidaInsumoDTO>> ObtenXIdAlmacenYPrestamo(int idAlmacen)
        {
            List<AlmacenSalidaInsumoDTO> lista = new List<AlmacenSalidaInsumoDTO>();
            var salidasAlmacen = await _almacenSalidaService.ObtenXIdAlmacen(idAlmacen);
            if (salidasAlmacen.Count() <= 0) { 
                return lista;
            }
            foreach (var SA in salidasAlmacen) {
                var insumosSA = await IsumosAlmacenSalidaObtenXIdSalidaAlmacen(SA.Id);
                var insumoSAPrestamo = insumosSA.Where(z => z.EsPrestamo == true && z.PrestamoFinalizado == false );
                lista.AddRange(insumoSAPrestamo);
            }
            return lista;
        }

        public async Task<List<InsumosExistenciaDTO>> obtenerInsumosDisponibles(int idAlmacen) {
            List<InsumosExistenciaDTO> ListaID = new List<InsumosExistenciaDTO>();
            var almacen = await _almacenService.ObtenXId(idAlmacen);
            var insumosExistencia = await _existenciaInsumos.ObtenXIdAlmacen(idAlmacen);
            if (insumosExistencia.Count() <= 0)
            {
                return ListaID;
            }
            var insumosEntrada = insumosExistencia.Where(z => z.CantidadInsumosRetira == 0).GroupBy(z => z.IdInsumo).ToList();
            var insumosSalida = insumosExistencia.Where(z => z.CantidadInsumosAumenta == 0).GroupBy(z => z.IdInsumo).ToList();
            var insumos = await _insumoService.ObtenXIdProyecto(Convert.ToInt32(almacen.IdProyecto));
            
            foreach (var IE in insumosEntrada)
            {
                var insumo = insumos.Where(z => z.id == IE.Key).ToList();
                decimal cantidadE = IE.Sum(z => z.CantidadInsumosAumenta);
                var IS = insumosSalida.Find(z => z.Key == IE.Key);
                decimal cantidadS = 0;
                if (IS != null) {
                    cantidadS = IS.Sum(z => z.CantidadInsumosRetira);
                }
                if (insumo.Count() <= 0) {
                    continue;
                }
                if (cantidadE > cantidadS)
                {
                    decimal cantidadD = cantidadE - cantidadS;
                    ListaID.Add(new InsumosExistenciaDTO() { 
                        IdInsumo = IE.Key,
                        Codigo = insumo[0].Codigo,
                        Descripcion = insumo[0].Descripcion,
                        Unidad = insumo[0].Unidad,
                        CantidadDisponible = cantidadD
                    });
                }

            }
            return ListaID;
        } 

        public async Task<RespuestaDTO> transpasoInsumos(TranspasoAlmacenDTO parametro, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                //Valida que al DTO no le hagan falta datos
                if (parametro.Insumos.Count <= 0 || parametro.IdAlmacenDestino<=0 || parametro.IdAlmacenOrigen <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se puede realizar un traspaso sin insumos o sin seleccionar un almacen de destino.";
                    return respuesta;
                }
                
                //Valida que el almacen de orign no sea el mismo que el de destino
                if (parametro.IdAlmacenOrigen == parametro.IdAlmacenDestino)
                {
                    respuesta.Descripcion = "No puedes transefir al mismo almacen con el que trabajas.";
                    respuesta.Estatus = false;
                    return respuesta;
                }

                //Obtiene el nombre de usuario
                var usuarioNombre = claims.Where(z => z.Type == "username").ToList();

                //Obtiene los almacenes y las existencias
                var almacenOriInfo = await _almacenService.ObtenXId(parametro.IdAlmacenOrigen);
                var almacenDestInfo = await _almacenService.ObtenXId(parametro.IdAlmacenDestino);
                var existencias = await _existenciasProceso.obtenInsumosExistentes(parametro.IdAlmacenOrigen);
                //Comprueba que hayan existencias suficientes para los procesos
                foreach (var insumo in parametro.Insumos)
                {
                    var existencia = existencias.Find(e => e.IdInsumo == insumo.IdInsumo);
                    if (existencia == null)
                    {
                        respuesta.Estatus = false;
                        respuesta.Descripcion = "No hay existencias para algunos insumos.";
                        return respuesta;
                    }
                    if (existencia.CantidadInsumos < insumo.CantidadExistencia)
                    {
                        respuesta.Estatus = false;
                        respuesta.Descripcion = "No hay existencias suficientes para algunos insumos.";
                        return respuesta;
                    }
                }
                //var existenciasDestino = await _almacenExistenciaInsumoService.ObtenTodos();
                //Crea un par de listas para almacenar la información de los movimientos de los insumos.
                List<AlmacenSalidaInsumoCreacionDTO> insumosSalida = new List<AlmacenSalidaInsumoCreacionDTO>();
                List<AlmacenEntradaInsumoCreacionDTO> insumosEntrada = new List<AlmacenEntradaInsumoCreacionDTO>();
                foreach (var insumo in parametro.Insumos)
                {
                    var insumoObjeto = await _insumoService.ObtenXId(insumo.IdInsumo);
                    //Omití crear las existencias como tal, pues las entradas y salidas realizan la función de definir las existencias

                    //Crea la existencia para sacar el insumo del origen
                    //AlmacenExistenciaInsumoCreacionDTO existenciaOrigen = new AlmacenExistenciaInsumoCreacionDTO
                    //{
                    //    IdInsumo = insumo.IdInsumo,
                    //    IdProyecto = almacenOriInfo.IdProyecto,
                    //    IdAlmacen = parametro.IdAlmacenOrigen,
                    //    CantidadInsumosAumenta = 0,
                    //    CantidadInsumosRetira = insumo.CantidadExistencia,
                    //    EsNoConsumible = false,
                    //    FechaRegistro = DateTime.Now
                    //};
                    //respuesta = await _almacenExistenciaInsumoService.CreaExistenciaInsumoEntrada(existenciaOrigen);
                    //if (!respuesta.Estatus)
                    //{
                    //    return respuesta;
                    //}

                    ////Crea la existencia para ingresar el insumo al otro almacen
                    //AlmacenExistenciaInsumoCreacionDTO exist = new AlmacenExistenciaInsumoCreacionDTO
                    //{
                    //    IdInsumo = insumo.IdInsumo,
                    //    IdProyecto = almacenDestInfo.IdProyecto,
                    //    IdAlmacen = parametro.IdAlmacenDestino,
                    //    CantidadInsumosAumenta = insumo.CantidadExistencia,
                    //    CantidadInsumosRetira = 0,
                    //    EsNoConsumible = false,
                    //    FechaRegistro = DateTime.Now
                    //};
                    //respuesta = await _almacenExistenciaInsumoService.CreaExistenciaInsumoEntrada(exist);
                    insumosSalida.Add(new AlmacenSalidaInsumoCreacionDTO
                    {
                        IdInsumo = insumo.IdInsumo,
                        CantidadPorSalir = insumo.CantidadExistencia,
                        EsPrestamo = false
                    });
                    insumosEntrada.Add(new AlmacenEntradaInsumoCreacionDTO
                    {
                        IdInsumo = insumo.IdInsumo, 
                        IdAlmacenEntrada = parametro.IdAlmacenDestino,
                        Descripcion = ""+insumo.NombreInsumo,
                        Unidad = insumoObjeto.Unidad,
                        IdTipoInsumo = insumoObjeto.idTipoInsumo,
                        CantidadPorRecibir = insumo.CantidadExistencia,
                        CantidadRecibIda = insumo.CantidadExistencia,
                        IdOrdenCompra = 0,
                        IdInsumoXOrdenCompra = 0
                    });
                    //if (!respuesta.Estatus)
                    //{
                    //    existenciaOrigen = new AlmacenExistenciaInsumoCreacionDTO
                    //    {
                    //        IdInsumo = insumo.IdInsumo,
                    //        IdProyecto = almacenOriInfo.IdProyecto,
                    //        IdAlmacen = parametro.IdAlmacenOrigen,
                    //        CantidadInsumosAumenta = insumo.CantidadExistencia,
                    //        CantidadInsumosRetira = 0,
                    //        EsNoConsumible = false,
                    //        FechaRegistro = DateTime.Now
                    //    };
                    //    await _almacenExistenciaInsumoService.Crear(existenciaOrigen);
                    //    return respuesta;
                    //}

                }
                AlmacenSalidaCreacionDTO almacenSalida = new AlmacenSalidaCreacionDTO
                {
                    IdAlmacen = parametro.IdAlmacenOrigen,
                    Observaciones = "Traspaso al almacen "+almacenDestInfo.AlmacenNombre,
                    PersonaRecibio = usuarioNombre[0].Value,
                    IdProyecto = almacenOriInfo.IdProyecto,
                    EsBaja = false,
                    ListaAlmacenSalidaInsumoCreacion = insumosSalida
                };
                var almacenSalResult = await CrearAlmacenSalida1(almacenSalida,claims);
                if (!almacenSalResult.Estatus)
                {
                    return almacenSalResult;
                }
                AlmacenEntradaCreacionDTO almacenEntrada = new AlmacenEntradaCreacionDTO
                {
                    IdAlmacen = parametro.IdAlmacenDestino,
                    IdContratista = null,
                    Observaciones = "Traspaso desde almacen " + almacenOriInfo.AlmacenNombre,
                    ListaInsumosEnAlmacenEntrada = insumosEntrada
                };
                respuesta = await _almacenEntradaProceso.CrearAjusteEntradaAlmacen1(almacenEntrada, claims);
                if (respuesta.Estatus)
                {
                    respuesta.Descripcion = "Traspaso realizado exitosamente.";
                }
                else
                {
                    respuesta.Descripcion = "Ocurrió un error al intentar crear el traspaso.";
                }
                return respuesta;

            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrió un error al intentar realizar el traspaso.";
                return respuesta;
            }
        }

    }
}
