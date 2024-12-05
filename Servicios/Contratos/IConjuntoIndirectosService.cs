using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IConjuntoIndirectosService<T> where T : DbContext
    {
        Task<ConjuntoIndirectosDTO> CrearYObtener(ConjuntoIndirectosDTO objeto);
        Task<ConjuntoIndirectosDTO> ObtenerXIdProyecto(int IdProyecto);
        Task<ConjuntoIndirectosDTO> ObtenerXId(int Id);
        Task<RespuestaDTO> Editar(ConjuntoIndirectosDTO conjuntoIndirectos);
    }
}
