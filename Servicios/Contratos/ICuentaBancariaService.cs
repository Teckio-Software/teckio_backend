using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


namespace ERP_TECKIO
{
    /// <summary>
    /// Interfaz de una cuenta bancaria
    /// </summary>
    public interface ICuentaBancariaService<T> where T : DbContext 
    {
        Task<List<CuentaBancariaDTO>> ObtenTodos();
        Task<CuentaBancariaDTO> ObtenXId(int Id);
        Task<List<CuentaBancariaDTO>> ObtenXIdContratista(int IdContratista);
        Task<CuentaBancariaDTO> CrearYObtener(CuentaBancariaDTO modelo);
        Task<bool> Crear(CuentaBancariaDTO modelo);
        Task<bool> Editar(CuentaBancariaDTO modelo);
        Task<bool> Eliminar(int Id);
        
    }
}
