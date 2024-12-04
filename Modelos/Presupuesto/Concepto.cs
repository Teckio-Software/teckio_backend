




namespace ERP_TECKIO.Modelos;
public partial class Concepto
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string? Unidad { get; set; }

    public int? IdEspecialidad { get; set; }

    public decimal? CostoUnitario { get; set; }
    public decimal? PorcentajeIndirecto { get; set; }

    public int? IdProyecto { get; set; }
    public virtual ICollection<Estimaciones> Estimaciones { get; set; } = new List<Estimaciones>();

    public virtual Especialidad? IdEspecialidadNavigation { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual ICollection<PrecioUnitario> PrecioUnitarios { get; set; } = new List<PrecioUnitario>();
    public virtual ICollection<IndirectosXConcepto> IndirectosXConceptos { get; set; } = new List<IndirectosXConcepto>();

    public virtual ICollection<ProgramacionEstimada> ProgramacionEstimada { get; set; } = new List<ProgramacionEstimada>();
    public virtual ICollection<ProgramacionEstimadaGantt> ProgramacionEstimadaGantts { get; set; } = new List<ProgramacionEstimadaGantt>();
}
