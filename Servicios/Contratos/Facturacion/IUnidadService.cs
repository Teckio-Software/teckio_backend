using ERP_TECKIO.DTO.Factura;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface IUnidadService<T> where T : DbContext
    {
        Task<bool> Crear(UnidadDTO parametro);
        Task<UnidadDTO> ObtenerXId(int Id);
        Task<List<UnidadDTO>> ObtenerTodos();

    }
}
