namespace ERP_TECKIO.Modelos;

public partial class TipoImpuesto
{
    public int Id { get; set; }

    public string ClaveImpuesto { get; set; } = null!;

    public string DescripcionImpuesto { get; set; } = null!;

    public virtual ICollection<FacturaDetalleImpuesto> FacturaDetalleImpuestos { get; set; } = new List<FacturaDetalleImpuesto>();
    public virtual ICollection<ImpuestoInsumoCotizado> ImpuestoInsumoCotizados { get; set; } = new List<ImpuestoInsumoCotizado>();
    public virtual ICollection<ImpuestoInsumoOrdenCompra> ImpuestoInsumoOrdenCompras { get; set; } = new List<ImpuestoInsumoOrdenCompra>();
}
