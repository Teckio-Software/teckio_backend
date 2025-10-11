namespace ERP_TECKIO.Modelos.Contabilidad
{
    public class OrdenVentaXMovimientoBancario : OrdenVentaXMovimientoBancarioAbstract
    {
        public virtual OrdenVentum? IdOrdenVentumNavigation { get; set; }
        public virtual MovimientoBancario? IdMovimientoBancarioNavigation { get; set; }
    }

    public abstract class OrdenVentaXMovimientoBancarioAbstract
    {
        public int Id { get; set; }
        public int IdOrdenVenta { get; set; }
        public int IdMovimientoBancario { get; set; }
        public int Estatus { get; set; }
        public decimal TotalSaldado { get; set; }
    }
}
