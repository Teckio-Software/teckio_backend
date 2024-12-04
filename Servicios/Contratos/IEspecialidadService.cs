using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IEspecialidadService<T> where T : DbContext
    {
        Task<List<EspecialidadDTO>> ObtenerTodos();
        Task<EspecialidadDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(EspecialidadDTO especialidad);
        Task<EspecialidadDTO> CrearYObtener(EspecialidadDTO especialidad);
        Task<RespuestaDTO> Editar(EspecialidadDTO parametro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
