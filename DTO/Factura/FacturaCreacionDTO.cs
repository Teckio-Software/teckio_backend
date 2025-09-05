namespace ERP_TECKIO.DTO.Factura
{
    public class FacturaCreacionDTO
    {
        public List<int> IdOrdenesVenta { get; set; } = new List<int>();
        public string RFCEmisor { get; set; }
        public int Tipo { get; set; }
        public int Modalidad { get; set; }
        public string MetodoPago { get; set; }
        public string CodigoPostal { get; set; }
        public int TipoCambio { get; set; }
        public int IdCliente { get; set; }
        public int IdFormaPago { get; set; }
        public int IdRegimenFiscalSat { get; set; }
        public int IdUsoCfdi { get; set; }
        public int IdMonedaSat { get; set; }
    }
}
