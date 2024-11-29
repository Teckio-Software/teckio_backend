
namespace ERP_TECKIO.Modelos;

public partial class FacturaEmisor : FacturaEmisorAbstract
{
    

    public virtual Factura IdFacturaNavigation { get; set; } = null!;
}

public abstract class FacturaEmisorAbstract
{
    public int Id { get; set; }

    public int IdFactura { get; set; }

    public string RegimenFiscal { get; set; } = null!;

    public string Rfc { get; set; } = null!;
}
