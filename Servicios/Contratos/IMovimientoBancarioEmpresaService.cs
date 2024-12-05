using Microsoft.EntityFrameworkCore;



namespace ERP_TECKIO
{
    public interface IMovimientoBancarioEmpresaService<T> where T : DbContext
    {
        Task<bool> Crear(MBancarioBeneficiarioDTO modelo);
        Task<MBancarioBeneficiarioDTO> CrearYObtener(MBancarioBeneficiarioDTO modelo);
    }
}
