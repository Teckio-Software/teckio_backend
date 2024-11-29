
namespace ERP_TECKIO.Modelos;

public abstract class FacturaComplementoPagoAbstract
{
    public int Id { get; set; }

    public int IdFactura { get; set; }

    public string Uuid { get; set; } = null!;
    public decimal ImpuestoSaldoInsoluto { get; set; }
    public decimal ImpuestoSaldoAnterior { get; set; }
    public decimal ImpuestoPagado { get; set; }
    public int NumeroParcialidades { get; set; }
}

public partial class FacturaComplementoPago : FacturaComplementoPagoAbstract
{
    public virtual Factura IdFacturaNavigation { get; set; } = null!;
}
