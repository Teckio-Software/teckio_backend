using DocumentFormat.OpenXml.Office2016.Drawing.Charts;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class InsumoXProduccionProceso<TContext> where TContext : DbContext
    {
        private readonly IInsumoXProduccionService<TContext> _insumoService;
        private readonly IProduccionService<TContext> _produccionService;

        public InsumoXProduccionProceso(IInsumoXProduccionService<TContext> insumoService, IProduccionService<TContext> produccionService)
        {
            _insumoService = insumoService;
            _produccionService = produccionService;
        }

        public async Task<RespuestaDTO> Crear(InsumoXProduccionDTO parametro)
        {
            try
            {
                var produccion = await _produccionService.ObtenerXId(parametro.IdProduccion);
                if (produccion.Id <= 0)
                {
                    return new RespuestaDTO
                    {
                        Descripcion = "No se encontró la prodicción asociada",
                        Estatus = false
                    };
                }
                if (produccion.Estatus != 0)
                {
                    return new RespuestaDTO
                    {
                        Descripcion = "Solo puedes agregar insumos a la producción mientras esta capturada",
                        Estatus = false
                    };
                }
                var resultado = await _insumoService.Crear(parametro);
                return resultado;
            }
            catch
            {
                return new RespuestaDTO
                {
                    Descripcion = "Ocurrio un error al intentar crear el insumo por producción",
                    Estatus = false
                };
            }
        }

        public async Task<InsumoXProduccionDTO> CrearYObtener(InsumoXProduccionDTO parametro)
        {
            try
            {
                var produccion = await _produccionService.ObtenerXId(parametro.IdProduccion);
                if (produccion.Id <= 0)
                {
                    return new InsumoXProduccionDTO();
                }
                if (produccion.Estatus != 0)
                {
                    return new InsumoXProduccionDTO();
                }
                var resultado = await _insumoService.CrearYObtener(parametro);
                return resultado;
            }
            catch
            {
                return new InsumoXProduccionDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(InsumoXProduccionDTO parametro)
        {
            try
            {
                var produccion = await _produccionService.ObtenerXId(parametro.IdProduccion);
                if (produccion.Id <= 0)
                {
                    return new RespuestaDTO
                    {
                        Descripcion = "No se encontró la prodicción asociada",
                        Estatus = false
                    };
                }
                if (produccion.Estatus != 0)
                {
                    return new RespuestaDTO
                    {
                        Descripcion = "Solo puedes editar insumos de la producción mientras esta capturada",
                        Estatus = false
                    };
                }
                var resultado = await _insumoService.Editar(parametro);
                return resultado;
            }
            catch
            {
                return new RespuestaDTO
                {
                    Descripcion = "Ocurrio un error al intentar editar el insumo por producción",
                    Estatus = false
                };
            }
        }

        public async Task<RespuestaDTO> Eliminar(int parametro)
        {
            try
            {
                var insumo = await _insumoService.ObtenerXId(parametro);
                if (insumo.Id <= 0)
                {
                    return new RespuestaDTO
                    {
                        Descripcion = "No se encontro el insumo por producción",
                        Estatus = false
                    };
                }
                var produccion = await _produccionService.ObtenerXId(insumo.IdProduccion);
                if (produccion.Id <= 0)
                {
                    return new RespuestaDTO
                    {
                        Descripcion = "No se encontró la prodicción asociada",
                        Estatus = false
                    };
                }
                if (produccion.Estatus != 0)
                {
                    return new RespuestaDTO
                    {
                        Descripcion = "Solo puedes editar insumos de la producción mientras esta capturada",
                        Estatus = false
                    };
                }
                var resultado = await _insumoService.Eliminar(parametro);
                return resultado;
            }
            catch
            {
                return new RespuestaDTO
                {
                    Descripcion = "Ocurrio un error al intentar eliminar el insumo por producción",
                    Estatus = false
                };
            }
        }

        public async Task<List<InsumoXProduccionDTO>> ObtenerXProduccion(int id)
        {
            try
            {
                var lista = await _insumoService.ObtenerXProduccion(id);
                return lista;
            }
            catch
            {
                return new List<InsumoXProduccionDTO>();
            }
        }
    }
}
