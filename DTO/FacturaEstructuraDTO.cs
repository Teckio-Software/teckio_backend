
namespace ERP_TECKIO
{
    public class FacturaEstructuraDTO
    {
        public int IdFactura { get; set; }
        public string SerieCfdi { get; set; }
        public string FolioCfdi { get; set; }
        public string NumeroOrdenCompra { get; set; }
        public string RazonSocial { get; set; }
        public DateTime FechaTimbrado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal TotalIva { get; set; }
        public decimal TotalIsr { get; set; }
        public decimal ImpuestosLocales { get; set; }
        public decimal Total { get; set; }
        //Revalidación
        public bool EstatusAnteriorCfdi { get; set; }
        public bool EstatusActualCfdi { get; set; }
        public DateTime FechaRevalidacion { get; set; }
        //Complemento de pago
        public DateTime FechaComplementoPago { get; set; }
        //REPSE
        public int EstatusRepse { get; set; }
        public List<FacturaComplementoPagoVista> ComplementosPagos { get; set; }
    }

    public class FacturaComplementoPagoVista
    {
        public int IdFactura { get; set; }
        public string Serie { get; set; }
        public string Folio { get; set; }
        public string Uuid { get; set; }
    }
    /// <summary>
    /// Esta clase es para mostrar al usuario el detalle de la factura
    /// </summary>
    public class FacturaTeckioDetallesDTO
    {
        public decimal TotalIVA { get; set; }
        public decimal TotalISR { get; set; }
        public decimal ImpuestosLocales { get; set; }
        public string RazonSocial { get; set; }

        public List<FacturaComplementoPagoVista> ComplementosPagos { get; set; }
    }
}
