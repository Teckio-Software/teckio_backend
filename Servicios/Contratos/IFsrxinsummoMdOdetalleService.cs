using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IFsrxinsummoMdOdetalleService<T> where T : DbContext
    {
        Task<FsrxinsummoMdOdetalleDTO> CrearYObtener(FsrxinsummoMdOdetalleDTO objeto);
        Task<bool> Crear(FsrxinsummoMdOdetalleDTO objeto);
        Task<bool> Eliminar(int IdFsrDeatalle);
        Task<bool> Editar(FsrxinsummoMdOdetalleDTO objeto);
        Task<FsrxinsummoMdOdetalleDTO> ObtenerXId(int IdFsrDetalle);
        Task<List<FsrxinsummoMdOdetalleDTO>> ObtenerXIdFsr(int IdFsr);
    }
}
