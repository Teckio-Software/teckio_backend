using ERP_TECKIO.Modelos;
namespace ERP_TECKIO
{
    //public abstract class FacturaDetalleAbstract
    //{
    //    public int Id { get; set; }

    //    public int IdFactura { get; set; }

    //    public string Descripcion { get; set; } = null!;

    //    public decimal Cantidad { get; set; }

    //    public decimal PrecioUnitario { get; set; }

    //    public string UnidadSat { get; set; } = null!;

    //    public decimal Importe { get; set; }

    //    public decimal Descuento { get; set; }
    //}
    public class FacturaDetalleDTO
    {
        public int Id { get; set; }

        public int IdFactura { get; set; }

        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public string UnidadSat { get; set; } = null!;

        public decimal Importe { get; set; }

        public decimal Descuento { get; set; }
        public int IdProductoYservicio { get; set; }
        public int IdCategoriaProductoYServicio { get; set; }
        public int IdSubcategoriaProductoYServicio { get; set; }
    }
    //public class FacturaDetalleCentroCostosDTO : FacturaDetalleAbstract
    //{
    //    public int Tipo { get; set; }
    //    public string CuentaContable { get; set; }
    //}
}
