
namespace ERP_TECKIO;

public partial class DependenciaProgramacionEstimada
{
    public int Id { get; set; }

    public int IdProgramacionEstimadaGantt { get; set; }

    public virtual ProgramacionEstimadaGantt IdProgramacionEstimadaGanttNavigation { get; set; } = null!;
}
