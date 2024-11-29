using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


namespace ERP_TECKIO
{
    public interface IFacturaService<TContext> where TContext : DbContext
    {
        Task<List<FacturaBaseDTO>> ObtenTodos();
        Task<List<FacturaBaseDTO>> ObtenTodosT();
        Task<List<FacturaBaseDTO>> ObtenSoloValidas();
        Task<List<FacturaBaseDTO>> ObtenXUuid(string uuid);
        Task<List<FacturaBaseDTO>> ObtenXRfcEmisor(string rfcEmisor);
        Task<List<FacturaBaseDTO>> ObtenXRfcReceptor(string rfcReceptor);
        Task<FacturaBaseDTO> ObtenXId(int Id);
        Task<FacturaBaseDTO> ObtenXIdT(int Id);
        Task<bool> Crear(FacturaBaseDTO parametro);
        Task<FacturaBaseDTO> CrearYObtener(FacturaBaseDTO parametro);
        Task<bool> Editar(FacturaBaseDTO parametro);
        Task<bool> EsEnviado(int IdFactura);
    }
}
