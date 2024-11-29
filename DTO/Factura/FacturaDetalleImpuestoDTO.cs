
namespace ERP_TECKIO
{
    public class FacturaDetalleImpuestoDTO
    {
        public int Id { get; set; }

        public int IdFacturaDetalle { get; set; }

        public int IdTipoImpuesto { get; set; }

        public int IdTipoFactor { get; set; }

        public int IdClasificacionImpuesto { get; set; }

        public decimal Base { get; set; }

        public decimal Importe { get; set; }

        public decimal TasaCuota { get; set; }
        public int IdCategoriaImpuesto { get; set; }
    }
}
