namespace ERP_TECKIO
{
    public class InsumoXCompraDirectaDTO
    {
        public int Id { get; set; }
        public int IdCompraDirecta { get; set; }
        public int IdInsumoRequisicion { get; set; }
        public int IdInsumo { get; set; }
        public string CodigoInsumo { get; set; }
        public string DescripcionInsumo { get; set; }
        public string UnidadInsumo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal ImporteSinIva { get; set; }
        public decimal Iva { get; set; }
        public decimal Isr { get; set; }
        public decimal Ieps { get; set; }
        public decimal Isan { get; set; }
        public decimal ImporteConIva { get; set; }
        public int EstatusInsumoCompraDirecta { get; set; }
        public int EstatusInsumosSurtIdos { get; set; }
    }

    public class InsumoXCompraDirectaCreacionDTO
    {
        public int IdCompraDirecta { get; set; }
        public int IdInsumoRequisicion { get; set; }
        public int IdInsumo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal ImporteSinIva { get; set; }
        public decimal Iva { get; set; }
        public decimal Isr { get; set; }
        public decimal Ieps { get; set; }
        public decimal Isan { get; set; }
        public decimal ImporteConIva { get; set; }
        public int EstatusInsumoCompraDirecta { get; set; }
    }
}
