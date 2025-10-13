using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IFacturaXOrdenVentaXMovimientoBancarioService<T> where T : DbContext
    {
        Task<bool> Crear(FacturaXOrdenVentaXMovimientoBancarioDTO modelo);
        Task<bool> Editar(FacturaXOrdenVentaXMovimientoBancarioDTO modelo);
        Task<List<FacturaXOrdenVentaXMovimientoBancarioDTO>> ObtenXIdMovimientoBancario(int IdMovimientoBancario);
        Task<List<FacturaXOrdenVentaXMovimientoBancarioDTO>> ObtenXIdFacturaXOrdenVenta(int IdFacturaXOrdenVenta);
    }
}
