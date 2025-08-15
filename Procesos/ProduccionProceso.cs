using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class ProduccionProceso<TContext> where TContext: DbContext
    {
        private readonly IProduccionService<TContext> _produccionService;
        private readonly IInsumoXProduccionService<TContext> _insumoService;

        public ProduccionProceso(IProduccionService<TContext> produccionService, IInsumoXProduccionService<TContext> insumoService)
        {
            _produccionService = produccionService;
            _insumoService = insumoService;
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

        public async Task<RespuestaDTO> Editar(ProduccionDTO produccion)
        {
            try
            {
                var respuesta = await _produccionService.Editar(produccion);
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
