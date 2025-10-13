using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IOrdenVentaXMovimientoBancarioService<T> where T : DbContext
    {
        Task<bool> Crear(OrdenVentaXMovimientoBancarioDTO modelo);
        Task<bool> Editar(OrdenVentaXMovimientoBancarioDTO modelo);
        Task<List<OrdenVentaXMovimientoBancarioDTO>> ObtenXIdMovimientoBancario(int IdMovimientoBancario);
        Task<List<OrdenVentaXMovimientoBancarioDTO>> ObtenXIdOrdenVenta(int IdOrdenVenta);
    }
}
