
using ERP_TECKIO.Modelos.Contabilidad;

namespace ERP_TECKIO.Modelos;

public partial class MovimientoBancario : MovimientoBancarioAbstract
{
    public virtual Poliza? IdPolizaNavigation { get; set; }

    public virtual Factura? IdFacturaNavigation { get; set; }

    public virtual CuentaBancariaEmpresa? IdCuentaBancariaEmpresaNavigation { get; set; }

    public ICollection<MovimientoBancarioContratista> MBContratistas { get; set; } = new List<MovimientoBancarioContratista>();
    public ICollection<MovimientoBancarioCliente> MBClientes { get; set; } = new List<MovimientoBancarioCliente>();
    public ICollection<MovimientoBancarioEmpresa> MBEmpresa { get; set; } = new List<MovimientoBancarioEmpresa>();
    public ICollection<OrdenCompraXMovimientoBancario> OrdenCompraXMovimientoBancarios { get; set; } = new List<OrdenCompraXMovimientoBancario>();
    public ICollection<FacturaXOrdenCompraXMovimientoBancario> FacturaXOrdenCompraXMovimientoBancarios { get; set; } = new List<FacturaXOrdenCompraXMovimientoBancario>();
    public ICollection<OrdenVentaXMovimientoBancario> OrdenVentaXMovimientoBancarios { get; set; } = new List<OrdenVentaXMovimientoBancario>();
    public ICollection<FacturaXOrdenVentaXMovimientoBancario> FacturaXOrdenVentaXMovimientoBancarios { get; set; } = new List<FacturaXOrdenVentaXMovimientoBancario>(); 
}

public abstract class MovimientoBancarioAbstract{
    public int Id { get; set; }
    public int? IdPoliza { get; set; }
    public int? IdFactura { get; set; }
    public int IdCuentaBancariaEmpresa { get; set; }
    public string NoMovimientoBancario { get; set; }
    public string Folio { get; set; }
    public DateTime? FechaAlta { get; set; }
    public DateTime FechaAplicacion { get; set; }
    public DateTime? FechaCobra { get; set; }
    public int Modalidad { get; set; }
    public int TipoDeposito { get; set; }
    public decimal MontoTotal { get; set; }
    public string Concepto { get; set; }
    public decimal TipoCambio { get; set; }
    public int Moneda { get; set; }
    public int Estatus { get; set; }
    public int TipoBeneficiario { get; set; }
}
