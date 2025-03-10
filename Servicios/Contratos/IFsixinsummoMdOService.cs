using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IFsixinsummoMdOService<T> where T : DbContext
    {
        Task<FsixinsummoMdODTO> CrearYObtener(FsixinsummoMdODTO objeto);
        Task<bool> Eliminar(int IdFsi);
        Task<bool> Editar(FsixinsummoMdODTO objeto);
        Task<FsixinsummoMdODTO> ObtenerXId(int IdFsi);
        Task<FsixinsummoMdODTO> ObtenerXIdInsumo(int IdInsumo);
        Task<List<FsixinsummoMdODTO>> ObtenerXIdProyecto(int IdProyecto);
    }
}
