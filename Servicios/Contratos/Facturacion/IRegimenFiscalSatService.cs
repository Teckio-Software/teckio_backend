using ERP_TECKIO.DTO.Factura;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface IRegimenFiscalSatService<T> where T : DbContext
    {
        Task<bool> Crear(RegimenFiscalSatDTO parametro);
        Task<RegimenFiscalSatDTO> ObtenerXClave(string clave);
        Task<RegimenFiscalSatDTO> ObtenerXId(int Id);
        Task<List<RegimenFiscalSatDTO>> ObtenerTodos();
    }
}
