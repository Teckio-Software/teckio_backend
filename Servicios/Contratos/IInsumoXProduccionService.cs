using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IInsumoXProduccionService<T> where T : DbContext
    {
        Task<RespuestaDTO> Crear(InsumoXProduccionDTO modelo);
        Task<InsumoXProduccionDTO> CrearYObtener(InsumoXProduccionDTO modelo);
        Task<RespuestaDTO> Editar(InsumoXProduccionDTO modelo);
        Task<RespuestaDTO> Eliminar(int id);
        Task<InsumoXProduccionDTO> ObtenerXId(int id);
    }
}
