
namespace ERP_TECKIO.Modelos;

public partial class TipoFactor
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<FacturaDetalleImpuesto> FacturaDetalleImpuestos { get; set; } = new List<FacturaDetalleImpuesto>();
}
