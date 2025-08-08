using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IImpuestoDetalleOrdenVentaService<T> where T : DbContext
    {
        Task<RespuestaDTO> Crear(ImpuestoDetalleOrdenVentaDTO modelo);
        Task<OrdenVentaDTO> CrearYObtener(ImpuestoDetalleOrdenVentaDTO modelo);
        Task<RespuestaDTO> Editar(ImpuestoDetalleOrdenVentaDTO modelo);
        Task<RespuestaDTO> Eliminar(ImpuestoDetalleOrdenVentaDTO modelo);
        Task<List<ImpuestoDetalleOrdenVentaDTO>> ObtenerXIdDetalle(int id);
    }
}
