using ERP_TECKIO.DTO.Factura;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface IFacturaXOrdenVentaService<T> where T : DbContext
    {
        Task<bool> Crear(FacturaXOrdenVentaDTO parametro);
        Task<bool> Editar(FacturaXOrdenVentaDTO parametro);
        Task<List<FacturaXOrdenVentaDTO>> ObtenerXIdOrdenVenta(int IdOrdenVenta);
    }
}
