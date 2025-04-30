using ERP_TECKIO.DTO;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface IDetalleValidacionService<TContext> where TContext : DbContext
    {
        Task<List<DetalleValidacionDTO>> ObtenerTodos();
        Task<DetalleValidacionDTO> ObtenerXId(int Id);
        Task<DetalleValidacionDTO> CrearYObtener(DetalleValidacionDTO registro);
        Task<RespuestaDTO> Editar(DetalleValidacionDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
