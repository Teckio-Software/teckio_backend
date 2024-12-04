using Microsoft.EntityFrameworkCore;



namespace ERP_TECKIO
{
    public interface IMovimientoBancarioSaldoService<T> where T : DbContext
    {
        Task<bool> Crear(MovimientoBancarioSaldoDTO modelo);
        Task<bool> ActualizarSaldoXIdCuentaBancariaYAnioYMes(MovimientoBancarioSaldoDTO modelo);
        Task<MovimientoBancarioSaldoDTO> ObtenerXIdCuentaBancariaYAnioYMes(MovimientoBancarioSaldoDTO modelo);
        Task<List<MovimientoBancarioSaldoDTO>> ObtenerXIdCuentaBancaria(int IdCuentaBancaria);
    }
}
