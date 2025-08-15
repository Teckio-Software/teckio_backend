using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class ExistenciaProductoAlmacenProceso<TContex> where TContex : DbContext
    {
        private readonly IExistenciaProductoAlmacenService<TContex> _existenciaProAlService;

        public ExistenciaProductoAlmacenProceso(IExistenciaProductoAlmacenService<TContex> existenciaProAlService)
        {
            _existenciaProAlService = existenciaProAlService;
        }

        public async Task<RespuestaDTO> Crear(ExistenciaProductoAlmacenDTO parametro)
        {
            try
            {
                var resultado = await _existenciaProAlService.Crear(parametro);
                return resultado;
            }
            catch
            {
                return new RespuestaDTO
                {
                    Descripcion = "Ocurrio un error al intentar crear la existencia producto almacen",
                    Estatus = false
                };
            }
        }

        public async Task<ExistenciaProductoAlmacenDTO> CrearYObtener(ExistenciaProductoAlmacenDTO parametro)
        {
            try
            {
                var resultado = await _existenciaProAlService.CrearYObtener(parametro);
                return resultado;
            }
            catch
            {
                return new ExistenciaProductoAlmacenDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(ExistenciaProductoAlmacenDTO parametro)
        {
            try
            {
                var resultado = await _existenciaProAlService.Editar(parametro);
                return resultado;
            }
            catch
            {
                return new RespuestaDTO
                {
                    Descripcion = "Ocurrio un error al intentar editar la existencia producto almacen",
                    Estatus = false
                };
            }
        }

        public async Task<RespuestaDTO> Eliminar(int parametro)
        {
            try
            {
                var objeto = new ExistenciaProductoAlmacenDTO
                {
                    Id = parametro
                };
                var resultado = await _existenciaProAlService.Editar(objeto);
                return resultado;
            }
            catch
            {
                return new RespuestaDTO
                {
                    Descripcion = "Ocurrio un error al intentar eliminar la existencia producto almacen",
                    Estatus = false
                };
            }
        }
    }
}
