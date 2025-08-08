using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IDetalleOrdenVentaService<T> where T : DbContext
    {
        Task<RespuestaDTO> Crear(DetalleOrdenVentaDTO modelo);
        Task<DetalleOrdenVentaDTO> CrearYObtener(DetalleOrdenVentaDTO modelo);
        Task<RespuestaDTO> Editar(DetalleOrdenVentaDTO modelo);
        Task<RespuestaDTO> Eliminar(DetalleOrdenVentaDTO modelo);
        Task<List<DetalleOrdenVentaDTO>> ObtenerXIdOrdenVenta(int id);
    }
}
