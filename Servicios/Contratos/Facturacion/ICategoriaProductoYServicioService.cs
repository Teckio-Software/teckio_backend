using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface ICategoriaProductoYServicioService<TContext> where TContext : DbContext
    {
        Task<List<CategoriaProductoYServicioDTO>> ObtenerTodos();
        Task<CategoriaProductoYServicioDTO> ObtenerXId(int Id);
        Task<CategoriaProductoYServicioDTO> CrearYObtener(CategoriaProductoYServicioDTO registro);
        Task<RespuestaDTO> Editar(CategoriaProductoYServicioDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
