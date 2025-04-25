using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface ITipoImpuestoService<TContext> where TContext : DbContext
    {
        Task<List<TipoImpuestoDTO>> ObtenTodos();
        Task<TipoImpuestoDTO> ObtenXClave(string Clave);
        Task<List<TipoImpuestoDTO>> ObtenXDescripcion(string Descripcion);
        Task<TipoImpuestoDTO> ObtenXId(int Id);
        Task<bool> Crear(TipoImpuestoDTO parametro);
        Task<TipoImpuestoDTO> CrearYObtener(TipoImpuestoDTO parametro);
        Task<bool> Editar(TipoImpuestoDTO parametro);
        Task<bool> Eliminar(int Id);
    }
}
