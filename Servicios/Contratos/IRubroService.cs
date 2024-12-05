

namespace ERP_TECKIO
{
    public interface IRubroService<T>
    {
        Task<List<RubroDTO>> ObtenTodos();
        Task<RubroDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(RubroCreacionDTO modelo);
        Task<RespuestaDTO> Editar(RubroDTO modelo);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
