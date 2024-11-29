

namespace ERP_TECKIO.Modelos;

public partial class ProgramacionEstimada
{
    public int Id { get; set; }

    public int IdProyecto { get; set; }

    public int IdPrecioUnitario { get; set; }

    public DateTime? Inicio { get; set; }

    public DateTime? Termino { get; set; }

    public int? IdPredecesora { get; set; }

    public int? DiasTranscurridos { get; set; }

    public int? Nivel { get; set; }

    public int? IdPadre { get; set; }

    public string? Predecesor { get; set; }

    public int? IdConcepto { get; set; }
    public decimal? Progreso { get; set; }
    public int Comando { get; set; }
    public int DiasComando { get; set; }
    public virtual Concepto? IdConceptoNavigation { get; set; }

    public virtual PrecioUnitario IdPrecioUnitarioNavigation { get; set; } = null!;

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
}
