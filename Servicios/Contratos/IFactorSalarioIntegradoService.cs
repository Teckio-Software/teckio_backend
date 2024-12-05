using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IFactorSalarioIntegradoService<T> where T : DbContext
    {
        Task<List<FactorSalarioIntegradoDTO>> ObtenerTodosXProyecto(int IdProyecto);
        Task<FactorSalarioIntegradoDTO> ObtenXId(int Id);
        Task<FactorSalarioIntegradoDTO> CrearYObtener(FactorSalarioIntegradoDTO registro);
        Task<RespuestaDTO> Editar(FactorSalarioIntegradoDTO registro);
        Task Eliminar(int Id);
    }
}
