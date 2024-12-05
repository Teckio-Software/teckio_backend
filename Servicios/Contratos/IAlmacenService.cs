using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    /// <summary>
    /// Interfaz que contiene todos los campos para usar la tabla de almacenes
    /// </summary>
    public interface IAlmacenService<T> where T : DbContext
    {
        Task<List<AlmacenDTO>> ObtenerXIdProyecto(int IdProyecto);
        Task<List<AlmacenDTO>> ObtenerXCodigo(string parametro);
        Task<List<AlmacenDTO>> ObtenTodos();
        Task<AlmacenDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(AlmacenCreacionDTO modelo);
        Task<RespuestaDTO> Crear(AlmacenDTO modelo);
        Task<RespuestaDTO> Editar(AlmacenDTO modelo);
        Task<RespuestaDTO> Eliminar(int Id);
    }

    
}
