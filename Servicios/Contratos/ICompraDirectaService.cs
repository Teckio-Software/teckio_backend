

using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


namespace ERP_TECKIO
{
    public interface ICompraDirectaService<T> where T : DbContext
    {
        Task<List<CompraDirectaDTO>> ObtenXIdProyecto(int IdProyecto);
        Task<List<CompraDirectaDTO>> ObtenXIdRequisicion(int IdRequisicion);
        Task<CompraDirectaDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(CompraDirectaCreacionDTO parametro);
        Task<CompraDirectaDTO> CrearYObtener(CompraDirectaCreacionDTO parametro);
        Task<RespuestaDTO> Editar(CompraDirectaDTO parametro);
        Task<RespuestaDTO> ActualizarEstatusCancelar(int Id);
        Task<RespuestaDTO> ActualizarEstatusFacturado(int Id);
        Task<RespuestaDTO> ActualizarEstatusEntradaAlmacen(int Id);
    }
    public interface IInsumoXCompraDirectaService<T> where T : DbContext
    {
        /// <summary>
        /// Para uso interno, sin filtro
        /// </summary>
        /// <returns></returns>
        Task<List<InsumoXCompraDirectaDTO>> ObtenTodos();
        /// <summary>
        /// Para controller
        /// </summary>
        /// <param name="IdCompraDirecta"></param>
        /// <returns></returns>
        Task<List<InsumoXCompraDirectaDTO>> ObtenXIdCompraDirecta(int IdCompraDirecta);
        Task<InsumoXCompraDirectaDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(InsumoXCompraDirectaCreacionDTO parametro);
        Task<InsumoXCompraDirectaDTO> CrearYObtener(InsumoXCompraDirectaCreacionDTO parametro);
        Task<RespuestaDTO> Editar(InsumoXCompraDirectaDTO modelo);
        Task<RespuestaDTO> ActualizarEstatusCancelar(int Id);
        Task<RespuestaDTO> ActualizarEstatusFacturado(int Id);
    }
}
