namespace ERP_TECKIO.DTO
{
    public class OrdenVentaDTO : OrdenVentaAbstract
    {
        public List<DetalleOrdenVentaDTO> DetalleOrdenVenta { get; set; } = new List<DetalleOrdenVentaDTO>();

    }
}
