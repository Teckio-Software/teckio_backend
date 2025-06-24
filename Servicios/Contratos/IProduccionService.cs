using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IProduccionService<T> where T : DbContext
    {
        Task<RespuestaDTO> Crear(ProduccionDTO modelo);
        Task<ProduccionDTO> CrearYObtener(ProduccionDTO modelo);
        Task<RespuestaDTO> Editar(ProduccionDTO modelo);
        Task<RespuestaDTO> Eliminar(ProduccionDTO modelo);
    }
}
