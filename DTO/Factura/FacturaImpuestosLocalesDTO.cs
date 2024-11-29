

namespace ERP_TECKIO
{
    public class FacturaImpuestosLocalesDTO
    {
        public int Id { get; set; }

        public int IdFactura { get; set; }

        public int IdCategoriaImpuesto { get; set; }

        public int IdClasificacionImpuesto { get; set; }

        public decimal TotalImpuesto { get; set; }

        public string DescripcionImpuestoLocal { get; set; } = null!;
    }
}
