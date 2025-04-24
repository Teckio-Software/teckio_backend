using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface ICategoriaImpuestoService<TContext> where TContext : DbContext
    {
        Task<List<CategoriaImpuestoDTO>> ObtenerTodos();
        Task<CategoriaImpuestoDTO> ObtenerXId(int Id);
        Task<CategoriaImpuestoDTO> CrearYObtener(CategoriaImpuestoDTO registro);
        Task<RespuestaDTO> Editar(CategoriaImpuestoDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
