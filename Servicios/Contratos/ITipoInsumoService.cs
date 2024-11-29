

using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;

/// <summary>
/// Interfaz que contiene los tipos de insumo
/// </summary>
namespace ERP_TECKIO
{
    /// <summary>
    /// Interfaz que contiene los tipos de insumo
    /// </summary>
    public interface ITipoInsumoService<T> where T: DbContext
    {
        Task<List<TipoInsumoDTO>> Lista();
        Task<TipoInsumoDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(TipoInsumoCreacionDTO modelo);
        Task<TipoInsumoDTO> CrearYObtener(TipoInsumoCreacionDTO modelo);
        Task<RespuestaDTO> Editar(TipoInsumoDTO modelo);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
