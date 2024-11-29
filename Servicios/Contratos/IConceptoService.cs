using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


namespace ERP_TECKIO
{
    public interface IConceptoService<T> where T : DbContext
    {
        Task<List<ConceptoDTO>> ObtenerTodos(int IdProyecto);
        Task<ConceptoDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(ConceptoDTO especialidad);
        Task<ConceptoDTO> CrearYObtener(ConceptoDTO especialidad);
        Task<RespuestaDTO> Editar(ConceptoDTO parametro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
