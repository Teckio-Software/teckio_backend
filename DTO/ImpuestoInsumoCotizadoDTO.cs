
namespace ERP_TECKIO
{
    public class ImpuestoInsumoCotizadoDTO
    {
        public int Id { get; set; }
        public int IdInsumoCotizado { get; set; }
        public int IdImpuesto { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Importe { get; set; }

    }
    public class ImpuestoInsumoCotizadoCreacionDTO
    {
        public int IdInsumoCotizado { get; set; }
        public int IdImpuesto { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Importe { get; set; }

    }
}
