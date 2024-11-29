
namespace ERP_TECKIO.Modelos;
public partial class PrecioUnitario
{
    public int Id { get; set; }

    public int IdProyecto { get; set; }

    public decimal Cantidad { get; set; }

    public decimal CantidadExcedente { get; set; }

    public int? TipoPrecioUnitario { get; set; }

    public int? Nivel { get; set; }

    public int? IdPrecioUnitarioBase { get; set; }

    public bool? EsDetalle { get; set; }

    public int? IdConcepto { get; set; }
    public virtual ICollection<Estimaciones> Estimaciones { get; set; } = new List<Estimaciones>();

    public virtual ICollection<Generadores> Generadores { get; set; } = new List<Generadores>();

    public virtual Concepto? IdConceptoNavigation { get; set; }

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;

    public virtual ICollection<PrecioUnitarioDetalle> PrecioUnitarioDetalles { get; set; } = new List<PrecioUnitarioDetalle>();

    public virtual ICollection<ProgramacionEstimada> ProgramacionEstimada { get; set; } = new List<ProgramacionEstimada>();
    public virtual ICollection<DetalleXContrato> DetallesXContrato { get; set; } = new List<DetalleXContrato>();
    public virtual ICollection<PorcentajeAcumuladoContrato> PorcentajeAcumuladoContrato { get; set; } = new List<PorcentajeAcumuladoContrato>();
}
