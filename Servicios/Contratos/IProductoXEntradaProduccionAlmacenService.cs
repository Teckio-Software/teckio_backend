using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IProductoXEntradaProduccionAlmacenService<T> where T : DbContext
    {
        Task<RespuestaDTO> Crear(ProductosXEntradaProduccionAlmacenDTO modelo);
        Task<ProductosXEntradaProduccionAlmacenDTO> CrearYObtener(ProductosXEntradaProduccionAlmacenDTO modelo);
        Task<RespuestaDTO> Editar(ProductosXEntradaProduccionAlmacenDTO modelo);
        Task<RespuestaDTO> Eliminar(ProductosXEntradaProduccionAlmacenDTO modelo);
        Task<List<ProductosXEntradaProduccionAlmacenDTO>> ObtenerXIdEntrada(int id);
    }
}
