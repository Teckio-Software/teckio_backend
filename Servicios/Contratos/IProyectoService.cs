using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    /// <summary>
    /// Interfaz que contiene todos los campos para usar la tabla de Proyecto
    /// </summary>
    public interface IProyectoService<T> where T : DbContext
    {
        Task<List<ProyectoDTO>> Lista();
        Task<List<ProyectoDTO>> Estructurar(List<ProyectoDTO> registros);
        Task<ProyectoDTO> ObtenXId(int Id);
        Task<ProyectoDTO> ObtenXNombreProyecto(string nombreProyecto);
        Task<RespuestaDTO> Crear(ProyectoDTO modelo);
        Task<ProyectoDTO> CrearYObtener(ProyectoDTO modelo);
        Task<RespuestaDTO> Editar(ProyectoDTO modelo);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
