
namespace ERP_TECKIO.Modelos;

public partial class Clientes:ClientesAbstarct
{

    public virtual CuentaContable? IdCuentaAnticiposNavigation { get; set; }

    public virtual CuentaContable? IdCuentaContableNavigation { get; set; }

    public virtual CuentaContable? IdIepsNavigation { get; set; }

    public virtual CuentaContable? IdIvaExentoNavigation { get; set; }

    public virtual CuentaContable? IdIvaGravableNavigation { get; set; }

    public virtual CuentaContable? IdIvaPorTrasladarNavigation { get; set; }

    public virtual CuentaContable? IdIvaRetenidoNavigation { get; set; }

    public virtual CuentaContable? IdIvaTrasladadoNavigation { get; set; }

    public virtual CuentaContable? IdRetensionIsrNavigation { get; set; }

    public ICollection<CuentaBancariaCliente> CuentaBancariaClientes { get; set; } = new List<CuentaBancariaCliente>();
    public ICollection<MovimientoBancarioCliente> MBClientes { get; set; } = new List<MovimientoBancarioCliente>();
    public ICollection<OrdenVentum> OrdenVenta { get; set; } = new List<OrdenVentum>();
    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

}

public abstract class ClientesAbstarct
{
    public int Id { get; set; }

    public string RazonSocial { get; set; } = null!;

    public string Rfc { get; set; } = null!;

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public string? RepresentanteLegal { get; set; }
    public string Domicilio { get; set; } = null!;
    public string Colonia { get; set; } = null!;
    public string Municipio { get; set; } = null!;

    public string? NoExterior { get; set; }

    public string CodigoPostal { get; set; } = null!;

    public int? IdCuentaContable { get; set; }

    public int? IdIvaTrasladado { get; set; }

    public int? IdIvaPorTrasladar { get; set; }

    public int? IdCuentaAnticipos { get; set; }

    public int? IdIvaExento { get; set; }

    public int? IdIvaGravable { get; set; }

    public int? IdRetensionIsr { get; set; }

    public int? IdIeps { get; set; }

    public int? IdIvaRetenido { get; set; }

}