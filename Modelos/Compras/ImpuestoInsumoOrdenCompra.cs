
namespace ERP_TECKIO.Modelos
{
    public class ImpuestoInsumoOrdenCompra
    {
        public int Id { get; set; }

        public int IdInsumoOrdenCompra { get; set; }

        public int IdImpuesto { get; set; }

        public decimal Porcentaje { get; set; }

        public decimal Importe { get; set; }

        public virtual TipoImpuesto IdImpuestoNavigation { get; set; } = null!;

        public virtual InsumoXOrdenCompra IdInsumoOrdenCompraNavigation { get; set; } = null!;
    }
}
