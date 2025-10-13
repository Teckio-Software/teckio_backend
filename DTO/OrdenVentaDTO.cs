using ERP_TECKIO.DTO.Factura;

namespace ERP_TECKIO.DTO
{
    public class OrdenVentaDTO : OrdenVentaAbstract
    {
        public List<DetalleOrdenVentaDTO> DetalleOrdenVenta { get; set; } = new List<DetalleOrdenVentaDTO>();

        public string? RazonSocialCliente { get; set; }
        public decimal Saldo { get; set; }
        public decimal MontoAPagar { get; set; }
        public bool EsSeleccionado { get; set; }
    }

    public class OrdenVentaFacturasDTO
    {
        public int IdOrdenVenta { get; set; }
        public decimal MontoTotalOrdenVenta { get; set; }
        public decimal MontoTotalFactura { get; set; }
        public int EstatusSaldado { get; set; }
        public List<FacturaXOrdenVentaDTO> FacturasXOrdenVenta { get; set; } = new List<FacturaXOrdenVentaDTO>();
    }
}
