
namespace ERP_TECKIO.Modelos;

public partial class CuentaBancaria : CuentaBancariaAbstract
{
    public int IdContratista { get; set; }
    public virtual Contratista? IdContratistaNavigation { get; set; }
    public virtual Banco? IdBancoNavigation { get; set; }
    public virtual ICollection<MovimientoBancarioContratista> MBContratistas { get; set; } = new List<MovimientoBancarioContratista>();
}

public partial class CuentaBancariaCliente : CuentaBancariaAbstract
{
    public int IdCliente { get; set; }
    public virtual Clientes? IdClienteNavigation { get; set; }
    public virtual Banco? IdBancoNavigation { get; set; }
    public virtual ICollection<MovimientoBancarioCliente> MBClientes { get; set; } = new List<MovimientoBancarioCliente>();
}

public partial class CuentaBancariaEmpresa : CuentaBancariaAbstract { 
    public virtual Banco? IdBancoNavigation { get; set; }
    public virtual ICollection<MovimientoBancario> MovimientosBancarios { get; set; } = new List<MovimientoBancario>();
    public virtual ICollection<MovimientoBancarioEmpresa> MBEmpresa { get; set; } = new List<MovimientoBancarioEmpresa>();
    public virtual ICollection<MovimientoBancarioSaldo> MBESaldo { get; set; } = new List<MovimientoBancarioSaldo>();
}

public abstract class CuentaBancariaAbstract {
    public int Id { get; set; }
    public int IdBanco { get; set; }
    public string? NumeroCuenta { get; set; }
    public string NumeroSucursal { get; set; }
    public string Clabe {  get; set; }
    public int TipoMoneda { get; set; }
    public DateTime FechaAlta { get; set; }
}
