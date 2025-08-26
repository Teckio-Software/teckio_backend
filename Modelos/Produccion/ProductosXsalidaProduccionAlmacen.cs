using ERP_TECKIO.Modelos.Facturacion;

namespace ERP_TECKIO.Modelos
{
    public partial class ProductosXsalidaProduccionAlmacen : ProductosXsalidaProduccionAlmacenAbstract
    {
        public virtual ProductoYservicio IdProductoYservicioNavigation { get; set; } = null!;

        public virtual SalidaProduccionAlmacen IdSalidaProduccionAlmacenNavigation { get; set; } = null!;
    }
    public abstract class ProductosXsalidaProduccionAlmacenAbstract
    {
        public int Id { get; set; }

        public int IdSalidaProduccionAlmacen { get; set; }

        public int IdProductoYservicio { get; set; }

        public decimal Cantidad { get; set; }

        public int TipoOrigen { get; set; }

    }
}
