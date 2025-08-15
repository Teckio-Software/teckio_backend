using ERP_TECKIO.DTO.Factura;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface IProductoYServicioSatService<T> where T : DbContext
    {
        Task<bool> Crear(ProductoYServicioSatDTO parametro);
        Task<ProductoYServicioSatDTO> ObtenerXClave(string clave);
        Task<ProductoYServicioSatDTO> ObtenerXId(int Id);
        Task<List<ProductoYServicioSatDTO>> ObtenerTodos();
    }
}
