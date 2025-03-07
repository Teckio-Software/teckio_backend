

using ERP_TECKIO.Modelos.Presupuesto;

namespace ERP_TECKIO.Modelos;

public partial class PrecioUnitarioDetalle

{
    public int Id { get; set; }

    public int IdPrecioUnitario { get; set; }

    public int IdInsumo { get; set; }

    public bool EsCompuesto { get; set; }

    public decimal Cantidad { get; set; }

    public decimal CantidadExcedente { get; set; }

    public int IdPrecioUnitarioDetallePerteneciente { get; set; }

    public virtual Insumo IdInsumoNavigation { get; set; } = null!;

    public virtual PrecioUnitario IdPrecioUnitarioNavigation { get; set; } = null!;
    public virtual ICollection<OperacionesXPrecioUnitarioDetalle> OperacionesXPrecioUnitarioDetalles { get; set; } = new List<OperacionesXPrecioUnitarioDetalle>();

}