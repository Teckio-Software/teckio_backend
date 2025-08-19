using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class ProduccionProceso<TContext> where TContext: DbContext
    {
        private readonly IProduccionService<TContext> _produccionService;
        private readonly IInsumoXProduccionService<TContext> _insumoService;
        private readonly ExistenciasProceso<TContext> _existenciasProceso;
        private readonly IInsumoXProductoYServicioService<TContext> _insumoXProductoService;
        private readonly IRequisicionService<TContext> _requisicionService;
        private readonly IInsumoXRequisicionService<TContext> _insumoXRequisicionService;

        private readonly IMapper _mapper;

        public ProduccionProceso(IProduccionService<TContext> produccionService,
            IInsumoXProduccionService<TContext> insumoService, 
            ExistenciasProceso<TContext> existenciasProceso,
            IInsumoXProductoYServicioService<TContext> insumoXProductoService,
            IMapper mapper,
            IRequisicionService<TContext> requisicionService,
            IInsumoXRequisicionService<TContext> insumoXRequisicionService)
        {
            _produccionService = produccionService;
            _insumoService = insumoService;
            _existenciasProceso = existenciasProceso;
            _insumoXProductoService = insumoXProductoService;
            _mapper = mapper;
            _requisicionService = requisicionService;
            _insumoXRequisicionService = insumoXRequisicionService;
        }

        public async Task<RespuestaDTO> Crear(ProduccionDTO produccion)
        {
            try
            {
                produccion.Estatus = 0;
                var respuesta = await _produccionService.Crear(produccion);
                return respuesta;
            }
            catch
            {
                return new RespuestaDTO
                {
                    Descripcion = "Ocurrió un error al itentar crear la producción",
                    Estatus = false
                };
            }
        }

        public async Task<ProduccionDTO> CrearYObtener(ProduccionDTO produccion)
        {
            try
            {
                produccion.Estatus = 0;
                var respuesta = await _produccionService.CrearYObtener(produccion);
                return respuesta;
            }
            catch
            {
                return new ProduccionDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(ProduccionConAlmacenDTO produccion, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var usuarioNombre = claims.Where(z => z.Type == "username").ToList();
                var objeto = _mapper.Map<ProduccionDTO>(produccion);
                if (objeto.Estatus == 3)
                {
                    var existencias = await _existenciasProceso.obtenInsumosExistentes(produccion.IdAlmacen);
                    if (existencias.Count <= 0)
                    {
                        return new RespuestaDTO
                        {
                            Estatus = false,
                            Descripcion = "No hay existencias disponibles en el almacen"
                        };
                    }
                    var insumos = await _insumoXProductoService.ObtenerPorIdPrdYSer(produccion.IdProductoYservicio);
                    bool crear = true;
                    RequisicionDTO requisicion = new RequisicionDTO();
                    List<InsumoXRequisicionDTO> insumosXRequisicion = new List<InsumoXRequisicionDTO>();
                    foreach (var insumo in insumos)
                    {
                        var existencia = existencias.Find(e => e.IdInsumo == insumo.IdInsumo);
                        if (existencia == null)
                        {
                            crear = false;
                            //Crea una requisición, en este caso el insumo no se encuentra en el almacen.
                            insumosXRequisicion.Add(new InsumoXRequisicionDTO
                            {
                                IdInsumo = insumo.IdInsumo,
                                Denominacion = 0,
                                Cantidad = insumo.Cantidad,
                                CantidadComprada = 0,
                                CantidadEnAlmacen = 0,
                                EstatusInsumoRequisicion = 0,
                                EstatusInsumoComprado = 0,
                                EstatusInsumoSurtido = 0,
                                PersonaIniciales = usuarioNombre[0].Value,
                                Observaciones = ""
                            });
                        }
                        else
                        {
                            if (existencia.CantidadInsumos < insumo.Cantidad)
                            {
                                crear = false;
                                //Crea una requisición, en este caso no hay suficiente cantidad del insumo en el almacen.
                                insumosXRequisicion.Add(new InsumoXRequisicionDTO
                                {
                                    IdInsumo = insumo.IdInsumo,
                                    Denominacion = 0,
                                    Cantidad = insumo.Cantidad - existencia.CantidadInsumos,
                                    CantidadComprada = 0,
                                    CantidadEnAlmacen = 0,
                                    EstatusInsumoRequisicion = 0,
                                    EstatusInsumoComprado = 0,
                                    EstatusInsumoSurtido = 0,
                                    PersonaIniciales = usuarioNombre[0].Value,
                                    Observaciones = ""
                                });

                            }
                        }
                        
                    }
                    if (!crear)
                    {
                        requisicion.PersonaSolicitante = usuarioNombre[0].Value;
                        requisicion.IdProduccion = produccion.Id;
                        requisicion.EstatusRequisicion = 0;
                        requisicion.NoRequisicion = "Material faltante para producción";

                        var resultReq = await _requisicionService.CrearYObtener(requisicion);
                        if (resultReq.Id > 0)
                        {
                            var incrementable = 1;
                            foreach(var insReq in insumosXRequisicion)
                            {
                                insReq.IdRequisicion = resultReq.Id;
                                insReq.Folio = incrementable.ToString();
                                incrementable++;
                            }
                            var resultInsums = await _insumoXRequisicionService.CrearLista(insumosXRequisicion);
                            if (resultInsums)
                            {
                                return new RespuestaDTO
                                {
                                    Estatus = false,
                                    Descripcion = "No hay existencias suficientes para algunos insumos en el almacen, se crearón las requisiciones necesarias"
                                };
                            }
                        }


                        return new RespuestaDTO
                        {
                            Estatus = false,
                            Descripcion = "No hay existencias suficientes para algunos insumos en el almacen"
                        };
                    }
                    respuesta = await _produccionService.Editar(objeto);
                    return respuesta;
                }
                respuesta = await _produccionService.Editar(objeto);
                return respuesta;
            }
            catch
            {
                return new RespuestaDTO
                {
                    Descripcion = "Ocurrió un error al itentar editar la producción",
                    Estatus = false
                };
            }
        }

        //public async Task<RespuestaDTO> PasarAEnproduccion(ProduccionConAlmacenDTO produccionConAlmacen)
        //{
        //    try
        //    {
        //        ProduccionDTO produccion = new ProduccionDTO
        //        {
        //            Id = produccionConAlmacen.Id,
        //            IdProductoYservicio = produccionConAlmacen.IdProductoYservicio,
        //            FechaProduccion = produccionConAlmacen.FechaProduccion,
        //            Produjo = produccionConAlmacen.Produjo,
        //            Cantidad = produccionConAlmacen.Cantidad,
        //            Observaciones = produccionConAlmacen.Observaciones,
        //            Estatus = 3,
        //            Autorizo = produccionConAlmacen.Autorizo
        //        };
        //        var existencias = await _existenciasProceso.obtenInsumosExistentes(produccionConAlmacen.IdAlmacen);
        //        if (existencias.Count <= 0)
        //        {
        //            return new RespuestaDTO
        //            {
        //                Estatus = false,
        //                Descripcion = "No hay existencias disponibles en el almacen"
        //            };
        //        }
        //        var insumos = await _insumoXProductoService.ObtenerPorIdPrdYSer(produccionConAlmacen.IdProductoYservicio);
        //        foreach(var insumo in insumos)
        //        {
        //            var existencia = existencias.Find(e=>e.IdInsumo==insumo.IdInsumo);
        //            if (existencia == null)
        //            {
        //                return new RespuestaDTO
        //                {
        //                    Estatus = false,
        //                    Descripcion = "No hay existencias para algunos insumos en el almacen"
        //                };
        //            }
        //            if (existencia.CantidadInsumos < insumo.Cantidad)
        //            {
        //                return new RespuestaDTO
        //                {
        //                    Estatus = false,
        //                    Descripcion = "No hay existencias suficientes para algunos insumos en el almacen"
        //                };
        //            }
        //        }
        //        var respuesta = await _produccionService.Editar(produccion);
        //        return respuesta;
        //    }
        //    catch
        //    {
        //        return new RespuestaDTO
        //        {
        //            Descripcion = "Ocurrió un error al itentar editar la producción",
        //            Estatus = false
        //        };
        //    }
        //}

        public async Task<RespuestaDTO> Eliminar(int produccion)
        {
            try
            {
                var insumos = await _insumoService.ObtenerXProduccion(produccion);
                foreach(var insumo in insumos)
                {
                    await _insumoService.Eliminar(insumo.Id);
                }
                var respuesta = await _produccionService.Eliminar(produccion);
                return respuesta;
            }
            catch
            {
                return new RespuestaDTO
                {
                    Descripcion = "Ocurrió un error al itentar eliminar la producción",
                    Estatus = false
                };
            }
        }
    }
}
