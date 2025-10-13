using ERP_TECKIO.Modelos.Contabilidad;

namespace ERP_TECKIO.Modelos.Facturaion
{
    public class FacturaXOrdenVenta : FacturaXOrdenVentaAbstract
    {

        public virtual Factura? IdFacturaNavigation { get; set; }
        public virtual OrdenVentum? IdOrdenVentumNavigation { get; set; }
        public ICollection<FacturaXOrdenVentaXMovimientoBancario> FacturaXOrdenVentaXMovimientoBancarios { get; set; } = new List<FacturaXOrdenVentaXMovimientoBancario>();
    }

    public abstract class FacturaXOrdenVentaAbstract
    {
        public int Id { get; set; }
        public int IdFactura { get; set; }
        public int IdOrdenVenta { get; set; }
        public int Estatus { get; set; }
        public decimal TotalSaldado { get; set; }
    }
}
