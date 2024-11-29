
namespace ERP_TECKIO
{
    public class PolizaDetalleDTO
    {
        public int Id { get; set; }
        public int IdPoliza { get; set; }
        public int IdCuentaContable { get; set; }
        public string? CuentaContableCodigo { get; set; }
        public string? Concepto { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
    }
}
