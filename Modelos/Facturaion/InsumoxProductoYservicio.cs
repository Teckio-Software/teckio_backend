using ERP_TECKIO.Modelos.Facturacion;

namespace ERP_TECKIO.Modelos.Facturaion
{
    public class InsumoxProductoYservicio : InsumoxProductoYservicioAbstract
    {
        public virtual InsumoXproduccion IdInsumoNavigation { get; set; } = null!;

        public virtual ProductoYservicio IdProductoYservicioNavigation { get; set; } = null!;
    }

    public abstract class InsumoxProductoYservicioAbstract
    {
        public int Id { get; set; }

        public int IdProductoYservicio { get; set; }

        public int IdInsumo { get; set; }

        public decimal Cantidad { get; set; }

        
    }

}
