
namespace ERP_TECKIO.Modelos;

public partial class Requisicion
{
    public int Id { get; set; }

    public int? IdProyecto { get; set; }

    public string NoRequisicion { get; set; } = null!;

    public string? PersonaSolicitante { get; set; }

    public string? Observaciones { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int EstatusRequisicion { get; set; }

    public int? EstatusInsumosComprados { get; set; }

    public int? EstatusInsumosSurtidos { get; set; }

    public string? CodigoBusqueda { get; set; }

    public string? Residente { get; set; }

    public virtual ICollection<AlmacenEntradaInsumo> AlmacenEntradaInsumos { get; set; } = new List<AlmacenEntradaInsumo>();

    public virtual ICollection<CompraDirecta> CompraDirecta { get; set; } = new List<CompraDirecta>();

    public virtual ICollection<Cotizacion> Cotizacions { get; set; } = new List<Cotizacion>();

    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual ICollection<InsumoXRequisicion> InsumoXrequisicions { get; set; } = new List<InsumoXRequisicion>();

    public virtual ICollection<OrdenCompra> OrdenCompras { get; set; } = new List<OrdenCompra>();
}
