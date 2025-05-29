using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IOrdenCompraXMovimientoBancarioService<T> where T : DbContext
    {
        Task<bool> Crear(OrdenCompraXMovimientoBancarioDTO modelo);
        Task<bool> Editar(OrdenCompraXMovimientoBancarioDTO modelo);
        Task<List<OrdenCompraXMovimientoBancarioDTO>> ObtenXIdMovimientoBancario(int IdMovimientoBancario);
        Task<List<OrdenCompraXMovimientoBancarioDTO>> ObtenXIdOrdenCompra(int IdOrdenCompra);

    }
}
