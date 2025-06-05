using ERP_TECKIO.Modelos;
using ERP_TECKIO.Modelos.Facturaion;

namespace ERP_TECKIO.DTO.Factura
{
    public class FacturaImpuestosDTO
    {
        public int Id { get; set; }

        public int IdCategoriaImpuesto { get; set; }

        public int IdFactura { get; set; }

        public int IdClasificacionImpuesto { get; set; }
        public int IdTipoImpuesto { get; set; }


        public decimal TotalImpuesto { get; set; }

    }
}
