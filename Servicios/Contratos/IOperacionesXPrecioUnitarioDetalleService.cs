using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IOperacionesXPrecioUnitarioDetalleService<T> where T : DbContext
    {
        Task<List<OperacionesXPrecioUnitarioDetalleDTO>> ObtenerXIdPrecioUnitarioDetalle(int Id);
        Task<OperacionesXPrecioUnitarioDetalleDTO> ObtenerXId(int Id);
        Task<OperacionesXPrecioUnitarioDetalleDTO> CrearYObtener(OperacionesXPrecioUnitarioDetalleDTO registro);
        Task<RespuestaDTO> Editar(OperacionesXPrecioUnitarioDetalleDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
        Task<RespuestaDTO> EliminarXIdPrecioUnitarioDetalle(int IdPrecioUnitarioDetalle);
    }
}
