using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IDependenciaProgramacionEstimadaService<T> where T : DbContext
    {
        Task<List<DependenciaProgramacionEstimadaDTO>> ObtenerXIdProgramacionEstimadaGantt(int IdProgramacionEstimadaGantt);
        Task<DependenciaProgramacionEstimadaDTO> ObtenerXId(int Id);
        Task<DependenciaProgramacionEstimadaDTO> CrearYObtener(DependenciaProgramacionEstimadaDTO registro);
        Task<RespuestaDTO> Editar(DependenciaProgramacionEstimadaDTO registro);
        Task<RespuestaDTO> Eliminar(int IdRegistro);
        Task<RespuestaDTO> EliminarMultiple(List<DependenciaProgramacionEstimadaDTO> registros);
    }
}
