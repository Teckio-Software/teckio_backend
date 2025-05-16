namespace ERP_TECKIO.Modelos.Facturaion
{
    public class FacturaXOrdenCompra : FacturaXOrdenCompraAbstract
    {
        public virtual Factura IdFacturaNavigation { get; set; } = null!;
        public virtual OrdenCompra IdOrdenCompraNavigation { get; set; } = null!;

    }

    public abstract class FacturaXOrdenCompraAbstract
    {
        public int Id { get; set; }
        public int IdOrdenCompra { get; set; }
        public int IdFactura { get; set; }
        public int Estatus { get; set; }
    }
}
