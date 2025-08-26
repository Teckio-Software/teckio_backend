using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
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
        private readonly IExistenciaProductoAlmacenService<TContext> _existenciaProductoAlmacenService;
        private readonly IProductoXEntradaProduccionAlmacenService<TContext> _productoXEntradaProduccionAlmacenService;
        private readonly EntradaProduccionAlmacenProceso<TContext> _entradaProduccionAlmacenProceso;
        private readonly IInsumoXProduccionService<TContext> _insumoXProduccionService;
        private readonly IInsumoXAlmacenSalidaService<TContext> _insumoXAlmacenSalidaService;
        private readonly IAlmacenExistenciaInsumoService<TContext> _insumoExistenciaService;

        private readonly IMapper _mapper;

        public ProduccionProceso(IProduccionService<TContext> produccionService,
            IInsumoXProduccionService<TContext> insumoService, 
            ExistenciasProceso<TContext> existenciasProceso,
            IInsumoXProductoYServicioService<TContext> insumoXProductoService,
            IMapper mapper,
            IRequisicionService<TContext> requisicionService,
            IInsumoXRequisicionService<TContext> insumoXRequisicionService,
            IExistenciaProductoAlmacenService<TContext> existenciaProductoAlmacenService,
            IProductoXEntradaProduccionAlmacenService<TContext> productoXEntradaProduccionAlmacenService,
            EntradaProduccionAlmacenProceso<TContext> entradaProduccionAlmacenProceso,
            IInsumoXProduccionService<TContext> insumoXProduccionService,
            IInsumoXAlmacenSalidaService<TContext> insumoXAlmacenSalidaService,
            IAlmacenExistenciaInsumoService<TContext> insumoExistenciaService)
        {
            _produccionService = produccionService;
            _insumoService = insumoService;
            _existenciasProceso = existenciasProceso;
            _insumoXProductoService = insumoXProductoService;
            _mapper = mapper;
            _requisicionService = requisicionService;
            _insumoXRequisicionService = insumoXRequisicionService;
            _existenciaProductoAlmacenService = existenciaProductoAlmacenService;
            _productoXEntradaProduccionAlmacenService = productoXEntradaProduccionAlmacenService;
            _entradaProduccionAlmacenProceso = entradaProduccionAlmacenProceso;
            _insumoXProduccionService = insumoXProduccionService;
            _insumoXAlmacenSalidaService = insumoXAlmacenSalidaService;
            _insumoExistenciaService = insumoExistenciaService;
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

        public async Task<RespuestaDTO> Editar(ProduccionConAlmacenDTO produccion)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                //var usuarioNombre = claims.Where(z => z.Type == "username").ToList();
                var objeto = _mapper.Map<ProduccionDTO>(produccion);
                //if (objeto.Estatus == 3)
                //{
                //    var existencias = await _existenciasProceso.obtenInsumosExistentes(produccion.IdAlmacen);
                //    if (existencias.Count <= 0)
                //    {
                //        return new RespuestaDTO
                //        {
                //            Estatus = false,
                //            Descripcion = "No hay existencias disponibles en el almacen"
                //        };
                //    }
                //    var insumos = await _insumoXProduccionService.ObtenerXProduccion(produccion.Id);
                //    bool crear = true;
                //    RequisicionDTO requisicion = new RequisicionDTO();
                //    List<InsumoXRequisicionDTO> insumosXRequisicion = new List<InsumoXRequisicionDTO>();
                //    foreach (var insumo in insumos)
                //    {
                //        var existencia = existencias.Find(e => e.IdInsumo == insumo.IdInsumo);
                //        if (existencia == null)
                //        {
                //            crear = false;
                //            //Crea una requisición, en este caso el insumo no se encuentra en el almacen.
                //            insumosXRequisicion.Add(new InsumoXRequisicionDTO
                //            {
                //                IdInsumo = insumo.IdInsumo,
                //                Denominacion = 0,
                //                Cantidad = insumo.Cantidad,
                //                CantidadComprada = 0,
                //                CantidadEnAlmacen = 0,
                //                EstatusInsumoRequisicion = 0,
                //                EstatusInsumoComprado = 0,
                //                EstatusInsumoSurtido = 0,
                //                PersonaIniciales = usuarioNombre[0].Value,
                //                Observaciones = ""
                //            });
                //        }
                //        else
                //        {
                //            if (existencia.CantidadInsumos < insumo.Cantidad)
                //            {
                //                crear = false;
                //                //Crea una requisición, en este caso no hay suficiente cantidad del insumo en el almacen.
                //                insumosXRequisicion.Add(new InsumoXRequisicionDTO
                //                {
                //                    IdInsumo = insumo.IdInsumo,
                //                    Denominacion = 0,
                //                    Cantidad = insumo.Cantidad - existencia.CantidadInsumos,
                //                    CantidadComprada = 0,
                //                    CantidadEnAlmacen = 0,
                //                    EstatusInsumoRequisicion = 0,
                //                    EstatusInsumoComprado = 0,
                //                    EstatusInsumoSurtido = 0,
                //                    PersonaIniciales = usuarioNombre[0].Value,
                //                    Observaciones = ""
                //                });

                //            }
                //        }

                //    }
                //    if (!crear)
                //    {
                //        requisicion.PersonaSolicitante = usuarioNombre[0].Value;
                //        requisicion.IdProduccion = produccion.Id;
                //        requisicion.EstatusRequisicion = 0;
                //        requisicion.NoRequisicion = "Material faltante para producción";

                //        var resultReq = await _requisicionService.CrearYObtener(requisicion);
                //        if (resultReq.Id > 0)
                //        {
                //            var incrementable = 1;
                //            foreach(var insReq in insumosXRequisicion)
                //            {
                //                insReq.IdRequisicion = resultReq.Id;
                //                insReq.Folio = incrementable.ToString();
                //                incrementable++;
                //            }
                //            var resultInsums = await _insumoXRequisicionService.CrearLista(insumosXRequisicion);
                //            if (resultInsums)
                //            {
                //                return new RespuestaDTO
                //                {
                //                    Estatus = false,
                //                    Descripcion = "No hay existencias suficientes para algunos insumos en el almacen, se crearón las requisiciones necesarias"
                //                };
                //            }
                //        }
                //        return new RespuestaDTO
                //        {
                //            Estatus = false,
                //            Descripcion = "No hay existencias suficientes para algunos insumos en el almacen"
                //        };
                //    }
                //    respuesta = await _produccionService.Editar(objeto);
                //    return respuesta;
                //}
                //if(objeto.Estatus == 4)
                //{
                //    var insumos = await _insumoXProduccionService.ObtenerXProduccion(produccion.Id);
                //    if (insumos.Count <= 0)
                //    {
                //        respuesta.Estatus = false;
                //        respuesta.Descripcion = "No se encontrarón los insumos relacionados a la producción";
                //        return respuesta;
                //    }
                //    var existencias = await _existenciasProceso.obtenInsumosExistentes(produccion.IdAlmacen);
                //    if (existencias.Count <= 0)
                //    {
                //        respuesta.Estatus = false;
                //        respuesta.Descripcion = "No hay existencias disponibles en el almacen";
                //        return respuesta;
                //    }

                //    foreach (var insumo in insumos)
                //    {
                //        var existencia = existencias.Find(e => e.IdInsumo == insumo.IdInsumo);
                //        if (existencia == null)
                //        {
                //            respuesta.Estatus = false;
                //            respuesta.Descripcion = "No hay existencias para algunos insumos en el almacen";
                //            return respuesta;
                //        }
                //        if (existencia.CantidadInsumos < insumo.Cantidad)
                //        {
                //            respuesta.Estatus = false;
                //            respuesta.Descripcion = "No hay existencias suficientes para algunos insumos en el almacen";
                //            return respuesta;
                //        }

                //        //Crea la salida
                //        AlmacenSalidaInsumoDTO salida = new AlmacenSalidaInsumoDTO
                //        {
                //            IdInsumo = insumo.IdInsumo,
                //            CantidadPorSalir = insumo.Cantidad,
                //            //IdProyecto = null,
                //            EsPrestamo = false
                //        };

                //        var resultSalida = await _insumoXAlmacenSalidaService.CrearYObtener(salida);

                //        //Si la salida no se creo no hace nada
                //        if (resultSalida.Id<=0)
                //        {
                //            respuesta.Estatus = false;
                //            respuesta.Descripcion = "Ocurrió un error al crear la salida del insumo";
                //            return respuesta;
                //        }

                //        //Edita la existencia del insumo, si falla se elimina la salida y se regresa un error
                //        existencia.CantidadInsumos -= insumo.Cantidad;

                //        var resultExistSal = await _insumoExistenciaService.ActualizaExistenciaInsumoSalida(existencia.Id, existencia.CantidadInsumos);

                //        if (!resultExistSal.Estatus)
                //        {
                //            //Aqui se elimina la salida
                //            await _insumoXAlmacenSalidaService.Cancelar(resultSalida.Id);
                //            respuesta.Estatus = false;
                //            respuesta.Descripcion = "Ocurrió un error al intentar editar la existencia del insumo";
                //            return respuesta;
                //        }

                //    }
                //}
                //if (objeto.Estatus == 5)
                //{
                //    EntradaProduccionAlmacenDTO entradaProductAlm = new EntradaProduccionAlmacenDTO
                //    {
                //        FechaEntrada = DateTime.Now,
                //        IdAlmacen = produccion.IdAlmacen,
                //        Observaciones = "",
                //        Detalles = new List<ProductosXEntradaProduccionAlmacenDTO>()
                //    };

                //    var resultEntrada = await _entradaProduccionAlmacenProceso.CrearYObtener(entradaProductAlm);
                //    if(resultEntrada.Id <= 0)
                //    {
                //        return new RespuestaDTO
                //        {
                //            Estatus = false,
                //            Descripcion = "Ocurrió un error al crear la entrada de producción al almacén"
                //        };
                //    }

                //    var existencia = await _existenciaProductoAlmacenService.ObtenerExistencia(produccion.IdProductoYservicio, produccion.IdAlmacen);

                //    if (existencia.Id > 0)
                //    {
                //        existencia.Cantidad = existencia.Cantidad + produccion.Cantidad;
                //        var resultExist = await _existenciaProductoAlmacenService.Editar(existencia);
                //        if (!resultExist.Estatus)
                //        {
                //            await _entradaProduccionAlmacenProceso.Eliminar(resultEntrada.Id);
                //            return new RespuestaDTO
                //            {
                //                Estatus = false,
                //                Descripcion = "Ocurrió un error al editar la existencia del producto en el almacén"
                //            };
                //        }
                //    }
                //    else
                //    {
                //        ExistenciaProductoAlmacenDTO existenciaProductoAlmacen = new ExistenciaProductoAlmacenDTO
                //        {
                //            IdProductoYservicio = produccion.IdProductoYservicio,
                //            IdAlmacen = produccion.IdAlmacen,
                //            Cantidad = produccion.Cantidad
                //        };
                //        var resultExist = await _existenciaProductoAlmacenService.Crear(existenciaProductoAlmacen);
                //        if (!resultExist.Estatus)
                //        {
                //            await _entradaProduccionAlmacenProceso.Eliminar(resultEntrada.Id);
                //            return new RespuestaDTO
                //            {
                //                Estatus = false,
                //                Descripcion = "Ocurrió un error al editar la existencia del producto en el almacén"
                //            };
                //        }
                //    }

                //}
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

        public async Task<RespuestaDTO> CambiarEstatus(ProduccionConAlmacenDTO produccionConAlmacen, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var produccion = await _produccionService.ObtenerXId(produccionConAlmacen.Id);
                if(produccion.Id <= 0)
                {
                    return new RespuestaDTO
                    {
                        Descripcion = "No se encontró la producción",
                        Estatus = false
                    };
                }
                produccion.Estatus = produccionConAlmacen.Estatus;
                var usuarioNombre = claims.Where(z => z.Type == "username").ToList();
                if (produccion.Estatus == 3)
                {
                    var existencias = await _existenciasProceso.obtenInsumosExistentes(produccionConAlmacen.IdAlmacen);
                    if (existencias.Count <= 0)
                    {
                        return new RespuestaDTO
                        {
                            Estatus = false,
                            Descripcion = "No hay existencias disponibles en el almacen"
                        };
                    }
                    var insumos = await _insumoXProduccionService.ObtenerXProduccion(produccion.Id);
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
                            foreach (var insReq in insumosXRequisicion)
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
                    respuesta = await _produccionService.Editar(produccion);
                    return respuesta;
                }
                if (produccion.Estatus == 4)
                {
                    var insumos = await _insumoXProduccionService.ObtenerXProduccion(produccion.Id);
                    if (insumos.Count <= 0)
                    {
                        respuesta.Estatus = false;
                        respuesta.Descripcion = "No se encontrarón los insumos relacionados a la producción";
                        return respuesta;
                    }
                    var existencias = await _existenciasProceso.obtenInsumosExistentes(produccionConAlmacen.IdAlmacen);
                    if (existencias.Count <= 0)
                    {
                        respuesta.Estatus = false;
                        respuesta.Descripcion = "No hay existencias disponibles en el almacen";
                        return respuesta;
                    }

                    foreach (var insumo in insumos)
                    {
                        var existencia = existencias.Find(e => e.IdInsumo == insumo.IdInsumo);
                        if (existencia == null)
                        {
                            respuesta.Estatus = false;
                            respuesta.Descripcion = "No hay existencias para algunos insumos en el almacen";
                            return respuesta;
                        }
                        if (existencia.CantidadInsumos < insumo.Cantidad)
                        {
                            respuesta.Estatus = false;
                            respuesta.Descripcion = "No hay existencias suficientes para algunos insumos en el almacen";
                            return respuesta;
                        }

                        //Crea la salida
                        AlmacenSalidaInsumoDTO salida = new AlmacenSalidaInsumoDTO
                        {
                            IdInsumo = insumo.IdInsumo,
                            CantidadPorSalir = insumo.Cantidad,
                            IdProyecto = null,
                            EsPrestamo = false,
                            IdAlmacenSalida = produccionConAlmacen.IdAlmacen,
                            EstatusInsumo = 1, // Asumiendo que 1 es el estatus de capturado
                            PrestamoFinalizado = false,

                        };

                        var resultSalida = await _insumoXAlmacenSalidaService.CrearYObtener(salida);

                        //Si la salida no se creo no hace nada
                        if (resultSalida.Id <= 0)
                        {
                            respuesta.Estatus = false;
                            respuesta.Descripcion = "Ocurrió un error al crear la salida del insumo";
                            return respuesta;
                        }

                        //Edita la existencia del insumo, si falla se elimina la salida y se regresa un error
                        existencia.CantidadInsumos -= insumo.Cantidad;

                        var resultExistSal = await _insumoExistenciaService.ActualizaExistenciaInsumoSalida(existencia.Id, existencia.CantidadInsumos);

                        if (!resultExistSal.Estatus)
                        {
                            //Aqui se elimina la salida
                            await _insumoXAlmacenSalidaService.Cancelar(resultSalida.Id);
                            respuesta.Estatus = false;
                            respuesta.Descripcion = "Ocurrió un error al intentar editar la existencia del insumo";
                            return respuesta;
                        }

                    }
                }
                if (produccion.Estatus == 5)
                {
                    EntradaProduccionAlmacenDTO entradaProductAlm = new EntradaProduccionAlmacenDTO
                    {
                        FechaEntrada = DateTime.Now,
                        IdAlmacen = produccionConAlmacen.IdAlmacen,
                        Observaciones = "",
                        Detalles = new List<ProductosXEntradaProduccionAlmacenDTO>()
                    };

                    var resultEntrada = await _entradaProduccionAlmacenProceso.CrearYObtener(entradaProductAlm);
                    if (resultEntrada.Id <= 0)
                    {
                        return new RespuestaDTO
                        {
                            Estatus = false,
                            Descripcion = "Ocurrió un error al crear la entrada de producción al almacén"
                        };
                    }

                    var existencia = await _existenciaProductoAlmacenService.ObtenerExistencia(produccion.IdProductoYservicio, produccionConAlmacen.IdAlmacen);

                    if (existencia.Id > 0)
                    {
                        existencia.Cantidad = existencia.Cantidad + produccion.Cantidad;
                        var resultExist = await _existenciaProductoAlmacenService.Editar(existencia);
                        if (!resultExist.Estatus)
                        {
                            await _entradaProduccionAlmacenProceso.Eliminar(resultEntrada.Id);
                            return new RespuestaDTO
                            {
                                Estatus = false,
                                Descripcion = "Ocurrió un error al editar la existencia del producto en el almacén"
                            };
                        }
                    }
                    else
                    {
                        ExistenciaProductoAlmacenDTO existenciaProductoAlmacen = new ExistenciaProductoAlmacenDTO
                        {
                            IdProductoYservicio = produccion.IdProductoYservicio,
                            IdAlmacen = produccionConAlmacen.IdAlmacen,
                            Cantidad = produccion.Cantidad
                        };
                        var resultExist = await _existenciaProductoAlmacenService.Crear(existenciaProductoAlmacen);
                        if (!resultExist.Estatus)
                        {
                            await _entradaProduccionAlmacenProceso.Eliminar(resultEntrada.Id);
                            return new RespuestaDTO
                            {
                                Estatus = false,
                                Descripcion = "Ocurrió un error al editar la existencia del producto en el almacén"
                            };
                        }
                    }


                }
                respuesta = await _produccionService.Editar(produccion);
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
