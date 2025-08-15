using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class EntradaProduccionAlmacenProceso<TContext> where TContext: DbContext
    {
        private readonly IEntradaProduccionAlmacenService<TContext> _entradaProdAlService;

        private readonly IProductoXEntradaProduccionAlmacenService<TContext> _detallesServixe;

        private readonly IExistenciaProductoAlmacenService<TContext> _existenciaProdAlService;

        public EntradaProduccionAlmacenProceso(IEntradaProduccionAlmacenService<TContext> entradaProdAlService,
            IExistenciaProductoAlmacenService<TContext> existenciaProdAlService,
            IProductoXEntradaProduccionAlmacenService<TContext> detalleService
            )
        {
            _entradaProdAlService = entradaProdAlService;
            _existenciaProdAlService = existenciaProdAlService;
            _detallesServixe = detalleService;
        }

        public async Task<RespuestaDTO> Crear(EntradaProduccionAlmacenDTO parametro)
        {
            try
            {
                var resultado = await _entradaProdAlService.Crear(parametro);
                if (resultado.Estatus)
                {
                    foreach(var detalle in parametro.Detalles)
                    {
                        var resultDet = await _detallesServixe.Crear(detalle);
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
                        var resultDet = await _detallesServixe.Crear(detalle);
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
