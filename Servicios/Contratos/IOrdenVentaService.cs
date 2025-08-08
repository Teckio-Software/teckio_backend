using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IOrdenVentaService<T> where T : DbContext
    {
        Task<RespuestaDTO> Crear(OrdenVentaDTO modelo);
        Task<OrdenVentaDTO> CrearYObtener(OrdenVentaDTO modelo);
        Task<List<OrdenVentaDTO>> ObtenerTodos();
        Task<RespuestaDTO> Editar(OrdenVentaDTO modelo);
        Task<RespuestaDTO> Eliminar(OrdenVentaDTO modelo);
    }
}