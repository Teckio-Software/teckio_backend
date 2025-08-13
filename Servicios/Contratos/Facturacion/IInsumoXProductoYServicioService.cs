using ERP_TECKIO.DTO.Factura;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface IInsumoXProductoYServicioService<T> where T: DbContext
    {
        Task<RespuestaDTO> Crear(InsumoXProductoYServicioDTO parametro);
        Task<RespuestaDTO> Editar(InsumoXProductoYServicioDTO parametro);
        Task<RespuestaDTO> Eliminar(InsumoXProductoYServicioDTO parametro);
        Task<List<InsumoXProductoYServicioDTO>> ObtenerPorIdPrdYSer(int id);

    }
}
