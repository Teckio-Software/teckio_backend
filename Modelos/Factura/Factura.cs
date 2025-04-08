
namespace ERP_TECKIO.Modelos;

public partial class Factura : FacturaAbstract
{
    public virtual ICollection<AcuseValidacion> AcuseValidacions { get; set; } = new List<AcuseValidacion>();
    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();
    public virtual ICollection<FacturaImpuesto> FacturaImpuestos { get; set; } = new List<FacturaImpuesto>();
    public virtual Archivo IdArchivoNavigation { get; set; } = null!;
    public virtual Archivo? IdArchivoPdfNavigation { get; set; } = null!;
    public virtual ICollection<PolizaProveedores> PolizasProveedores {  get; set; } = new List<PolizaProveedores>();
    public virtual ICollection<FacturaImpuestosLocal> FacturaImpuestosLocales { get; set; } = new List<FacturaImpuestosLocal>();
    public virtual ICollection<FacturaComplementoPago> FacturaComplementoPagos { get; set; } = new List<FacturaComplementoPago>();
    public virtual ICollection<FacturaEmisor> FacturaEmisors { get; set; } = new List<FacturaEmisor>();
    public virtual ICollection<FacturaReceptor> FacturaReceptors { get; set; } = new List<FacturaReceptor>();
    public virtual ICollection<MovimientoBancario> MovimientosBancarios { get; set; } = new List<MovimientoBancario>();
    public virtual Clientes IdClienteNavigation { get; set; } = null!;

    public virtual FormaPagoSat IdFormaPagoNavigation { get; set; } = null!;

    public virtual MonedaSat IdMonedaSatNavigation { get; set; } = null!;

    public virtual RegimenFiscalSat IdRegimenFiscalSatNavigation { get; set; } = null!;

    public virtual UsoCfdiSat IdUsoCfdiNavigation { get; set; } = null!;
}

public abstract class FacturaAbstract
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public DateTime FechaValidacion { get; set; }

    public DateTime FechaTimbrado { get; set; }

    public DateTime FechaEmision { get; set; }

    public string RfcEmisor { get; set; } = null!;

    public decimal Subtotal { get; set; }

    public decimal Total { get; set; }

    public string SerieCfdi { get; set; } = null!;

    public string FolioCfdi { get; set; } = null!;

    public int Estatus { get; set; }

    public int Tipo { get; set; }

    public int Modalidad { get; set; }

    public long IdArchivo { get; set; }

    public string MetodoPago { get; set; } = null!;

    public decimal Descuento { get; set; }

    public long? IdArchivoPdf { get; set; }

    public bool? EstatusEnviadoCentroCostos { get; set; }

    public string VersionFactura { get; set; } = null!;

    public string CodigoPostal { get; set; } = null!;

    public decimal TipoCambio { get; set; }

    public string FormaPago { get; set; } = null!;
    public string Moneda { get; set; } = null!;
    public string RfcReceptor { get; set; } = null!;

    public int IdCliente { get; set; }

    public int IdFormaPago { get; set; }

    public int IdRegimenFiscalSat { get; set; }

    public int IdUsoCfdi { get; set; }

    public int IdMonedaSat { get; set; }

}
