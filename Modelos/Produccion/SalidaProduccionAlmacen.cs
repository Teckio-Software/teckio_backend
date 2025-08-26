namespace ERP_TECKIO.Modelos
{
    public partial class SalidaProduccionAlmacen : SalidaProduccionAlmacenAbstract
    {
        public virtual ICollection<ProductosXsalidaProduccionAlmacen> ProductosXsalidaProduccionAlmacens { get; set; } = new List<ProductosXsalidaProduccionAlmacen>();
        public virtual Almacen IdAlmacenNavigation { get; set; } = null!;

    }

    public abstract class SalidaProduccionAlmacenAbstract
    {
        public int Id { get; set; }

        public int IdAlmacen { get; set; }

        public DateTime FechaEntrada { get; set; }

        public string Recibio { get; set; } = null!;

        public string? Observaciones { get; set; }

    }
}
