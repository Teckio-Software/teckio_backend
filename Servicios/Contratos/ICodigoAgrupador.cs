using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    /// <summary>
    /// Interfaz del código agrupador del SAT
    /// </summary>
    public interface ICodigoAgrupadorService
    {
        Task<List<CodigoAgrupadorSatDTO>> ObtenTodos();
        Task<CodigoAgrupadorSatDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(CodigoAgrupadorSatCreacionDTO modelo);
        Task<RespuestaDTO> Editar(CodigoAgrupadorSatDTO modelo);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
