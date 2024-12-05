
namespace ERP_TECKIO.Modelos;

public partial class AcuseValidacion
{
    public int Id { get; set; }

    public int IdFactura { get; set; }

    public int IdUsuario { get; set; }

    public string Folio { get; set; } = null!;

    public bool Estatus { get; set; }

    public DateTime Fecha { get; set; }

    public virtual ICollection<DetalleValidacion> DetalleValidacions { get; set; } = new List<DetalleValidacion>();

    public virtual Factura IdFacturaNavigation { get; set; } = null!;
}
