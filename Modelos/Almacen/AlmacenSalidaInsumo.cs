
namespace ERP_TECKIO.Modelos;

public partial class AlmacenSalidaInsumo
{
    public int Id { get; set; }

    public int IdProyecto { get; set; }

    public int IdAlmacenSalida { get; set; }

    public int IdInsumo { get; set; }

    public decimal CantidadPorSalir { get; set; }

    public int? EstatusInsumo { get; set; }

    public string? PersonaSurtio { get; set; }

    public string? PersonaRecibio { get; set; }

    public bool? EsPrestamo { get; set; }
    public bool? PrestamoFinalizado { get; set; }

    public virtual AlmacenSalida IdAlmacenSalidaNavigation { get; set; } = null!;

    public virtual Insumo IdInsumoNavigation { get; set; } = null!;

    public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
}
