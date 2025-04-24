
namespace ERP_TECKIO.Modelos;

public partial class TipoFactorAbstract
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;
}

public partial class TipoFactor : TipoFactorAbstract
{
    public virtual ICollection<FacturaDetalleImpuesto> FacturaDetalleImpuestos { get; set; } = new List<FacturaDetalleImpuesto>();
}
