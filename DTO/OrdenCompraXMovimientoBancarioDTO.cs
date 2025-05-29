namespace ERP_TECKIO.DTO
{
    public class OrdenCompraXMovimientoBancarioDTO
    {
        public int Id { get; set; }
        public int IdMovimientoBancario { get; set; }
        public int IdOrdenCompra { get; set; }
        public int? Estatus { get; set; }
        public decimal? TotalSaldado { get; set; }
    }
}
