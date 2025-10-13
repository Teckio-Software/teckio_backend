using ERP_TECKIO.Modelos.Facturaion;

namespace ERP_TECKIO.Modelos.Contabilidad
{
    public class FacturaXOrdenVentaXMovimientoBancario : FacturaXOrdenVentaXMovimientoBancarioAbstract
    {
        public virtual MovimientoBancario? IdMovimientoBancarioNavigation { get; set; }
        public virtual FacturaXOrdenVenta? IdFacturaXOrdenVentaNavigation { get; set; }
    }

    public abstract class FacturaXOrdenVentaXMovimientoBancarioAbstract
    {
        public int Id { get; set; }
        public int IdFacturaXOrdenVenta { get; set; }
        public int IdMovimientoBancario { get; set; }
        public int Estatus { get; set; }
        public decimal TotalSaldado { get; set; }
    }
}
