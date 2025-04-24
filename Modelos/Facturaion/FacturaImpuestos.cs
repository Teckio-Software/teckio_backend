namespace ERP_TECKIO.Modelos.Facturaion
{
    public class FacturaImpuestosAbstract
    {
        public int Id { get; set; }
        public int IdCategoriaImpuesto { get; set; }
        public int IdFactura {  get; set; }
        public int IdClasificacionImpuesto { get; set; }
        public decimal TotalImpuesto { get; set; }
    }

    public class FacturaImpuestos: FacturaImpuestosAbstract
    {
        public virtual CategoriaImpuesto IdCategoriaImpuestoNavigation { get; set; } = null!;
        public virtual Factura IdFacturaNavigation { get; set; } = null!;
        public virtual ClasificacionImpuesto IdClasificacionImpuestoNavigation { get; set; } = null!;
    }
}
