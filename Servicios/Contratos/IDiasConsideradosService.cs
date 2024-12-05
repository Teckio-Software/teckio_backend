using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IDiasConsideradosService<T> where T : DbContext
    {
        Task<List<DiasConsideradosDTO>> ObtenerTodosXFSI(int IdFactorSalarioIntegrado);
        Task<DiasConsideradosDTO> ObtenXId(int Id);
        Task<DiasConsideradosDTO> CrearYObtener(DiasConsideradosDTO registro);
        Task<RespuestaDTO> Editar(DiasConsideradosDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
