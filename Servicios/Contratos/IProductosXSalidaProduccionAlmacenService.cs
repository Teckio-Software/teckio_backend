using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IProductosXSalidaProduccionAlmacenService<T> where T : DbContext
    {
        Task<RespuestaDTO> Crear(ProductosXsalidaProduccionAlmacenDTO parametro);
        Task<ProductosXsalidaProduccionAlmacenDTO> CrearYObtener(ProductosXsalidaProduccionAlmacenDTO parametro);
        Task<RespuestaDTO> Editar(ProductosXsalidaProduccionAlmacenDTO parametro);
        Task<RespuestaDTO> Eliminar(int parametro);
        Task<List<ProductosXsalidaProduccionAlmacenDTO>> ObtenerXSalida(int idSalida);

    }
}
