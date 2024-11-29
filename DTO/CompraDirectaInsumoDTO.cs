namespace ERP_TECKIO
{
    public class CompraDirectaInsumoDTO
    {
        public int Id { get; set; }
        public int IdCompraDirecta { get; set; }
        public int IdInsumo { get; set; }
        public string CodigoInsumo { get; set; }
        public string DescripcionInsumo { get; set; }
        public string UnidadInsumo { get; set; }
        public decimal PrecioUnitarioInsumo { get; set; }
        public decimal CantidadInsumos { get; set; }
        public decimal PorcentajeIva { get; set; }
        //public decimal Descuento { get; set; }
        public decimal MontoTotalConIva { get; set; }
        public decimal InsumosSurtIdos { get; set; }
        public decimal Iva { get; set; }
        public decimal Isr { get; set; }
        public decimal Ieps { get; set; }
        public decimal Isan { get; set; }
    }

    public class CompraDirectaInsumoCreacionDTO
    {
        public int IdInsumo { get; set; }
        public decimal PrecioUnitarioInsumo { get; set; }
        public decimal CantidadInsumos { get; set; }
        public decimal Iva { get; set; }
        public decimal Isr { get; set; }
        public decimal Ieps { get; set; }
        public decimal Isan { get; set; }
    }
}
