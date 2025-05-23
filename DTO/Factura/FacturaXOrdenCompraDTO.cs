using ERP_TECKIO.Modelos.Facturaion;

namespace ERP_TECKIO.DTO.Factura
{
    public class FacturaXOrdenCompraDTO : FacturaXOrdenCompraAbstract
    {
        public string Uuid { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaCarga { get; set; }
        public decimal MontoAPagar { get; set; }
        public decimal Saldo { get; set; }

        public List<FacturaDetalleDTO> DetalleFactura { get; set; } = new List<FacturaDetalleDTO>();
    }
}
