using ERP_TECKIO.Modelos;

namespace ERP_TECKIO.DTO.Factura
{
    public class FacturaDTO : FacturaAbstract
    {
        public string RazonSocialCliente { get; set; }
        public string RegimenFiscal { get; set; }
        public string UsoCfdi { get; set; }
        public string MonedaSat { get; set; }
        public string RfcReceptor { get; set; } = null!;
        public string FormaPago { get; set; } = null!;
    }
}
