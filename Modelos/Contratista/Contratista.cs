
namespace ERP_TECKIO.Modelos;

public partial class Contratista
{
    public int Id { get; set; }

    public string RazonSocial { get; set; } = null!;

    public string Rfc { get; set; } = null!;

    public bool EsProveedorServicio { get; set; }

    public bool EsProveedorMaterial { get; set; }

    public string RepresentanteLegal { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Domicilio { get; set; }

    public string? NExterior { get; set; }

    public string? Colonia { get; set; }

    public string? Municipio { get; set; }

    public string CodigoPostal { get; set; } = null!;

    public int? IdCuentaContable { get; set; }

    public int? IdIvaAcreditableContable { get; set; }

    public int? IdIvaPorAcreditar { get; set; }

    public int? IdCuentaAnticipos { get; set; }

    public int? IdCuentaRetencionIsr { get; set; }

    public int? IdCuentaRetencionIva { get; set; }

    public int? IdEgresosIvaExento { get; set; }

    public int? IdEgresosIvaGravable { get; set; }

    public int? IdIvaAcreditableFiscal { get; set; }

    
    public virtual CuentaContable? IdCuentaAnticiposNavigation { get; set; }

    public virtual CuentaContable? IdCuentaContableNavigation { get; set; }

    public virtual CuentaContable? IdCuentaRetencionIsrNavigation { get; set; }

    public virtual CuentaContable? IdCuentaRetencionIvaNavigation { get; set; }

    public virtual CuentaContable? IdEgresosIvaExentoNavigation { get; set; }

    public virtual CuentaContable? IdEgresosIvaGravableNavigation { get; set; }

    public virtual CuentaContable? IdIvaAcreditableContableNavigation { get; set; }

    public virtual CuentaContable? IdIvaAcreditableFiscalNavigation { get; set; }

    public virtual CuentaContable? IdIvaPorAcreditarNavigation { get; set; }
    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    public virtual ICollection<Cotizacion>? Cotizacions { get; set; } = new List<Cotizacion>();

    public virtual ICollection<OrdenCompra> OrdenCompras { get; set; } = new List<OrdenCompra>();
    public virtual ICollection<AlmacenEntrada> AlmacenEntradas { get; set; } = new List<AlmacenEntrada>();
    public virtual ICollection<CuentaBancaria> CuentaBancarias { get; set; } = new List<CuentaBancaria>();
    public virtual ICollection<MovimientoBancarioContratista> MBContratistas { get; set; } = new List<MovimientoBancarioContratista>();
}