
namespace ERP_TECKIO.Modelos;

public partial class InsumoXOrdenCompra
{
    public int Id { get; set; }

    public int? IdOrdenCompra { get; set; }

    public int? IdInsumoXcotizacion { get; set; }

    public int? IdInsumo { get; set; }

    public decimal? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public decimal? ImporteSinIva { get; set; }

    public decimal? Iva { get; set; }

    public decimal? Isr { get; set; }

    public decimal? Ieps { get; set; }

    public decimal? Isan { get; set; }

    public decimal? ImporteConIva { get; set; }

    public int? EstatusInsumoOrdenCompra { get; set; }

    public decimal? CantidadRecibida { get; set; } = 0;

    public virtual Insumo? IdInsumoNavigation { get; set; }

    public virtual InsumoXCotizacion? IdInsumoXcotizacionNavigation { get; set; }

    public virtual OrdenCompra? IdOrdenCompraNavigation { get; set; }

    public virtual ICollection<ImpuestoInsumoOrdenCompra> ImpuestoInsumoOrdenCompras { get; set; } = new List<ImpuestoInsumoOrdenCompra>();
}
