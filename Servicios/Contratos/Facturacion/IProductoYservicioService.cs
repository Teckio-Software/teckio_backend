using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface IProductoYservicioService<TContext> where TContext : DbContext
    {
        Task<List<ProductoYservicioDTO>> ObtenerTodos();
        Task<ProductoYservicioDTO> ObtenerXId(int Id);
        Task<ProductoYservicioDTO> ObtenerXDescripcionYClave(string descripcion, int Idclave);
        Task<ProductoYservicioDTO> CrearYObtener(ProductoYservicioDTO registro);
        Task<RespuestaDTO> Crear(ProductoYservicioDTO registro);
        Task<RespuestaDTO> Editar(ProductoYservicioDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
