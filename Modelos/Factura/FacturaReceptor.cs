
namespace ERP_TECKIO.Modelos;

public partial class FacturaReceptor : FacturaReceptorAbstract
{
    

    public virtual Factura IdFacturaNavigation { get; set; } = null!;
}

public abstract class FacturaReceptorAbstract
{
    public int Id { get; set; }

    public int IdFactura { get; set; }

    public string RegimenFiscalReceptor { get; set; } = null!;

    public string Rfc { get; set; } = null!;

    public string UsoCfdi { get; set; } = null!;
}
