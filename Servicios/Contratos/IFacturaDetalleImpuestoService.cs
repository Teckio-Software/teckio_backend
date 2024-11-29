using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


namespace ERP_TECKIO
{
    public interface IFacturaDetalleImpuestoService<TContext> where TContext : DbContext
    {
        Task<List<FacturaDetalleImpuestoDTO>> ObtenTodos();
        Task<List<FacturaDetalleImpuestoDTO>> ObtenXIdTipoImpuesto(int IdTipoImpuesto);
        Task<List<FacturaDetalleImpuestoDTO>> ObtenXIdTipoFactor(int IdTipoFactor);
        Task<List<FacturaDetalleImpuestoDTO>> ObtenXIdClasificacionImpuesto(int IdClasificacionImpuesto);
        Task<List<FacturaDetalleImpuestoDTO>> ObtenXIdFacturaDetalle(int IdFacturaDetalle);
        Task<FacturaDetalleImpuestoDTO> ObtenXId(int Id);
        Task<bool> Crear(FacturaDetalleImpuestoDTO parametro);
        Task<FacturaDetalleImpuestoDTO> CrearYObtener(FacturaDetalleImpuestoDTO parametro);
        Task<bool> Editar(FacturaDetalleImpuestoDTO parametro);
        Task<bool> Eliminar(int Id);
    }
}
