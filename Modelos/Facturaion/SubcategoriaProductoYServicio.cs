using ERP_TECKIO.Modelos.Facturacion;

namespace ERP_TECKIO.Modelos.Facturaion
{
    public class SubcategoriaProductoYServicioAbstract
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }

    public class SubcategoriaProductoYServicio : SubcategoriaProductoYServicioAbstract
    {
        public virtual ICollection<ProductoYservicio> PorductoYservicios { get; set; } = new List<ProductoYservicio>();
    }
}
