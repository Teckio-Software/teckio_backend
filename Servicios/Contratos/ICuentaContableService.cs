
using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    /// <summary>
    /// Interfaz para las cuentas contables
    /// </summary>
    public interface ICuentaContableService<T> where T : DbContext
    {
        Task<List<CuentaContableDTO>> ObtenTodos();
        Task<List<CuentaContableDTO>> ObtenAsignables();
        Task<CuentaContableDTO> ObtenXId(int Id);
        Task<CuentaContableDTO> Crear(CuentaContableCreacionDTO modelo);
        Task<RespuestaDTO> Editar(CuentaContableDTO modelo);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
