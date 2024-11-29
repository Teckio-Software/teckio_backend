using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


namespace ERP_TECKIO
{
    public interface ICuentaBancariaEmpresaService<T> where T : DbContext
    {
        Task<List<CuentaBancariaEmpresasDTO>> ObtenTodos();
        Task<CuentaBancariaEmpresasDTO> ObtenXId(int Id);
        Task<CuentaBancariaEmpresasDTO> CrearYObtener(CuentaBancariaEmpresasDTO modelo);
        Task<bool> Crear(CuentaBancariaEmpresasDTO modelo);
        Task<bool> Editar(CuentaBancariaEmpresasDTO modelo);
        Task<bool> Eliminar(int Id);
    }
}
