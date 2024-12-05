using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IPolizaDetalleService<T> where T : DbContext
    {
        Task<List<PolizaDetalleDTO>> ObtenTodosXIdPoliza(int IdPoliza);
        Task<PolizaDetalleDTO> ObtenXId(int id);
        Task<RespuestaDTO> Crear(PolizaDetalleDTO poliza);
        Task<RespuestaDTO> Editar(PolizaDetalleDTO poliza);
    }
}