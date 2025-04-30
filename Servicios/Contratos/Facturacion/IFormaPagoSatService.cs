using ERP_TECKIO.DTO.Factura;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface IFormaPagoSatService<T> where T : DbContext
    {
        Task<bool> Crear(FormaPagoSatDTO parametro);
        Task<FormaPagoSatDTO> ObtenerXClave(string clave);
        Task<FormaPagoSatDTO> ObtenerXId(int Id);
        Task<List<FormaPagoSatDTO>> ObtenerTodos();

    }
}
