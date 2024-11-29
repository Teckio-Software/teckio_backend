using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;



namespace ERP_TECKIO
{
    public interface IRelacionFSRInsumoService<T> where T : DbContext
    {
        Task<List<RelacionFSRInsumoDTO>> ObtenerTodosXProyecto(int IdProyecto);
        Task<RelacionFSRInsumoDTO> ObtenXId(int Id);
        Task<RelacionFSRInsumoDTO> CrearYObtener(RelacionFSRInsumoDTO registro);
        Task<RespuestaDTO> Editar(RelacionFSRInsumoDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
