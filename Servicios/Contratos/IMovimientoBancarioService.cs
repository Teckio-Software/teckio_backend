using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


namespace ERP_TECKIO
{
    /// <summary>
    /// Interfaz para los movimientos bancarios
    /// </summary>
    public interface IMovimientoBancarioService<T> where T : DbContext
    {
        Task<bool> Crear(MovimientoBancarioTeckioDTO modelo);
        Task<MovimientoBancarioTeckioDTO> CrearYObtener(MovimientoBancarioTeckioDTO modelo);
        Task<List<MovimientoBancarioTeckioDTO>> ObtenerXIdCuentaBancaria(int IdCuentaBancaria);
        Task<List<MovimientoBancarioTeckioDTO>> ObtenerTodos();
        Task<MovimientoBancarioTeckioDTO> ObtenerXId(int Id);
        Task<bool> ActualizarEstatusXId(int IdMovimientoBancario, int estatus);
        Task<decimal> ObtenerMontoXIdCuentaBancariaYAnioYMes(DateTime fecha, int IdCuentaBancaria);
    }
}
