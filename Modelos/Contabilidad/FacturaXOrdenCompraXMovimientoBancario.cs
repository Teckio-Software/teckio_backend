using ERP_TECKIO.Modelos.Facturaion;

namespace ERP_TECKIO.Modelos.Contabilidad
{
    public class FacturaXOrdenCompraXMovimientoBancario
    {
        public int Id { get; set; }
        public int IdFacturaXOrdenCompra { get; set; }
        public int IdMovimientoBancario { get; set; }
        public int Estatus { get; set; }
        public decimal TotalSaldado { get; set; }
        public virtual MovimientoBancario? IdMovimientoBancarioNavigation { get; set; }
        public virtual FacturaXOrdenCompra? IdFacturaXOrdenCompraNavigation { get; set; }

    }
}
