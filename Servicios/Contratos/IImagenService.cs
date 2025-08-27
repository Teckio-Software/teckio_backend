using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IImagenService<T> where T: DbContext
    {
        Task<RespuestaDTO> Crear(ImagenDTO modelo);
        Task<ImagenDTO> CrearYObtener(ImagenDTO modelo);
        Task<RespuestaDTO> Editar(ImagenDTO modelo);
        Task<RespuestaDTO> Eliminar(int id);
        Task<ImagenDTO> Obtener(int id);
        Task<List<ImagenDTO>> ObtenerTodos();
    }
}
