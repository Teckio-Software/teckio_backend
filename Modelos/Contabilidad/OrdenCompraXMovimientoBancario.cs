namespace ERP_TECKIO.Modelos.Contabilidad
{
    public class OrdenCompraXMovimientoBancario
    {
        public int Id { get; set; }
        public int IdMovimientoBancario { get; set; }
        public int IdOrdenCompra { get; set; }
        public int? Estatus { get; set; }
        public decimal? TotalSaldado { get; set; }
            
        public virtual MovimientoBancario? IdMovimientoBancarioNavigation { get; set; }
        public virtual OrdenCompra? IdOrdenCompraNavigation { get; set; }
    }
}
