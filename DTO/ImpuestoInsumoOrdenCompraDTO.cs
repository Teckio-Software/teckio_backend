
namespace ERP_TECKIO
{
    public class ImpuestoInsumoOrdenCompraDTO
    {
        public int Id { get; set; }
        public int IdInsumoOrdenCompra { get; set; }
        public int IdImpuesto { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Importe { get; set; }
        public string DescripcionImpuesto { get; set; }

    }
    public class ImpuestoInsumoOrdenCompraCreacionDTO
    {
        public int IdInsumoOrdenCompra { get; set; }
        public int IdImpuesto { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Importe { get; set; }

    }
}
