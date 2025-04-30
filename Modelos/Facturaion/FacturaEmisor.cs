
using ERP_TECKIO.Modelos.Facturacion;

namespace ERP_TECKIO.Modelos;

public partial class FacturaEmisor : FacturaEmisorAbstract
{
    

    public virtual Factura IdFacturaNavigation { get; set; } = null!;
    public virtual RegimenFiscalSat IdRegimenFiscalSatNavigation { get; set; } = null!;
}

public abstract class FacturaEmisorAbstract
{
    public int Id { get; set; }

    public int IdFactura { get; set; }

    public int IdRegimenFiscalSat { get; set; }

    public string Rfc { get; set; } = null!;
    public string RegimenFiscal = null!;
}
