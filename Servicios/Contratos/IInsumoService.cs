
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO
{
    /// <summary>
    /// Interfaz que contiene todos los campos para el uso de la tabla <c>Insumo</c>
    /// </summary>
    public interface IInsumoService<T> where T : DbContext
    {
        Task<List<InsumoDTO>> ObtenXIdProyecto(int IdProyecto);
        Task<List<InsumoDTO>> ObtenerInsumoXTipoYProyecto(int IdProyecto, int IdTipoInsumo);

        Task<List<InsumoDTO>> ObtenerInsumoXProyecto(int IdProyecto);
        Task<InsumoDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(InsumoCreacionDTO modelo);
        Task<InsumoDTO> CrearYObtener(InsumoCreacionDTO modelo);
        Task<RespuestaDTO> Editar(InsumoDTO modelo);
        Task<RespuestaDTO> Eliminar(int Id);
        Task<List<InsumoDTO>> ObtenerTodos();
        Task<bool> EditarMultiple(List<InsumoDTO> registros);
        Task<bool> RemoverAutorizacionMultiple(List<InsumoDTO> registros);
        Task<bool> AutorizarMultiple(List<InsumoParaExplosionDTO> registros);
        Task<bool> AutorizarMultipleXPU(List<PrecioUnitarioDetalleDTO> registros);

    }
}
