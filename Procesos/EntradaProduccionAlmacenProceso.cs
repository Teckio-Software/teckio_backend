using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class EntradaProduccionAlmacenProceso<TContext> where TContext: DbContext
    {
        private readonly IEntradaProduccionAlmacenService<TContext> _entradaProdAlService;

        private readonly IProductoXEntradaProduccionAlmacenService<TContext> _detallesService;

        private readonly IExistenciaProductoAlmacenService<TContext> _existenciaProdAlService;

        public EntradaProduccionAlmacenProceso(IEntradaProduccionAlmacenService<TContext> entradaProdAlService,
            IExistenciaProductoAlmacenService<TContext> existenciaProdAlService,
            IProductoXEntradaProduccionAlmacenService<TContext> detalleService
            )
        {
            _entradaProdAlService = entradaProdAlService;
            _existenciaProdAlService = existenciaProdAlService;
            _detallesService = detalleService;
        }

        public async Task<RespuestaDTO> Crear(EntradaProduccionAlmacenDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var resultado = await _entradaProdAlService.CrearYObtener(parametro);
                if (resultado.Id > 0)
                {
                    foreach(var detalle in parametro.Detalles)
                    {
                        detalle.IdEntradaProduccionAlmacen = resultado.Id;
                        var resultDet = await _detallesService.Crear(detalle);
                        if (resultDet.Estatus)
                        {
                            ExistenciaProductoAlmacenDTO existencia = new ExistenciaProductoAlmacenDTO
                            {
                                IdProductoYservicio = detalle.IdProductoYservicio,
                                IdAlmacen = parametro.IdAlmacen,
                                Cantidad = detalle.Cantidad
                            };
                            var resultExist = await _existenciaProdAlService.Crear(existencia);
                        }
                    }
                }
                if(resultado.Id > 0)
                {
                    respuesta.Descripcion = "Entrada producción creada exitosamente";
                    respuesta.Estatus = true;
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al intentar crear la entrada producción";
                    respuesta.Estatus = false;
                }
                return respuesta;
            }
            catch
            {
                return new RespuestaDTO
                {
                    Descripcion = "Ocurrio un error al intentar crear la entrada produccion almacen",
                    Estatus = false
                };
            }
        }

        public async Task<EntradaProduccionAlmacenDTO> CrearYObtener(EntradaProduccionAlmacenDTO parametro)
        {
            try
            {
                var resultado = await _entradaProdAlService.CrearYObtener(parametro);
                if (resultado.Id>0)
                {
                    foreach (var detalle in parametro.Detalles)
                    {
                        var resultDet = await _detallesService.Crear(detalle);
                        if (resultDet.Estatus)
                        {
                            ExistenciaProductoAlmacenDTO existencia = new ExistenciaProductoAlmacenDTO
                            {
                                IdProductoYservicio = detalle.IdProductoYservicio,
                                IdAlmacen = parametro.IdAlmacen,
                                Cantidad = detalle.Cantidad
                            };
                            var resultExist = await _existenciaProdAlService.Crear(existencia);
                        }
                    }
                }
                return resultado;
            }
            catch
            {
                return new EntradaProduccionAlmacenDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(EntradaProduccionAlmacenDTO parametro)
        {
            try
            {
                var resultado = await _entradaProdAlService.Editar(parametro);
                return resultado;
            }
            catch
            {
                return new RespuestaDTO
                {
                    Descripcion = "Ocurrio un error al intentar editar la entrada produccion almacen",
                    Estatus = false
                };
            }
        }

        public async Task<RespuestaDTO> Eliminar(int parametro)
        {
            try
            {
                var objeto = new EntradaProduccionAlmacenDTO
                {
                    Id = parametro
                };
                var detalles = await _detallesService.ObtenerXIdEntrada(objeto.Id);
                foreach(var detalle in detalles)
                {
                    var existencia = await _existenciaProdAlService.ObtenerExistencia(objeto.IdAlmacen, detalle.IdProductoYservicio);
                    existencia.Cantidad -= detalle.Cantidad;
                    var resultExist = await _existenciaProdAlService.Editar(existencia);
                    if (resultExist.Estatus)
                    {
                        var resultDet = await _detallesService.Eliminar(detalle);
                        if (!resultDet.Estatus)
                        {
                            return new RespuestaDTO
                            {
                                Descripcion = "Ocurrio un error al intentar eliminar la entrada produccion almacen, no se pudo eliminar el detalle",
                                Estatus = false
                            };
                        }

                    }
                    else
                    {
                        return new RespuestaDTO
                        {
                            Descripcion = "Ocurrio un error al intentar eliminar la entrada produccion almacen, no se pudo actualizar la existencia",
                            Estatus = false
                        };
                    }
                }
                var resultado = await _entradaProdAlService.Eliminar(objeto);
                return resultado;
            }
            catch
            {
                return new RespuestaDTO
                {
                    Descripcion = "Ocurrio un error al intentar eliminar la entrada produccion almacen",
                    Estatus = false
                };
            }
        }
    }
}
