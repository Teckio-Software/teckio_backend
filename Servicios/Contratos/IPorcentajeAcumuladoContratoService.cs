using Microsoft.EntityFrameworkCore;



namespace ERP_TECKIO
{
    public interface IPorcentajeAcumuladoContratoService<T> where T : DbContext
    {
        Task<List<PorcentajeAcumuladoContratoDTO>> ObtenerRegistros();
        Task<PorcentajeAcumuladoContratoDTO> ObtenXId(int Id);
        Task<PorcentajeAcumuladoContratoDTO> CrearYObtener(PorcentajeAcumuladoContratoDTO modelo);
        Task<RespuestaDTO> Editar(PorcentajeAcumuladoContratoDTO modelo);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
