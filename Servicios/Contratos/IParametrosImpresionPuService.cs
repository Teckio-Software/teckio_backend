using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IParametrosImpresionPuService<T> where T : DbContext
    {
        Task<RespuestaDTO> Crear(ParametrosImpresionPuDTO modelo);
        Task<ParametrosImpresionPuDTO> CrearYObtener(ParametrosImpresionPuDTO modelo);
        Task<RespuestaDTO> Editar(ParametrosImpresionPuDTO modelo);
        Task<RespuestaDTO> Eliminar(int id);
        Task<ParametrosImpresionPuDTO> Obtener(int id);
        Task<List<ParametrosImpresionPuDTO>> ObtenerTodos();
        Task<List<ParametrosImpresionPuDTO>> ObtenerPorCliente(int idCliente);
    }
}
