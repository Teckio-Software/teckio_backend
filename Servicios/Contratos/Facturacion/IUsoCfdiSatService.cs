using ERP_TECKIO.DTO.Factura;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface IUsoCfdiSatService<T> where T : DbContext
    {
        Task<bool> Crear(UsoCfdiSatDTO parametro);
        Task<UsoCfdiSatDTO> ObtenerXClave(string clave);
        Task<UsoCfdiSatDTO> ObtenerXId(int Id);
    }
}
