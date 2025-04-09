namespace ERP_TECKIO.Modelos;

public partial class FacturaDetalleImpuesto
{
    public int Id { get; set; }

    public int IdFacturaDetalle { get; set; }

    public int IdTipoImpuesto { get; set; }

    public int IdTipoFactor { get; set; }

    public int IdClasificacionImpuesto { get; set; }

    public decimal Base { get; set; }

    public decimal Importe { get; set; }

    public decimal TasaCuota { get; set; }
    public int IdCategoriaImpuesto { get; set; }
    public virtual CategoriaImpuesto IdCategoriaImpuestoNavigation { get; set; } = null!;
    public virtual ClasificacionImpuesto IdClasificacionImpuestoNavigation { get; set; } = null!;

    public virtual FacturaDetalle IdFacturaDetalleNavigation { get; set; } = null!;

    public virtual TipoFactor IdTipoFactorNavigation { get; set; } = null!;

    public virtual TipoImpuesto IdTipoImpuestoNavigation { get; set; } = null!;
}
