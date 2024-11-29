
namespace ERP_TECKIO.Modelos;

public partial class OrdenCompra
{
    public int Id { get; set; }

    public int? IdRequisicion { get; set; }

    public int? IdCotizacion { get; set; }

    public int? IdContratista { get; set; }

    public int? IdProyecto { get; set; }

    public string? NoOrdenCompra { get; set; }

    public string? Elaboro { get; set; }

    public int? Estatus { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? Chofer { get; set; }

    public string? Observaciones { get; set; }

    public int? EstatusInsumosSurtidos { get; set; }

    public virtual ICollection<AlmacenEntradaInsumo> AlmacenEntradaInsumos { get; set; } = new List<AlmacenEntradaInsumo>();

    public virtual Contratista? IdContratistaNavigation { get; set; }

    public virtual Cotizacion? IdCotizacionNavigation { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual Requisicion? IdRequisicionNavigation { get; set; }

    public virtual ICollection<InsumoXOrdenCompra> InsumoXordenCompras { get; set; } = new List<InsumoXOrdenCompra>();
}
