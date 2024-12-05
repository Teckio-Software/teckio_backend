using Microsoft.EntityFrameworkCore;



namespace ERP_TECKIO
{
    public interface ICuentaBancariaClienteService<T> where T : DbContext
    {
        Task<List<CuentaBancariaClienteDTO>> ObtenTodos();
        Task<CuentaBancariaClienteDTO> ObtenXId(int Id);
        Task<List<CuentaBancariaClienteDTO>> ObtenXIdCliente(int IdCliente);
        Task<CuentaBancariaClienteDTO> CrearYObtener(CuentaBancariaClienteDTO modelo);
        Task<bool> Crear(CuentaBancariaClienteDTO modelo);
        Task<bool> Editar(CuentaBancariaClienteDTO modelo);
        Task<bool> Eliminar(int Id);
    }
}
