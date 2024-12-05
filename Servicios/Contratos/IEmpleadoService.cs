using ERP_TECKIO.DTO.Usuario;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IEmpleadoService<T> where T : DbContext
    {
        public Task<RespuestaDTO> Crear(EmpleadoDTO objeto);
        public Task<RespuestaDTO> Editar(EmpleadoDTO objeto);
        public Task<List<EmpleadoDTO>> ObtenerTodos();
        public Task<EmpleadoDTO> ObtenerXId(int IdEmpleado);
        public Task<EmpleadoDTO> ObtenerXIdUser(int IdUser);
        public Task<RespuestaDTO> Eliminar(int IdEmpleado);
    }
}
