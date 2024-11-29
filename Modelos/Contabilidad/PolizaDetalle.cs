
namespace ERP_TECKIO.Modelos
{
    public partial class PolizaDetalle
    {
        public int Id { get; set; }
        public int IdPoliza { get; set; }
        public int IdCuentaContable { get; set; }
        public string? Concepto { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
        public virtual Poliza IdPolizaNavigation { get; set; } = null!;
        public virtual CuentaContable IdCuentaContableNavigation { get; set; } = null!;
    }
}
