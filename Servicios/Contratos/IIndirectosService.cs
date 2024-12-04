using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IIndirectosService<T> where T : DbContext
    {
        Task<IndirectosDTO> CrearYObtener(IndirectosDTO objeto);
        Task<IndirectosDTO> ObtenerXId(int idIndirecto);
        Task<RespuestaDTO> Crear(IndirectosDTO objeto);
        Task<RespuestaDTO> Editar(IndirectosDTO objeto);
        Task<RespuestaDTO> Eliminar(IndirectosDTO objeto);
        Task<List<IndirectosDTO>> ObtenerXIdConjunto(int IdConjunto);
    }


    public interface IIndirectosXConceptoService<T> where T : DbContext
    {
        Task<IndirectosXConceptoDTO> CrearYObtener(IndirectosXConceptoDTO objeto);
        Task<IndirectosXConceptoDTO> ObtenerXId(int idIndirecto);
        Task<RespuestaDTO> Crear(IndirectosXConceptoDTO objeto);
        Task<RespuestaDTO> Editar(IndirectosXConceptoDTO objeto);
        Task<RespuestaDTO> Eliminar(IndirectosXConceptoDTO objeto);
        Task<List<IndirectosXConceptoDTO>> ObtenerXIdConcepto(int IdConcepto);
    }
}
