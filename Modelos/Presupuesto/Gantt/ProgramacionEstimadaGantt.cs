using ERP_TECKIO.Modelos;

using System;
using System.Collections.Generic;

namespace ERP_TECKIO;

public partial class ProgramacionEstimadaGantt
{
    public int Id { get; set; }

    public int IdProyecto { get; set; }

    public int IdPrecioUnitario { get; set; }

    public int IdConcepto { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaTermino { get; set; }

    public int Duracion { get; set; }

    public int Progreso { get; set; }

    public int Comando { get; set; }

    public int DesfaseComando { get; set; }

    public int? IdPadre { get; set; }

    public virtual ICollection<DependenciaProgramacionEstimada> Dependencias { get; set; } = new List<DependenciaProgramacionEstimada>();
    public virtual ICollection<DependenciaProgramacionEstimada> DependenciaPredecesora { get; set; } = new List<DependenciaProgramacionEstimada>();

    public virtual Concepto IdConceptoNavigation { get; set; } = null!;

    public virtual PrecioUnitario IdPrecioUnitarioNavigation { get; set; } = null!;

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
}
