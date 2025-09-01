
using ERP_TECKIO.Modelos;

namespace ERP_TECKIO.DTO
{
    public class SalidaProduccionAlmacenDTO: SalidaProduccionAlmacenAbstract
    {
        public List<ProductosXsalidaProduccionAlmacenDTO> ProductosXsalidaProduccionAlmacens { get; set; } = new List<ProductosXsalidaProduccionAlmacenDTO>();
    }
}
