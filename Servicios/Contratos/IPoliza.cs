using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IPolizaService<T> where T : DbContext
    {
        Task<List<PolizaDTO>> ObtenTodosXEmpresa();
        Task<PolizaDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(PolizaDTO poliza);
        Task<PolizaDTO> CrearYObtener(PolizaDTO poliza);
        Task<RespuestaDTO> Contabilizar(PolizaDTO poliza);
        Task<RespuestaDTO> Auditar(PolizaDTO poliza);
        Task<RespuestaDTO> Cancelar(PolizaDTO poliza);
        Task<RespuestaDTO> Editar(PolizaDTO poliza);
    }
}
