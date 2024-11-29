
namespace ERP_TECKIO.Modelos;

public partial class Banco : BancoAbstract
{
   public virtual ICollection<CuentaBancaria> CuentaBancarias { get; set; } = new List<CuentaBancaria>();
    public ICollection<CuentaBancariaCliente> CuentaBancariaClientes { get; set; } = new List<CuentaBancariaCliente>();
    public ICollection<CuentaBancariaEmpresa> cuentaBancariaEmpresas { get; set; } = new List<CuentaBancariaEmpresa>();

}

public abstract class BancoAbstract {
    public int Id { get; set; }

    public string Clave { get; set; }

    public string? Nombre { get; set; }

    public string? RazonSocial { get; set; }
}