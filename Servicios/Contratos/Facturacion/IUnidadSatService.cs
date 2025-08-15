using ERP_TECKIO.DTO.Factura;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface IUnidadSatService<T> where T : DbContext
    {
        Task<bool> Crear(UnidadSatDTO parametro);
        Task<UnidadSatDTO> ObtenerXClave(string clave);
        Task<UnidadSatDTO> ObtenerXId(int Id);
        Task<List<UnidadSatDTO>> ObtenerTodos();
    }
}
