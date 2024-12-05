
namespace ERP_TECKIO.Modelos;

public partial class SaldosBalanzaComprobacion
{
    public int Id { get; set; }

    public int IdCuentaContable { get; set; }

    public int Mes { get; set; }

    public int Anio { get; set; }

    public decimal SaldoInicial { get; set; }

    public decimal SaldoFinal { get; set; }

    public decimal Debe { get; set; }

    public decimal Haber { get; set; }

    public bool EsUltimo { get; set; }

    public virtual CuentaContable IdCuentaContableNavigation { get; set; } = null!;
}
