using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    /// <summary>
    /// Interfaz para los contratistas
    /// </summary>
    public interface IContratistaService<TContext> where TContext : DbContext
    {
        Task<List<ContratistaDTO>> ObtenTodos();
        Task<ContratistaDTO> ObtenXRfc(string Rfc);
        Task<ContratistaDTO> ObtenXId(int Id);
        Task<ContratistaDTO> CrearYObtener(ContratistaDTO modelo);
        Task<RespuestaDTO> Editar(ContratistaDTO modelo);
        Task<RespuestaDTO> Eliminar(int Id);
    }
    /// <summary>
    /// Interfaz para los insumos por contratista
    /// </summary>
    public interface IInsumoXContratistaService
    {
        Task<List<InsumoXContratistaDTO>> Lista();
        Task<InsumoXContratistaDTO> ObtenXId(int Id);
        Task<InsumoXContratistaDTO> Crear(InsumoXContratistaCreacionDTO modelo);
        Task<bool> Editar(InsumoXContratistaDTO modelo);
        Task<bool> Eliminar(int Id);
    }
    /// <summary>
    /// Interfaz para los insumos por contratista
    /// </summary>
    public interface IEspecialIdadXContratistaService
    {
        Task<List<EspecialIdadXContratistaDTO>> Lista();
        Task<EspecialIdadXContratistaDTO> ObtenXId(int Id);
        Task<EspecialIdadXContratistaDTO> Crear(EspecialIdadXContratistaCreacionDTO modelo);
        Task<bool> Editar(EspecialIdadXContratistaDTO modelo);
        Task<bool> Eliminar(int Id);
    }
}
