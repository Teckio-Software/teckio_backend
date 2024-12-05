namespace ERP_TECKIO.Modelos;

public partial class AlmacenEntrada
{
    public int Id { get; set; }

    public int IdAlmacen { get; set; }

    public int? IdContratista { get; set; }

    public string NoEntrada { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }

    public string PersonaRegistra { get; set; } = null!;

    public string? CodigoCreacion { get; set; }

    public string? Observaciones { get; set; }

    public int? Estatus { get; set; }

    public virtual ICollection<AlmacenEntradaInsumo> AlmacenEntradaInsumos { get; set; } = new List<AlmacenEntradaInsumo>();

    public virtual Almacen IdAlmacenNavigation { get; set; } = null!;

    public virtual Contratista IdContratistaNavigation { get; set; } = null!;
}
