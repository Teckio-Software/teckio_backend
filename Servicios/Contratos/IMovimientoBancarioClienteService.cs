using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IMovimientoBancarioClienteService<T> where T : DbContext
    {
        Task<bool> Crear(MBancarioBeneficiarioDTO modelo);
        Task<MBancarioBeneficiarioDTO> CrearYObtener(MBancarioBeneficiarioDTO modelo);
        Task<MBancarioBeneficiarioDTO> ObtenerXIdMovimientoBancario(int IdMovimientoBancario);
        Task<List<MBancarioBeneficiarioDTO>> ObtenerTodos();
    }
}
