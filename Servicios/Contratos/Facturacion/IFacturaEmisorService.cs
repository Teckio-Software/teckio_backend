using ERP_TECKIO.DTO.Factura;
using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface IFacturaEmisorService<TContext> where TContext : DbContext
    {
        Task<List<FacturaEmisorDTO>> ObtenTodos();
        Task<FacturaEmisorDTO> ObtenXIdFactura(int IdFactura);
        Task<List<FacturaEmisorDTO>> ObtenXRfc(string Rfc);
        Task<FacturaEmisorDTO> ObtenXId(int Id);
        Task<bool> Crear(FacturaEmisorDTO parametro);
        Task<FacturaEmisorDTO> CrearYObtener(FacturaEmisorDTO parametro);
        Task<bool> Editar(FacturaEmisorDTO parametro);
        Task<bool> Eliminar(int Id);
    }
}
