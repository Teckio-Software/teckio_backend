using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;



namespace ERP_TECKIO
{
    public interface IFactorSalarioRealService<T> where T : DbContext
    {
        Task<List<FactorSalarioRealDTO>> ObtenerTodosXProyecto(int IdProyecto);
        Task<FactorSalarioRealDTO> ObtenXId(int Id);
        Task<FactorSalarioRealDTO> CrearYObtener(FactorSalarioRealDTO registro);
        Task<RespuestaDTO> Editar(FactorSalarioRealDTO registro);
        Task Eliminar(int Id);
    }
}
