
namespace ERP_TECKIO.Modelos;

public partial class AlmacenEntradaInsumo : AlmacenEntradaInsumoAbstract
{
    public virtual Insumo IdInsumoNavigation { get; set; } = null!;

    public virtual OrdenCompra? IdOrdenCompraNavigation { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual Requisicion? IdRequisicionNavigation { get; set; }

    public virtual AlmacenEntrada IdAlmacenEntradaNavigation { get; set; } = null!;
}

public abstract class AlmacenEntradaInsumoAbstract
{
    public int Id { get; set; }

    public int? IdProyecto { get; set; }

    public int? IdRequisicion { get; set; }

    public int? IdOrdenCompra { get; set; }

    public int IdAlmacenEntrada { get; set; }

    public int IdInsumo { get; set; }

    public decimal CantidadPorRecibir { get; set; }

    public decimal? CantidadRecibida { get; set; }

    public int? Estatus { get; set; }
}
