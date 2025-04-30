using ERP_TECKIO.DTO.Factura;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface IMonedaSatService<T> where T : DbContext
    {
        Task<bool> Crear(MonedaSatDTO parametro);
        Task<MonedaSatDTO> ObtenerXClave(string clave);
        Task<MonedaSatDTO> ObtenerXId(int Id);
        Task<List<MonedaSatDTO>> ObtenerTodos();

    }
}
