using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IGeneradoresService<T> where T : DbContext
    {
        Task<List<GeneradoresDTO>> ObtenerTodosXIdPrecioUnitario(int IdPrecioUnitario);
        Task<GeneradoresDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(GeneradoresDTO modelo);
        Task<GeneradoresDTO> CrearYObtener(GeneradoresDTO modelo);
        Task<RespuestaDTO> Editar(GeneradoresDTO modelo);
        Task<RespuestaDTO> Eliminar(int Id);
        Task<bool> EliminarTodos(int idPrecioU);
    }
}
