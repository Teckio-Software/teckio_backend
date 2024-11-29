using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


namespace ERP_TECKIO
{
    public interface IContratoService<T> where T : DbContext
    {
        Task<List<ContratoDTO>> ObtenerRegistrosXIdProyecto(int IdProyecto);
        Task<ContratoDTO> ObtenXId(int Id);
        Task<ContratoDTO> CrearYObtener(ContratoDTO modelo);
        Task<RespuestaDTO> Editar(ContratoDTO modelo);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
