
namespace ERP_TECKIO.Modelos;

public partial class AlmacenSalida
{
    public int Id { get; set; }

    public int IdAlmacen { get; set; }

    public string CodigoSalida { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public string? CodigoCreacion { get; set; }

    public string? Observaciones { get; set; }

    public int? Estatus { get; set; }

    public string? PersonaSurtio { get; set; }

    public string? PersonaRecibio { get; set; }

    public virtual ICollection<AlmacenSalidaInsumo> AlmacenSalidaInsumos { get; set; } = new List<AlmacenSalidaInsumo>();

    public virtual Almacen IdAlmacenNavigation { get; set; } = null!;
}
