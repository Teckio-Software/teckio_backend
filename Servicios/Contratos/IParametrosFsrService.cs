using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IParametrosFsrService<T> where T : DbContext
    {
        Task<ParametrosFsrDTO> ObtenerXIdProyecto(int IdProyecto);
        Task<ParametrosFsrDTO> ObtenXId(int Id);
        Task<ParametrosFsrDTO> CrearYObtener(ParametrosFsrDTO registro);
        Task<RespuestaDTO> Crear(ParametrosFsrDTO registro);
        Task<RespuestaDTO> Editar(ParametrosFsrDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
