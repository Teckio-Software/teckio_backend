
namespace ERP_TECKIO
{
    public class MovimientoBancarioSaldoDTO
    {
        public int Id { get; set; }
        public int IdCuentaBancaria { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public decimal MontoFinal { get; set; }
    }
}
