
namespace ERP_TECKIO.Modelos;

public partial class FacturaImpuesto
{
    public int Id { get; set; }

    public int IdCategoriaImpuesto { get; set; }

    public int IdFactura { get; set; }

    public int IdClasificacionImpuesto { get; set; }
    public int IdTipoImpuesto { get; set; }

    public decimal TotalImpuesto { get; set; }

    public virtual CategoriaImpuesto IdCategoriaImpuestoNavigation { get; set; } = null!;

    public virtual ClasificacionImpuesto IdClasificacionImpuestoNavigation { get; set; } = null!;

    public virtual Factura IdFacturaNavigation { get; set; } = null!;
    public virtual TipoImpuesto IdTipoImpuestoNavigation { get; set; } = null!;
}
