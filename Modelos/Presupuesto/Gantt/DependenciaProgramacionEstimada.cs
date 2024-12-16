
using ERP_TECKIO.Modelos;

namespace ERP_TECKIO;

public partial class DependenciaProgramacionEstimada
{
    public int Id { get; set; }
    public int IdProgramacionEstimadaGantt { get; set; }
    public int IdProgramacionEstimadaGanttPredecesora { get; set; }
    public int IdProyecto { get; set; }
    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
    public virtual ProgramacionEstimadaGantt IdProgramacionEstimadaGanttNavigation { get; set; } = null!;
    public virtual ProgramacionEstimadaGantt IdProgramacionEstimadaGanttPredecesoraNavigation { get; set; } = null!;
}
