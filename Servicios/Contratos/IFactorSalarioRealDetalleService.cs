using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IFactorSalarioRealDetalleService<T> where T : DbContext
    {
        Task<List<FactorSalarioRealDetalleDTO>> ObtenerTodosXFSR(int IdFactorSalarioReal);
        Task<FactorSalarioRealDetalleDTO> ObtenXId(int Id);
        Task<FactorSalarioRealDetalleDTO> CrearYObtener(FactorSalarioRealDetalleDTO registro);
        Task<RespuestaDTO> Editar(FactorSalarioRealDetalleDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
