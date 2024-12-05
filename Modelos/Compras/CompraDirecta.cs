
namespace ERP_TECKIO.Modelos;

public partial class CompraDirecta
{
    public int Id { get; set; }

    public int? IdProyecto { get; set; }

    public int IdContratista { get; set; }

    public int IdRequisicion { get; set; }

    public string? NoCompraDirecta { get; set; }

    public string? Elaboro { get; set; }

    public int? Estatus { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public string? Chofer { get; set; }

    public string? Observaciones { get; set; }

    public decimal? ImporteSinIva { get; set; }

    public decimal MontoDescuento { get; set; }

    public decimal ImporteConIva { get; set; }

    public virtual Contratista IdContratistaNavigation { get; set; } = null!;

    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual Requisicion IdRequisicionNavigation { get; set; } = null!;

    public virtual ICollection<InsumoXCompraDirecta> InsumoXCompraDirecta { get; set; } = new List<InsumoXCompraDirecta>();
}
