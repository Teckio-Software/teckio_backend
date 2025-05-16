using ERP_TECKIO.DTO.Factura;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface IFacturaXOrdenCompraService<T> where T : DbContext
    {
        Task<bool> Crear(FacturaXOrdenCompraDTO parametro);
        Task<List<FacturaXOrdenCompraDTO>> ObtenerXIdOrdenCompra(int IdOrdenCompra);
    }
}
