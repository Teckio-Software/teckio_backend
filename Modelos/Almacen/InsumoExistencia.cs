
namespace ERP_TECKIO.Modelos;

public partial class InsumoExistencia
{
    public int Id { get; set; }

    public int IdInsumo { get; set; }

    public int? IdProyecto { get; set; }

    public int IdAlmacen { get; set; }

    public decimal? CantidadExistente { get; set; }

    public decimal? CantidadInsumosAumenta { get; set; }

    public decimal? CantidadInsumosRetira { get; set; }

    public bool EsNoConsumible { get; set; }
        
    public DateTime? FechaRegistro { get; set; }

    public virtual Almacen IdAlmacenNavigation { get; set; } = null!;

    public virtual Insumo IdInsumoNavigation { get; set; } = null!;

    public virtual Proyecto? IdProyectoNavigation { get; set; }
}

public partial class InsumoExistenciaAlumno01 : InsumosExistenciaabstract {
    
    public virtual ICollection<AlmacenSalidaInsumo> AlmacenSalidaInsumos { get; set; } = new List<AlmacenSalidaInsumo>();

    public virtual Almacen IdAlmacenNavigation { get; set; } = null!;

    public virtual Insumo IdInsumoNavigation { get; set; } = null!;

    public virtual Proyecto? IdProyectoNavigation { get; set; }
}

public abstract class InsumosExistenciaabstract
{
    public int Id { get; set; }

    public int IdInsumo { get; set; }

    public int? IdProyecto { get; set; }

    public int IdAlmacen { get; set; }

    public decimal? CantidadInsumosAumenta { get; set; }

    public decimal? CantidadInsumosRetira { get; set; }

    public bool EsNoConsumible { get; set; }
}
