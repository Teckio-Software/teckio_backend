namespace ERP_TECKIO.Modelos;

public partial class InsumoXCotizacion : InsumoXCotizacionAbstract
{
    public decimal? Iva { get; set; }

    public decimal? Isr { get; set; }

    public decimal? Ieps { get; set; }

    public decimal? Isan { get; set; }
    public virtual Cotizacion? IdCotizacionNavigation { get; set; }

    public virtual Insumo? IdInsumoNavigation { get; set; }

    public virtual InsumoXRequisicion? IdInsumoRequisicionNavigation { get; set; }

    public virtual ICollection<InsumoXOrdenCompra> InsumoXordenCompras { get; set; } = new List<InsumoXOrdenCompra>();

    public virtual ICollection<ImpuestoInsumoCotizado> ImpuestoInsumoCotizados { get; set; } = new List<ImpuestoInsumoCotizado>();
}

public abstract class InsumoXCotizacionAbstract
{
    public int Id { get; set; }

    public int? IdCotizacion { get; set; }

    public int? IdInsumoRequisicion { get; set; }

    public int? IdInsumo { get; set; }

    public decimal? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public decimal? ImporteSinIva { get; set; }
    public decimal? ImporteTotal { get; set; }

    public int? EstatusInsumoCotizacion { get; set; }

    public decimal Descuento { get; set; }

    
}
