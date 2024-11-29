
namespace ERP_TECKIO.Modelos;

public partial class InsumoXRequisicion
{
    public int Id { get; set; }

    public int? IdRequisicion { get; set; }

    public int? IdInsumo { get; set; }

    public int Denominacion { get; set; }

    public string? PersonaIniciales { get; set; } = null;

    public string Folio { get; set; } = null!;

    public decimal? Cantidad { get; set; }

    public decimal? CantidadComprada { get; set; }

    public decimal? CantidadEnAlmacen { get; set; }

    public string? Observaciones { get; set; }

    public DateTime? FechaEntrega { get; set; } = Convert.ToDateTime(null);

    public int EstatusInsumoRequisicion { get; set; }

    public int? EstatusInsumoComprado { get; set; }

    public int? EstatusInsumoSurtido { get; set; }
    public virtual Insumo? IdInsumoNavigation { get; set; }

    public virtual Requisicion? IdRequisicionNavigation { get; set; }

    public virtual ICollection<InsumoXCompraDirecta> InsumoXcompraDirecta { get; set; } = new List<InsumoXCompraDirecta>();

    public virtual ICollection<InsumoXCotizacion> InsumoXcotizacions { get; set; } = new List<InsumoXCotizacion>();
}
