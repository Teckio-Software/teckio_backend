
using ERP_TECKIO.Modelos.Facturacion;

namespace ERP_TECKIO.Modelos;

public partial class DetalleOrdenVentum : DetalleOrdenVentaAbstract
{
    public virtual Estimaciones? IdEstimacionNavigation { get; set; }

    public virtual OrdenVentum IdOrdenVentaNavigation { get; set; } = null!;

    public virtual ProductoYservicio IdProductoYservicioNavigation { get; set; } = null!;

    public virtual ICollection<ImpuestoDetalleOrdenVentum> ImpuestoDetalleOrdenVenta { get; set; } = new List<ImpuestoDetalleOrdenVentum>();
}

public abstract class DetalleOrdenVentaAbstract
{
    public int Id { get; set; }

    public int IdOrdenVenta { get; set; }

    public int IdProductoYservicio { get; set; }

    public int? IdEstimacion { get; set; }

    public decimal Cantitdad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Descuento { get; set; }

    public decimal ImporteTotal { get; set; }
}
