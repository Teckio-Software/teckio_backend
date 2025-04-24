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
        Task<DetalleValidacionDTO> CrearYObtener(DetalleValidacionDTO parametro);
        Task<RespuestaDTO> Editar(DetalleValidacionDTO parametro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
