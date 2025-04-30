using ERP_TECKIO.DTO;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface IFacturaImpuestosService<TContext> where TContext : DbContext
    {
        Task<List<FacturaImpuestosDTO>> ObtenerTodos();
        Task<FacturaImpuestosDTO> ObtenerXId(int Id);
        Task<FacturaImpuestosDTO> CrearYObtener(FacturaImpuestosDTO registro);
        Task<RespuestaDTO> Editar(FacturaImpuestosDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
