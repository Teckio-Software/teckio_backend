using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IFacturaComplementoPagoService<TContext> where TContext : DbContext
    {
        Task<List<FacturaComplementoPagoDTO>> ObtenTodos();
        Task<FacturaComplementoPagoDTO> ObtenXIdFactura(int IdFactura);
        Task<List<FacturaComplementoPagoDTO>> ObtenXUuidFactura(string Uuid);
        Task<FacturaComplementoPagoDTO> ObtenXId(int Id);
        Task<bool> Crear(FacturaComplementoPagoDTO parametro);
        Task<FacturaComplementoPagoDTO> CrearYObtener(FacturaComplementoPagoDTO parametro);
        Task<bool> Eliminar(int Id);
    }
}
