using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IFacturaXOrdenCompraXMovimientoBancarioService<T> where T : DbContext
    {
        Task<bool> Crear(FacturaXOrdenCompraXMovimientoBancarioDTO modelo);
        Task<bool> Editar(FacturaXOrdenCompraXMovimientoBancarioDTO modelo);
        Task<List<FacturaXOrdenCompraXMovimientoBancarioDTO>> ObtenXIdMovimientoBancario(int IdMovimientoBancario);
        Task<List<FacturaXOrdenCompraXMovimientoBancarioDTO>> ObtenXIdFacturaXOrdenCompra(int IdFacturaXOrdenCompra);
    }
}
