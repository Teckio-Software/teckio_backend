using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO
{
    public class OrdenCompraProceso<T> where T : DbContext
    {
        private readonly IOrdenCompraService<T> _ordenCompraService;
        private readonly IInsumoXOrdenCompraService<T> _insumoXOrdenCompraService;
        private readonly IInsumoXCotizacionService<T> _insumoXCotizacionService;
        private readonly ICotizacionService<T> _cotizacionService;
        private readonly IInsumoService<T> _insumos;
        private readonly CotizacionProceso<T> _cotizacionProceso;
        private readonly ActualizaEstatusSubProceso<T> _actualizaRequsicionEstatusInsumos;
        private readonly IImpuestoInsumoOrdenCompraService<T> _impuestoInsumoOrdenCompraService;
        private readonly IImpuestoInsumoCotizadoService<T> _impuestoInsumoCotizadoService;
        private readonly ITipoImpuestoService<T> _tipoImpuestoService;

        public OrdenCompraProceso(
            IOrdenCompraService<T> ordenCompraService,
            IInsumoXOrdenCompraService<T> insumoXOrdenCompraService,
            IInsumoXCotizacionService<T> insumoXCotizacionService,
            ICotizacionService<T> cotizacionService
            , IInsumoService<T> insumos
            , CotizacionProceso<T> cotizacionProceso
            , ActualizaEstatusSubProceso<T> actualizaRequsicionEstatusInsumos
            , IImpuestoInsumoOrdenCompraService <T> impuestoInsumoOrdenCompraService
            , IImpuestoInsumoCotizadoService<T> impuestoInsumoCotizadoService
            , ITipoImpuestoService<T> tipoImpuestoService
            )
        {
            _ordenCompraService = ordenCompraService;
            _insumoXOrdenCompraService = insumoXOrdenCompraService;
            _insumoXCotizacionService = insumoXCotizacionService;
            _cotizacionService = cotizacionService;
            _insumos = insumos;
            _cotizacionProceso = cotizacionProceso;
            _actualizaRequsicionEstatusInsumos = actualizaRequsicionEstatusInsumos;
            _impuestoInsumoOrdenCompraService = impuestoInsumoOrdenCompraService ;
            _impuestoInsumoCotizadoService = impuestoInsumoCotizadoService;
            _tipoImpuestoService = tipoImpuestoService;
        }

        public async Task<RespuestaDTO> CrearOrdenCompra(OrdenCompraCreacionDTO parametros, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var usuarioNombre = claims.Where(z => z.Type == "username").ToList();

            OrdenCompraDTO ordenCompraDTO = new OrdenCompraDTO();
            var cotizacion = await _cotizacionService.ObtenXId(parametros.IdCotizacion);
            ordenCompraDTO.IdContratista = cotizacion.IdContratista;
            ordenCompraDTO.IdRequisicion = cotizacion.IdRequisicion;
            ordenCompraDTO.IdProyecto = cotizacion.IdProyecto;
            ordenCompraDTO.IdCotizacion = cotizacion.Id;
            var ordencCompra = await _ordenCompraService.ObtenXIdCotizacion(parametros.IdCotizacion);
            string numero = "";
            if (ordencCompra.Count <= 0)
            {
                numero = cotizacion.NoCotizacion + "_OC_1";
            }
            else
            {
                numero = cotizacion.NoCotizacion + "_OC_" + (ordencCompra.Count() + 1).ToString();
            }
            ordenCompraDTO.NoOrdenCompra = numero;
            ordenCompraDTO.Elaboro = usuarioNombre[0].Value;
            ordenCompraDTO.Estatus = 1;
            ordenCompraDTO.Chofer = parametros.Chofer;
            ordenCompraDTO.Observaciones = parametros.Observaciones;
            ordenCompraDTO.EstatusInsumosSurtidos = 1;
            ordenCompraDTO.FechaRegistro = DateTime.Now;

            var crearOrdenCompra = await _ordenCompraService.CrearYObtener(ordenCompraDTO);
            if (crearOrdenCompra.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creó la orden de compra";
                return respuesta;
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Orden de compra creada";
            return respuesta;
        }

        public async Task<RespuestaDTO> CrearInsumoOrdenCompra(InsumoXOrdenCompraCreacionDTO objeto) {
            RespuestaDTO respuesta = new RespuestaDTO();

            var editarInsumoXCotizacion = await _insumoXCotizacionService.VerificaEstatusAutorizado(objeto.IdInsumoXCotizacion);
            if (!editarInsumoXCotizacion.Estatus)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se pudo comprar el insumo";
                return respuesta;
            }
            var IOC = new InsumoXOrdenCompraDTO();
            IOC.IdOrdenCompra = objeto.IdOrdenCompra;
            IOC.IdInsumoXCotizacion = objeto.IdInsumoXCotizacion;
            IOC.IdInsumo = objeto.IdInsumo;
            IOC.Cantidad = objeto.Cantidad;
            IOC.PrecioUnitario = objeto.PrecioUnitario;
            IOC.ImporteSinIva = objeto.ImporteSinIva;
            IOC.ImporteConIva = objeto.ImporteConIva;
            IOC.EstatusInsumoOrdenCompra = 1;
            var crearIOC = await _insumoXOrdenCompraService.CrearYObtener(IOC);
            if (crearIOC.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se pudo comprar el insumo";
                return respuesta;
            }
            var ImpuestoIXC = await _impuestoInsumoCotizadoService.ObtenerXIdInsumoCotizado(crearIOC.IdInsumoXCotizacion);
            List<ImpuestoInsumoOrdenCompraCreacionDTO> listaIIOC = new List<ImpuestoInsumoOrdenCompraCreacionDTO>();
            foreach (var IIXC in ImpuestoIXC)
            {
                listaIIOC.Add(new ImpuestoInsumoOrdenCompraCreacionDTO()
                {
                    IdImpuesto = IIXC.IdImpuesto,
                    IdInsumoOrdenCompra = crearIOC.Id,
                    Importe = IIXC.Importe,
                    Porcentaje = IIXC.Porcentaje,
                });
            }
            var crearImpuestos = await _impuestoInsumoOrdenCompraService.CrearLista(listaIIOC);

            var OC = await _ordenCompraService.ObtenXId(crearIOC.IdOrdenCompra);

            var actualizarInsumoXCotizacion = await _insumoXCotizacionService.ActualizaEstatusComprado(crearIOC.IdInsumoXCotizacion);
            if (actualizarInsumoXCotizacion.Estatus)
            {
                await _cotizacionProceso.ActualizarEstatusCotizacion(OC.IdCotizacion);
                await _actualizaRequsicionEstatusInsumos.ActualizaEstatusRequisicionInsumos(OC.IdRequisicion);
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Insumo comprado";
            return respuesta;
        }

        public async Task<RespuestaDTO> CrearOrdenCompra1(OrdenCompraCreacionDTO parametros, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var usuarioNombre = claims.Where(z => z.Type == "username").ToList();
            if (usuarioNombre.Count() <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No esta logueado";
                return respuesta;
            }
            if (string.IsNullOrEmpty(parametros.Chofer) || string.IsNullOrEmpty(parametros.Observaciones) || parametros.ListaInsumosOrdenCompra.Count() <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Capture todos los datos";
                return respuesta;
            }
            if(parametros.ListaInsumosOrdenCompra.Count() <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No hay insumos autorizados";
                return respuesta;
            }
            foreach (var item in parametros.ListaInsumosOrdenCompra) {
                if (item.IdInsumoXCotizacion == 0 || item.IdInsumo == 0
                   || item.Cantidad == 0 || item.PrecioUnitario == 0 || item.ImporteConIva == 0 || item.ImporteSinIva == 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Capture todos los datos";
                    return respuesta;
                }
            }

            OrdenCompraDTO ordenCompraDTO = new OrdenCompraDTO();
            var cotizacion = await _cotizacionService.ObtenXId(parametros.IdCotizacion);
            ordenCompraDTO.IdContratista = cotizacion.IdContratista;
            ordenCompraDTO.IdRequisicion = cotizacion.IdRequisicion;
            ordenCompraDTO.IdProyecto = cotizacion.IdProyecto;
            ordenCompraDTO.IdCotizacion = cotizacion.Id;
            var ordencCompra = await _ordenCompraService.ObtenXIdCotizacion(parametros.IdCotizacion);
            string numero = "";
            if (ordencCompra.Count <= 0)
            {
                numero = cotizacion.NoCotizacion + "_OC_1";
            }
            else
            {
                numero = cotizacion.NoCotizacion + "_OC_" + (ordencCompra.Count() + 1).ToString();
            }
            ordenCompraDTO.NoOrdenCompra = numero;
            ordenCompraDTO.Elaboro = usuarioNombre[0].Value;
            ordenCompraDTO.Estatus = 1;
            ordenCompraDTO.Chofer = parametros.Chofer;
            ordenCompraDTO.Observaciones = parametros.Observaciones;
            ordenCompraDTO.EstatusInsumosSurtidos = 1;
            ordenCompraDTO.FechaRegistro = DateTime.Now;

            var crearOrdenCompra = await _ordenCompraService.CrearYObtener(ordenCompraDTO);
            if (crearOrdenCompra.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creó la orden de compra";
                return respuesta;
            }

            List<InsumoXOrdenCompraDTO> insumosOrdenCompra = new List<InsumoXOrdenCompraDTO>();

            foreach (var item in parametros.ListaInsumosOrdenCompra) {
                int idInsumoXCotizacion = item.IdInsumoXCotizacion;
                var editarInsumoXCotizacion = await _insumoXCotizacionService.VerificaEstatusAutorizado(idInsumoXCotizacion);
                if (!editarInsumoXCotizacion.Estatus) {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo comprar el insumo";
                    return respuesta;
                }
                insumosOrdenCompra.Add(new InsumoXOrdenCompraDTO()
                {
                    IdOrdenCompra = crearOrdenCompra.Id,
                    IdInsumoXCotizacion = item.IdInsumoXCotizacion,
                    IdInsumo = item.IdInsumo,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.PrecioUnitario,
                    ImporteSinIva = item.ImporteSinIva,
                    ImporteConIva = item.ImporteConIva,
                    EstatusInsumoOrdenCompra = 1
                });
            }

            //var resultado = await _insumoXOrdenCompraService.CrearLista(insumosOrdenCompra);
            
            //if (!resultado)
            //{
            //    respuesta.Estatus = false;
            //    respuesta.Descripcion = "Ocurrió un problema";
            //    return respuesta;

            //}
            for (int i = 0; i < insumosOrdenCompra.Count; i++)
            {
                var crearIOC = await _insumoXOrdenCompraService.CrearYObtener(insumosOrdenCompra[i]);
                if (crearIOC.Id > 0) {
                    var ImpuestoIXC = await _impuestoInsumoCotizadoService.ObtenerXIdInsumoCotizado(crearIOC.IdInsumoXCotizacion);
                    List<ImpuestoInsumoOrdenCompraCreacionDTO> listaIIOC = new List<ImpuestoInsumoOrdenCompraCreacionDTO>();
                    foreach (var IIXC in ImpuestoIXC) {
                        listaIIOC.Add(new ImpuestoInsumoOrdenCompraCreacionDTO()
                        {
                            IdImpuesto = IIXC.IdImpuesto,
                            IdInsumoOrdenCompra = crearIOC.Id,
                            Importe = IIXC.Importe,
                            Porcentaje = IIXC.Porcentaje,
                        });
                    }
                    var crearImpuestos = await _impuestoInsumoOrdenCompraService.CrearLista(listaIIOC);
                    if (crearImpuestos) {
                        listaIIOC = new List<ImpuestoInsumoOrdenCompraCreacionDTO>();
                    }
                    var editarInsumoXCotizacion = await _insumoXCotizacionService.ActualizaEstatusComprado(insumosOrdenCompra[i].IdInsumoXCotizacion);
                    if (editarInsumoXCotizacion.Estatus)
                    {
                        await _cotizacionProceso.ActualizarEstatusCotizacion(crearOrdenCompra.IdCotizacion);
                        await _actualizaRequsicionEstatusInsumos.ActualizaEstatusRequisicionInsumos(crearOrdenCompra.IdRequisicion);
                    }
                }
                
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Orden de compra creada";
            return respuesta;
        }


        public async Task<List<OrdenCompraDTO>> OrdenesCompraXIdRequisicion(int idRequisicion)
        {
            List<OrdenCompraDTO> lista = new List<OrdenCompraDTO>();
            var ordenescompra = await _ordenCompraService.ObtenXIdRequisicion(idRequisicion);
            var OCordenDecendente = ordenescompra.OrderBy(x => x.FechaRegistro).Reverse();
            foreach (var oc in OCordenDecendente)
            {
                    string descripcionEstatus = "";
                    switch (oc.EstatusInsumosSurtidos)
                    {
                        case 1:
                            descripcionEstatus = "Pendiente";
                            break;
                        case 2:
                            descripcionEstatus = "Surtida parcialmente";
                            break;
                        case 3:
                            descripcionEstatus = "Surtida totalmente";
                            break;
                        default:
                            descripcionEstatus = "";
                            break;

                    }
                    lista.Add(new OrdenCompraDTO()
                    {
                        Id = oc.Id,
                        NoOrdenCompra = oc.NoOrdenCompra,
                        Chofer = oc.Chofer,
                        Observaciones = oc.Observaciones,
                        FechaRegistro = oc.FechaRegistro,
                        Estatus = oc.Estatus,
                        EstatusInsumosSurtidosDescripcion = descripcionEstatus
                    });
            }
            return lista;
        }

        public async Task<List<OrdenCompraDTO>> OrdenesCompraXIdCotizacion(int idCotizacion)
        {
            List<OrdenCompraDTO> lista = new List<OrdenCompraDTO>();
            var ordenescompra = await _ordenCompraService.ObtenXIdCotizacion(idCotizacion);
            foreach (var oc in ordenescompra)
            {
                string descripcionEstatus = "";
                switch (oc.EstatusInsumosSurtidos)
                {
                    case 1:
                        descripcionEstatus = "Pendiente";
                        break;
                    case 2:
                        descripcionEstatus = "Surtida parcialmente";
                        break;
                    case 3:
                        descripcionEstatus = "Surtida totalmente";
                        break;
                    default:
                        descripcionEstatus = "";
                        break;

                }
                lista.Add(new OrdenCompraDTO()
                {
                    Id = oc.Id,
                    NoOrdenCompra = oc.NoOrdenCompra,
                    Chofer = oc.Chofer,
                    Observaciones = oc.Observaciones,
                    FechaRegistro = oc.FechaRegistro,
                    Estatus = oc.Estatus,
                    EstatusInsumosSurtidosDescripcion = descripcionEstatus
                });
            }
            return lista;
        }

        public async Task<List<InsumoXOrdenCompraDTO>> InsumosOrdenCompraXIdRequisicion(int idRequisicion)
        {
            List<InsumoXOrdenCompraDTO> lista = new List<InsumoXOrdenCompraDTO>();
            var ordenescompra = await _ordenCompraService.ObtenXIdRequisicion(idRequisicion);
            foreach (var oc in ordenescompra)
            {
                var insumosXordenecompra = await _insumoXOrdenCompraService.ObtenTodos();
                var insumosXordenecompraXidOrdenCompra = insumosXordenecompra.Where(z => z.IdOrdenCompra == oc.Id);
                var insumos = await _insumos.ObtenXIdProyecto(oc.IdProyecto);
                foreach (var ioc in insumosXordenecompraXidOrdenCompra)
                {
                    var insumo = insumos.Where(z => z.id == ioc.IdInsumo).ToList();

                    string descripcionEstatus = "";
                    switch (ioc.EstatusInsumoOrdenCompra)
                    {
                        case 1:
                            descripcionEstatus = "Pendiente";
                            break;
                        case 2:
                            descripcionEstatus = "Surtida parcialmente";
                            break;
                        case 3:
                            descripcionEstatus = "Surtida totalmente";
                            break;
                        default:
                            descripcionEstatus = "";
                            break;

                    }
                    lista.Add(new InsumoXOrdenCompraDTO()
                    {
                        Id = ioc.Id,
                        IdOrdenCompra = ioc.IdOrdenCompra,
                        IdInsumoXCotizacion = ioc.IdInsumoXCotizacion,
                        IdInsumo = ioc.IdInsumo,
                        Codigo = insumo[0].Codigo,
                        Descripcion = insumo[0].Descripcion,
                        Unidad = insumo[0].Unidad,
                        Cantidad = ioc.Cantidad,
                        PrecioUnitario = ioc.PrecioUnitario,
                        ImporteSinIva = ioc.ImporteSinIva,
                        ImporteConIva = ioc.ImporteConIva,
                        EstatusInsumoOrdenCompraDescripcion = descripcionEstatus
                    });
                }
            }
            return lista;
        }

        public async Task<List<InsumoXOrdenCompraDTO>> InsumosOrdenCompraXIdCotizacion(int idCotizacion)
        {
            List<InsumoXOrdenCompraDTO> lista = new List<InsumoXOrdenCompraDTO>();
            var ordenescompra = await _ordenCompraService.ObtenXIdCotizacion(idCotizacion);
            foreach (var oc in ordenescompra)
            {
                var insumosXordenecompra = await _insumoXOrdenCompraService.ObtenTodos();
                var insumosXordenecompraXidOrdenCompra = insumosXordenecompra.Where(z => z.IdOrdenCompra == oc.Id);
                var insumos = await _insumos.ObtenXIdProyecto(oc.IdProyecto);
                foreach (var ioc in insumosXordenecompraXidOrdenCompra)
                {
                    var insumo = insumos.Where(z => z.id == ioc.IdInsumo).ToList();
                    string descripcionEstatus = "";
                    switch (ioc.EstatusInsumoOrdenCompra)
                    {
                        case 1:
                            descripcionEstatus = "Pendiente";
                            break;
                        case 2:
                            descripcionEstatus = "Surtida parcialmente";
                            break;
                        case 3:
                            descripcionEstatus = "Surtida totalmente";
                            break;
                        default:
                            descripcionEstatus = "";
                            break;

                    }
                    lista.Add(new InsumoXOrdenCompraDTO()
                    {
                        Id = ioc.Id,
                        IdOrdenCompra = ioc.IdOrdenCompra,
                        IdInsumoXCotizacion = ioc.IdInsumoXCotizacion,
                        IdInsumo = ioc.IdInsumo,
                        Codigo = insumo[0].Codigo,
                        Descripcion = insumo[0].Descripcion,
                        Unidad = insumo[0].Unidad,
                        Cantidad = ioc.Cantidad,
                        PrecioUnitario = ioc.PrecioUnitario,
                        ImporteSinIva = ioc.ImporteSinIva,
                        ImporteConIva = ioc.ImporteConIva,
                        EstatusInsumoOrdenCompraDescripcion = descripcionEstatus
                    });
                }
            }
            return lista;
        }

        public async Task<List<InsumoXOrdenCompraDTO>> InsumosOrdenCompraXIdOrdenCompra(int idOrdenCompra)
        {
            List<InsumoXOrdenCompraDTO> lista = new List<InsumoXOrdenCompraDTO>();
            var ordencompra = await _ordenCompraService.ObtenXId(idOrdenCompra);
                var insumosXordenecompra = await _insumoXOrdenCompraService.ObtenXIdOrdenCompra(idOrdenCompra);
                var insumos = await _insumos.ObtenXIdProyecto(ordencompra.IdProyecto);
                foreach (var ioc in insumosXordenecompra)
                {
                    var insumo = insumos.Where(z => z.id == ioc.IdInsumo).ToList();
                    string descripcionEstatus = "";
                    switch (ioc.EstatusInsumoOrdenCompra)
                    {
                        case 1:
                            descripcionEstatus = "Pendiente";
                            break;
                        case 2:
                            descripcionEstatus = "Surtida parcialmente";
                            break;
                        case 3:
                            descripcionEstatus = "Surtida totalmente";
                            break;
                        default:
                            descripcionEstatus = "";
                            break;

                    }
                    lista.Add(new InsumoXOrdenCompraDTO()
                    {
                        Id = ioc.Id,
                        IdOrdenCompra = ioc.IdOrdenCompra,
                        IdInsumoXCotizacion = ioc.IdInsumoXCotizacion,
                        IdInsumo = ioc.IdInsumo,
                        Codigo = insumo[0].Codigo,
                        Descripcion = insumo[0].Descripcion,
                        Unidad = insumo[0].Unidad,
                        Cantidad = ioc.Cantidad,
                        PrecioUnitario = ioc.PrecioUnitario,
                        ImporteSinIva = ioc.ImporteSinIva,
                        ImporteConIva = ioc.ImporteConIva,
                        EstatusInsumoOrdenCompraDescripcion = descripcionEstatus
                    });
                }
            return lista;
        }
        public async Task<List<InsumoXOrdenCompraDTO>> InsumosXOrdenCompraXIdContratista(int idContratista, int idProyecto) {
            List<InsumoXOrdenCompraDTO> lista =  new List<InsumoXOrdenCompraDTO> ();
            var ordenesCompra = await _ordenCompraService.ObtenXIdContratista(idContratista);
            var ordenespendientes = ordenesCompra.Where(z => z.EstatusInsumosSurtidos != 3 && z.IdProyecto == idProyecto);
            foreach (var item in ordenespendientes) {
                var insumos = await _insumos.ObtenXIdProyecto(item.IdProyecto);
                var insumosxordencompra = await _insumoXOrdenCompraService.ObtenXIdOrdenCompra(item.Id);
                var insumosXOCpendientes = insumosxordencompra.Where(z => z.EstatusInsumoOrdenCompra != 3);
                foreach(var ixoc in insumosXOCpendientes)
                {
                    var insumo = insumos.Where(z => z.id == ixoc.IdInsumo).ToList();
                    string descripcionEstatus = "";
                    switch (ixoc.EstatusInsumoOrdenCompra)
                    {
                        case 1:
                            descripcionEstatus = "Pendiente";
                            break;
                        case 2:
                            descripcionEstatus = "Surtida parcialmente";
                            break;
                        case 3:
                            descripcionEstatus = "Surtida totalmente";
                            break;
                        default:
                            descripcionEstatus = "";
                            break;

                    }
                    lista.Add(new InsumoXOrdenCompraDTO()   
                    { 
                        Id = ixoc.Id,
                        IdOrdenCompra = Convert.ToInt32(ixoc.IdOrdenCompra),
                        IdInsumoXCotizacion = Convert.ToInt32(ixoc.IdInsumoXCotizacion),
                        IdInsumo = Convert.ToInt32(ixoc.IdInsumo),
                        Codigo = insumo[0].Codigo,
                        Descripcion = insumo[0].Descripcion,
                        Unidad = insumo[0].Unidad,
                        Cantidad = ixoc.Cantidad - ixoc.CantidadRecibida,
                        CantidadRecibida = ixoc.CantidadRecibida,
                        PrecioUnitario = Convert.ToInt32(ixoc.PrecioUnitario),
                        ImporteSinIva = Convert.ToInt32(ixoc.ImporteSinIva),
                        ImporteConIva = Convert.ToInt32(ixoc.ImporteConIva),
                        EstatusInsumoOrdenCompraDescripcion = descripcionEstatus,
                        NoOrdenCompra = item.NoOrdenCompra
                    });
                }
            
            }
            return lista;
        }

        public async Task ActualizaOrdenCompraSurtidos(int idOrdenCompra)
        {
            var insumosXOrdenCompra = await _insumoXOrdenCompraService.ObtenXIdOrdenCompra(idOrdenCompra);
            var insumosPendientes = insumosXOrdenCompra.Where(z => z.EstatusInsumoOrdenCompra == 1).Count();
            var insumosSurtidosParcialmente = insumosXOrdenCompra.Where(z => z.EstatusInsumoOrdenCompra == 2).Count();
            var insumosSurtidosTotalmente = insumosXOrdenCompra.Where(z => z.EstatusInsumoOrdenCompra == 3).Count();

            if (insumosPendientes > 0)
            {
                var actualizaEstatusOrdenCompra = await _ordenCompraService.ActualizaOrdenCompraSurtidos(idOrdenCompra, 1);
            }
            if (insumosSurtidosTotalmente == insumosXOrdenCompra.Count)
            {
                var actualizaEstatusOrdenCompra = await _ordenCompraService.ActualizaOrdenCompraSurtidos(idOrdenCompra, 3);
            }
            if (insumosSurtidosParcialmente > 0)
            {
                var actualizaEstatusOrdenCompra = await _ordenCompraService.ActualizaOrdenCompraSurtidos(idOrdenCompra, 2);
            }
        }

        public async Task<List<ImpuestoInsumoOrdenCompraDTO>> ObtenerImpuestosInsumoOrdenCompra(int idInsumoXOrdenCompra) {
            var ImpuestosInsumosOC = await _impuestoInsumoOrdenCompraService.ObtenerXIdInsumoXOrdenCompra(idInsumoXOrdenCompra);
            if(ImpuestosInsumosOC.Count() <= 0)
            {
                return new List<ImpuestoInsumoOrdenCompraDTO>();
            }
            var impuestos = await _tipoImpuestoService.ObtenTodos();
            foreach (var IIOC in ImpuestosInsumosOC) {
                var impuesto = impuestos.Where(z => z.Id == IIOC.IdImpuesto).ToList();
                IIOC.DescripcionImpuesto = impuesto[0].DescripcionImpuesto;
            }
            return ImpuestosInsumosOC;
        }
    }
}
