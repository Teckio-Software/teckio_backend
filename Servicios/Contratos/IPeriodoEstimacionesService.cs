using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IPeriodoEstimacionesService<T> where T : DbContext
    {
        Task<List<PeriodoEstimacionesDTO>> ObtenTodosXIdProyecto(int IdProyecto);
        Task<PeriodoEstimacionesDTO> ObtenXId(int Id);
        Task<PeriodoEstimacionesDTO> CrearYObtener(PeriodoEstimacionesDTO registro);
        Task<bool> Editar(PeriodoEstimacionesDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
