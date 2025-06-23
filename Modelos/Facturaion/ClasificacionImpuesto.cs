
namespace ERP_TECKIO.Modelos;

public partial class ClasificacionImpuestoAbstract
{
    public int Id { get; set; }

    public string TipoClasificacionImpuesto { get; set; } = null!;
}
public partial class ClasificacionImpuesto: ClasificacionImpuestoAbstract
{
    public virtual ICollection<FacturaDetalleImpuesto> FacturaDetalleImpuestos { get; set; } = new List<FacturaDetalleImpuesto>();
    public virtual ICollection<FacturaImpuestosLocal> FacturaImpuestosLocales { get; set; } = new List<FacturaImpuestosLocal>();
    public virtual ICollection<FacturaImpuesto> FacturaImpuestos { get; set; } = new List<FacturaImpuesto>();
    public virtual ICollection<ImpuestoDetalleOrdenVentum> ImpuestoDetalleOrdenVenta { get; set; } = new List<ImpuestoDetalleOrdenVentum>();

}
