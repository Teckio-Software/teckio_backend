
namespace ERP_TECKIO.Modelos;

public partial class CategoriaImpuesto
{
    public int Id { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<FacturaDetalleImpuesto> FacturaDetalleImpuestos { get; set; } = new List<FacturaDetalleImpuesto>();

    public virtual ICollection<FacturaImpuesto> FacturaImpuestos { get; set; } = new List<FacturaImpuesto>();
    public virtual ICollection<FacturaImpuestosLocal> FacturaImpuestosLocales { get; set; } = new List<FacturaImpuestosLocal>();
}
