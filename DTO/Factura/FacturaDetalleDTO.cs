using ERP_TECKIO.Modelos;
namespace ERP_TECKIO
{
    public abstract class FacturaDetalleAbstract
    {
        public int Id { get; set; }

        public int IdFactura { get; set; }

        public string Descripcion { get; set; } = null!;

        public decimal Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public string UnidadSat { get; set; } = null!;

        public decimal Importe { get; set; }

        public decimal Descuento { get; set; }
    }
    public class FacturaDetalleDTO : FacturaDetalleAbstract
    {
        
    }
    public class FacturaDetalleCentroCostosDTO : FacturaDetalleAbstract
    {
        public int Tipo { get; set; }
        public string CuentaContable { get; set; }
    }
}
