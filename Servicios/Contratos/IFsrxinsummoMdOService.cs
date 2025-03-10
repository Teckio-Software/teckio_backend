using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IFsrxinsummoMdOService<T> where T : DbContext
    {
        Task<FsrxinsummoMdODTO> CrearYObtener(FsrxinsummoMdODTO objeto);
        Task<bool> Eliminar(int IdFsi);
        Task<bool> Editar(FsrxinsummoMdODTO objeto);
        Task<FsrxinsummoMdODTO> ObtenerXId(int IdFsi);
        Task<FsrxinsummoMdODTO> ObtenerXIdInsumo(int IdInsumo);
        Task<List<FsrxinsummoMdODTO>> ObtenerXIdProyecto(int IdProyecto);
    }
}
