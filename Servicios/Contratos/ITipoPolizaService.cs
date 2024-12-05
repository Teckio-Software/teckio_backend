using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    /// <summary>
    /// Interfaz del tipo de poliza
    /// </summary>
    public interface ITipoPolizaService<T> where T : DbContext
    {
        Task<List<TipoPolizaDTO>> ObtenTodos();
        Task<TipoPolizaDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(TipoPolizaCreacionDTO modelo);
        Task<RespuestaDTO> Editar(TipoPolizaDTO modelo);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
