using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IPrecioUnitarioXEmpleadoService<T> where T : DbContext
    {
        public Task<RespuestaDTO> CrearMultiple(List<PrecioUnitarioXEmpleadoDTO> lista);
        public Task<RespuestaDTO> Crear(PrecioUnitarioXEmpleadoDTO objeto);
        public Task<List<PrecioUnitarioXEmpleadoDTO>> ObtenerXIdEmpleado(int IdEmpleado);
        public Task<List<PrecioUnitarioXEmpleadoDTO>> ObtenerXIdEmpleadoYIdProyceto(int IdEmpleado, int IdProyceto);
    }
}
