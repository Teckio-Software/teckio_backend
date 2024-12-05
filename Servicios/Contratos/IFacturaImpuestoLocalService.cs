using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IFacturaImpuestoLocalService<TContext> where TContext : DbContext
    {
        Task<List<FacturaImpuestosLocalesDTO>> ObtenTodos();
        Task<List<FacturaImpuestosLocalesDTO>> ObtenXIdCategoriaImpuesto(int IdCategoriaImpuesto);
        Task<List<FacturaImpuestosLocalesDTO>> ObtenXIdFactura(int IdFactura);
        Task<List<FacturaImpuestosLocalesDTO>> ObtenXIdClasificacionImpuesto(int IdClasificacion);
        Task<FacturaImpuestosLocalesDTO> ObtenXId(int Id);
        Task<bool> Crear(FacturaImpuestosLocalesDTO parametro);
        Task<FacturaImpuestosLocalesDTO> CrearYObtener(FacturaImpuestosLocalesDTO parametro);
        Task<bool> Editar(FacturaImpuestosLocalesDTO parametro);
        Task<bool> Eliminar(int Id);
    }
}
