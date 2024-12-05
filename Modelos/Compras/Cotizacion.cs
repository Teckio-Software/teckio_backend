
namespace ERP_TECKIO.Modelos;

public partial class Cotizacion
{
    public int Id { get; set; }

    public int? IdProyecto { get; set; }

    public int? IdRequisicion { get; set; }

    public int? IdContratista { get; set; }

    public string NoCotizacion { get; set; } = null!;

    public string? Observaciones { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string? PersonaAutorizo { get; set; }

    public string? PersonaCompra { get; set; }

    public int EstatusCotizacion { get; set; }

    public int? EstatusInsumosComprados { get; set; }

    public decimal? ImporteSinIva { get; set; }

    public decimal? MontoDescuento { get; set; }

    public decimal? ImporteConIva { get; set; }

    public virtual Contratista? IdContratistaNavigation { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual Requisicion? IdRequisicionNavigation { get; set; }

    public virtual ICollection<InsumoXCotizacion> InsumoXcotizacions { get; set; } = new List<InsumoXCotizacion>();

    public virtual ICollection<OrdenCompra> OrdenCompras { get; set; } = new List<OrdenCompra>();
}
