using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IFsixinsummoMdOdetalleService<T> where T : DbContext
    {
        Task<FsixinsummoMdOdetalleDTO> CrearYObtener(FsixinsummoMdOdetalleDTO objeto);
        Task<bool> Crear(FsixinsummoMdOdetalleDTO objeto);
        Task<bool> Eliminar(int IdFsiDeatalle);
        Task<bool> Editar(FsixinsummoMdOdetalleDTO objeto);
        Task<FsixinsummoMdOdetalleDTO> ObtenerXId(int IdFsiDetalle);
        Task<List<FsixinsummoMdOdetalleDTO>> ObtenerXIdFsi(int IdFsi);
    }
}
