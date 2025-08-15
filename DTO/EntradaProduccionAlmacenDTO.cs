using ERP_TECKIO.Modelos;

namespace ERP_TECKIO.DTO
{
    public class EntradaProduccionAlmacenDTO : EntradaProduccionAlmacenAbstract
    {
        public List<ProductosXEntradaProduccionAlmacenDTO> Detalles { get; set; } = new List<ProductosXEntradaProduccionAlmacenDTO>();
    }
}
