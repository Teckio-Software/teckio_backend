

namespace ERP_TECKIO
{
    public class HistorialComprasDTO
    {
        public DateTime FechaCompra { get; set; }
        public int IdContratista { get; set; }
        public string? RazonContratista { get; set; }
        public int IdInsumo { get; set; }
        public string? CodigoInsumo { get; set; }
        public string? DescripcionInsumo { get; set; }
        public string? UnidadInsumo { get; set; }
        public string? DescripcionCompletaInsumo { get; set; }
        public string? DescripcionTipoPago { get; set; } //Contado o en parcialIdades
        public decimal CantidadInsumos { get; set; }
        public decimal MontoPrecioUnitario { get; set; } //Monto descuento
        public decimal ImporteIva { get; set; } //Importe iva
        public decimal TotalPedIdo { get; set; } //Total con iva
        public int Estatus { get; set; }
    }
}
