using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IEntradaProduccionAlmacenService<T> where T : DbContext
    {
        Task<RespuestaDTO> Crear(EntradaProduccionAlmacenDTO modelo);
        Task<EntradaProduccionAlmacenDTO> CrearYObtener(EntradaProduccionAlmacenDTO modelo);
        Task<RespuestaDTO> Editar(EntradaProduccionAlmacenDTO modelo);
        Task<RespuestaDTO> Eliminar(EntradaProduccionAlmacenDTO modelo);
    }
}
