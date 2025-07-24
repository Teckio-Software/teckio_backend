using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IPorcentajeCesantiaEdadService<T> where T : DbContext
    {
        Task<List<PorcentajeCesantiaEdadDTO>> ObtenerXIdProyecto(int IdProyecto);
        Task<PorcentajeCesantiaEdadDTO> ObtenXId(int Id);
        Task<PorcentajeCesantiaEdadDTO> CrearYObtener(PorcentajeCesantiaEdadDTO registro);
        Task<RespuestaDTO> Crear(PorcentajeCesantiaEdadDTO registro);
        Task<RespuestaDTO> Editar(PorcentajeCesantiaEdadDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
