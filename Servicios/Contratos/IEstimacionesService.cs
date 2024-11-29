using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


namespace ERP_TECKIO
{
    public interface IEstimacionesService<T> where T : DbContext
    {
        Task<List<EstimacionesDTO>> ObtenTodosXIdPeriodo(int IdPeriodo);
        Task<EstimacionesDTO> ObtenXId(int id);
        Task<EstimacionesDTO> CrearYObtener(EstimacionesDTO registro);
        Task<bool> Editar(EstimacionesDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
        Task<bool> EliminarMultiple(int IdPeriodo);
    }
}
