using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IPrecioUnitarioService<T> where T : DbContext
    {
        Task<List<PrecioUnitarioDTO>> ObtenerTodos(int IdProyecto);
        Task<List<PrecioUnitarioCopiaDTO>> ObtenerTodosParaCopia(int IdProyecto);
        Task<List<PrecioUnitarioCopiaDTO>> ObtenerCatalogoGeneral();
        Task<List<PrecioUnitarioDTO>> Estructurar(List<PrecioUnitarioDTO> registros, decimal IndirectoG);
        Task<List<PrecioUnitarioCopiaDTO>> EstructurarCopia(List<PrecioUnitarioCopiaDTO> registros);
        Task<PrecioUnitarioDTO> ObtenXId(int Id);
        Task<PrecioUnitarioDTO> ObtenXIdConcepto(int IdConcepto);
        Task<RespuestaDTO> Crear(PrecioUnitarioDTO padre);
        Task<RespuestaDTO> CrearCopia(PrecioUnitarioCopiaDTO padre);
        Task<PrecioUnitarioDTO> CrearYObtener(PrecioUnitarioDTO hijo);
        Task<PrecioUnitarioCopiaDTO> CrearYObtenerCopia(PrecioUnitarioCopiaDTO hijo);
        Task<RespuestaDTO> Editar(PrecioUnitarioDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}