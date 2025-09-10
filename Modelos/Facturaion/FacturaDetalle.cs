using ERP_TECKIO.Modelos.Facturacion;

namespace ERP_TECKIO.Modelos;

public partial class FacturaDetalle
{
    public int Id { get; set; }

    public int IdFactura { get; set; }

    public decimal Cantidad { get; set; }
    public string Descripcion { get; set; }


    public decimal PrecioUnitario { get; set; }

    public string UnidadSat { get; set; } = null!;

    public decimal Importe { get; set; }

    public decimal Descuento { get; set; }
    public int IdProductoYservicio { get; set; }


    public virtual ICollection<FacturaDetalleImpuesto> FacturaDetalleImpuestos { get; set; } = new List<FacturaDetalleImpuesto>();
    public virtual Factura IdFacturaNavigation { get; set; } = null!;
    public virtual ProductoYservicio IdProductoYservicioNavigation { get; set; } = null!;

}
