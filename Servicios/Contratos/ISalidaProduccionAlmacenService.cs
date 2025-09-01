using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface ISalidaProduccionAlmacenService<T> where T : DbContext
    {
        Task<RespuestaDTO> Crear(SalidaProduccionAlmacenDTO parametro);
        Task<SalidaProduccionAlmacenDTO> CrearYObtener(SalidaProduccionAlmacenDTO parametro);
        Task<RespuestaDTO> Editar(SalidaProduccionAlmacenDTO parametro);
        Task<RespuestaDTO> Eliminar(int parametro);
        Task<SalidaProduccionAlmacenDTO> ObtenerXId(int parametro);


    }
}
