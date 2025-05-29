namespace ERP_TECKIO.DTO
{
    public class FacturaXOrdenCompraXMovimientoBancarioDTO
    {
        public int Id { get; set; }
        public int IdFacturaXOrdenCompra { get; set; }
        public int IdMovimientoBancario { get; set; }
        public int Estatus { get; set; }
        public decimal TotalSaldado { get; set; }
    }
}
