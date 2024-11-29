using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


namespace ERP_TECKIO
{
    public interface IMovimientoBancarioClienteService<T> where T : DbContext
    {
        Task<bool> Crear(MBancarioBeneficiarioDTO modelo);
        Task<MBancarioBeneficiarioDTO> CrearYObtener(MBancarioBeneficiarioDTO modelo);
        Task<List<MBancarioBeneficiarioDTO>> ObtenerTodos();
    }
}
