namespace ERP_TECKIO.DTO
{
    public class SalidaProduccionAlmacenAutorizarOrdenVDTO
    {
        public int IdOrdenVenta { get; set; }
        public List<SalidaProduccionAlmacenDTO> Salidas { get; set; } = new List<SalidaProduccionAlmacenDTO>();

    }
}
