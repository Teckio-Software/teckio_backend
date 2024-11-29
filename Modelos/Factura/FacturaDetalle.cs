
namespace ERP_TECKIO.Modelos;

public partial class FacturaDetalle
{
    public int Id { get; set; }

    public int IdFactura { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public string UnidadSat { get; set; } = null!;

    public decimal Importe { get; set; }

    public decimal Descuento { get; set; }

    public virtual ICollection<FacturaDetalleImpuesto> FacturaDetalleImpuestos { get; set; } = new List<FacturaDetalleImpuesto>();
    public virtual Factura IdFacturaNavigation { get; set; } = null!;
}
