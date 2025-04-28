using ERP_TECKIO.DTO.Factura;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface ISubcategoriaProdutoYServicio<T> where T : DbContext
    {
        Task<List<SubcategoriaProductoYServicioDTO>> ObtenerTodos();
        Task<SubcategoriaProductoYServicioDTO> ObtenerXId(int Id);
        Task<SubcategoriaProductoYServicioDTO> CrearYObtener(SubcategoriaProductoYServicioDTO registro);
        Task<RespuestaDTO> Editar(SubcategoriaProductoYServicioDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
