
namespace ERP_TECKIO.Modelos
{
    public class MovimientoBancarioSaldo
    {
        public int Id { get; set; }
        public int IdCuentaBancaria { get; set; }
        public int Anio { get; set; }
        public int Mes {  get; set; }
        public decimal MontoFinal {  get; set; }

        public virtual CuentaBancariaEmpresa? IdCuentaBancariaNavigation { get; set; }
    }
}
