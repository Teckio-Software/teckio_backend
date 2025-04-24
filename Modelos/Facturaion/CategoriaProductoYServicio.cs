using ERP_TECKIO.Modelos.Facturacion;

namespace ERP_TECKIO.Modelos.Facturaion
{
    public class CategoriaProductoYServicioAbstract
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }

    public class CategoriaProductoYServicio: CategoriaProductoYServicioAbstract
    {
        public virtual ICollection<ProductoYservicio> PorductoYservicios { get; set; } = new List<ProductoYservicio>();
    }
}
