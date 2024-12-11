using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO
{
    public interface IProgramacionEstimadaGanttService<T> where T : DbContext
    {
        Task<List<ProgramacionEstimadaGanttDTO>> ObtenerXIdProyecto(int IdProyecto, DbContext db);
        Task<ProgramacionEstimadaGanttDTO> ObtenerXId(int Id, DbContext db);
        Task<List<ProgramacionEstimadaGantt>> ObtenerProgramacionesEnModelo(int IdProyecto);
        Task<ProgramacionEstimadaGanttDTO> CrearYObtener(ProgramacionEstimadaGanttDTO registro);
        Task<RespuestaDTO> Editar(ProgramacionEstimadaGanttDTO registro);
        Task<RespuestaDTO> EditarModelo(ProgramacionEstimadaGantt registro);
        Task<RespuestaDTO> Eliminar(int IdRegistro);
        Task<RespuestaDTO> EliminarMultiple(List<ProgramacionEstimadaGanttDTO> registros);
    }
}
