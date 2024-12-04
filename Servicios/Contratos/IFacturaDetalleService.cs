using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IFacturaDetalleService<TContext> where TContext : DbContext
    {
        Task<List<FacturaDetalleDTO>> ObtenTodos();
        Task<List<FacturaDetalleDTO>> ObtenXUnidadSat(string UnidadSat);
        Task<List<FacturaDetalleDTO>> ObtenXImporte(decimal Importe);
        Task<List<FacturaDetalleDTO>> ObtenXCantidad(decimal Cantidad);
        Task<List<FacturaDetalleDTO>> ObtenXIdFactura(int IdFactura);
        Task<FacturaDetalleDTO> ObtenXId(int Id);
        Task<bool> Crear(FacturaDetalleDTO parametro);
        Task<FacturaDetalleDTO> CrearYObtener(FacturaDetalleDTO parametro);
        Task<bool> Editar(FacturaDetalleDTO parametro);
        Task<bool> Eliminar(int Id);
    }
}
