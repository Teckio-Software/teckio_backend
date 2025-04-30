using ERP_TECKIO.DTO.Factura;
using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface IFacturaService<TContext> where TContext : DbContext
    {
        Task<List<FacturaDTO>> ObtenTodos();
        Task<List<FacturaDTO>> ObtenTodosT();
        Task<List<FacturaDTO>> ObtenSoloValidas();
        Task<List<FacturaDTO>> ObtenXUuid(string uuid);
        Task<List<FacturaDTO>> ObtenXRfcEmisor(string rfcEmisor);
        Task<List<FacturaDTO>> ObtenXRfcReceptor(string rfcReceptor);
        Task<FacturaDTO> ObtenXId(int Id);
        Task<FacturaDTO> ObtenXIdT(int Id);
        Task<bool> Crear(FacturaDTO parametro);
        Task<FacturaDTO> CrearYObtener(FacturaDTO parametro);
        Task<bool> Editar(FacturaDTO parametro);
        Task<bool> EsEnviado(int IdFactura);
    }
}
