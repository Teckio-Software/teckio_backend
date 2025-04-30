using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface IClasificacionImpuestoService<TContext> where TContext : DbContext
    {
        Task<List<ClasificacionImpuestoDTO>> ObtenerTodos();
        Task<ClasificacionImpuestoDTO> ObtenerXId(int Id);
        Task<ClasificacionImpuestoDTO> CrearYObtener(ClasificacionImpuestoDTO registro);
        Task<RespuestaDTO> Editar(ClasificacionImpuestoDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
