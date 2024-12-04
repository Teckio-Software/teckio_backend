using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IDetalleValidacionService<TContext> where TContext : DbContext
    {
        Task<List<DetalleValidacionDTO>> ObtenTodos();
        Task<List<DetalleValidacionDTO>> ObtenSoloCancelados();
        Task<List<DetalleValidacionDTO>> ObtenXIdAcuseValidacion(int IdAcuseValidacion);
        Task<List<DetalleValidacionDTO>> ObtenXIdCatalogoValidacion(string CodigoCatalogoValidacion);
        Task<DetalleValidacionDTO> ObtenXId(int Id);
        Task<bool> Crear(DetalleValidacionDTO parametro);
        Task<DetalleValidacionDTO> CrearYObtener(DetalleValidacionDTO parametro);
        Task<bool> Editar(DetalleValidacionDTO parametro);
        Task<bool> Eliminar(int Id);
    }
}
