using ERP_TECKIO;
using System;
using System.Collections.Generic;

namespace SistemaERP.Model.Procomi.Proyecto;

public partial class DependenciaProgramacionEstimada
{
    public int Id { get; set; }

    public int IdProgramacionEstimadaGantt { get; set; }

    public virtual ProgramacionEstimadaGantt IdProgramacionEstimadaGanttNavigation { get; set; } = null!;
}
