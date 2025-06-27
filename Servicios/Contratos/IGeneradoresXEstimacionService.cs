using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IGeneradoresXEstimacionService<T> where T : DbContext
    {
        Task<GeneradoresXEstimacionDTO> ObtenXId(int Id);
        Task<List<GeneradoresXEstimacionDTO>> ObtenXIdEstimacion(int Id);
        Task<RespuestaDTO> Crear(GeneradoresXEstimacionDTO modelo);
        Task<GeneradoresDTO> CrearYObtener(GeneradoresXEstimacionDTO modelo);
        Task<RespuestaDTO> Editar(GeneradoresXEstimacionDTO modelo);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
