using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IPrecioUnitarioDetalleService<T> where T: DbContext
    {
        Task<List<PrecioUnitarioDetalleDTO>> ObtenerTodosXIdPrecioUnitario(int IdPrecioUnitario);
        Task<List<PrecioUnitarioDetalleDTO>> ObtenerTodosXIdPrecioUnitarioFiltrados(int IdPrecioUnitario, int IdPerteneciente);
        Task<PrecioUnitarioDetalleDTO> ObtenerXId(int Id);
        Task<List<PrecioUnitarioDetalleDTO>> ObtenerTodos();
        Task<List<PrecioUnitarioDetalleDTO>> ObtenerTodosXIdPrecioUnitarioDetalle(PrecioUnitarioDetalleDTO PrecioUnitarioDetalle);
        Task<PrecioUnitarioDetalleDTO> ObtenXId(int Id); 
        Task<PrecioUnitarioDetalleDTO> CrearYObtener(PrecioUnitarioDetalleDTO PrecioUnitarioDetalle);
        Task<RespuestaDTO> Editar(PrecioUnitarioDetalleDTO PrecioUnitarioDetalle);
        Task<RespuestaDTO> Eliminar(int Id);
        Task Recalcular(PrecioUnitarioDetalleDTO registro);
    }
}