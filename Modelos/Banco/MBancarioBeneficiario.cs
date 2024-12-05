
namespace ERP_TECKIO.Modelos
{
    public abstract class MBancarioBeneficiario
    {
        public int Id { get; set; }
        public int IdBeneficiario { get; set; }
        public int IdCuentaBancaria { get; set; }
        public int IdMovimientoBancario { get; set; }

    }

    public partial class MovimientoBancarioContratista : MBancarioBeneficiario
    {
        public int IdContratista { get; set; }
        public virtual MovimientoBancario? IdMovimientoBancarioNavigation { get; set; }
        public virtual CuentaBancaria? IdCunetaBancariaNavigation { get; set; }
        public virtual Contratista? IdContratistaNavigation { get; set; }
    }

    public partial class MovimientoBancarioCliente : MBancarioBeneficiario
    {
        public int IdCliente { get; set; }
        public virtual MovimientoBancario? IdMovimientoBancarioNavigation { get; set; }
        public virtual CuentaBancariaCliente? IdCuentaBancariaClienteNavigation { get; set; }
        public virtual Clientes? IdClienteNavigation { get; set; }
    }

    public partial class MovimientoBancarioEmpresa : MBancarioBeneficiario
    {
        public virtual MovimientoBancario? IdMovimientoBancarioNavigation { get; set; }
        public virtual CuentaBancariaEmpresa? IdCuentaBancariaEmpresaNavigation { get; set; }
    }
}
