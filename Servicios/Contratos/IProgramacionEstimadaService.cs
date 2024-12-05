using Microsoft.EntityFrameworkCore;



namespace ERP_TECKIO
{
    public interface IProgramacionEstimadaService<T> where T : DbContext
    {
        Task<List<ProgramacionEstimadaDTO>> ObtenerTodosXProyecto(int IdProyecto);
        Task<ProgramacionEstimadaDTO> ObtenXId(int Id);
        Task<ProgramacionEstimadaDTO> CrearYObtener(ProgramacionEstimadaDTO hijo);
        Task<RespuestaDTO> Editar(ProgramacionEstimadaDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
