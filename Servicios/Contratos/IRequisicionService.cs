using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


namespace ERP_TECKIO
{
    /// <summary>
    /// Interfaz que contiene todos los campos para usar la tabla de <c>Requisición</c>
    /// </summary>
    public interface IRequisicionService<T> where T : DbContext
    {
        Task<List<RequisicionDTO>> ObtenTodos();
        Task<List<RequisicionDTO>> ObtenXIdProyecto(int idProyecto);
        Task<RequisicionDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(RequisicionDTO modelo);
        Task<RequisicionDTO> CrearYObtener(RequisicionDTO modelo);
        Task<RespuestaDTO> Editar(RequisicionDTO modelo);
        Task<RespuestaDTO> ActualizarRequisicionInsumosSurtidos(int Id, int estatus);
        Task<RespuestaDTO> ActualizarRequisicionInsumosComprados(int Id, int estatus);
        Task<RespuestaDTO> ActualizarEstatusAutorizar(int Id);
        Task<RespuestaDTO> ActualizarEstatusRemoverAutorizar(int Id);
        Task<RespuestaDTO> ActualizarEstatusCotizar(int Id);
        Task<RespuestaDTO> ActualizarEstatusCancelar(int Id);
        Task<RespuestaDTO> ActualizarEstatusComprar(int Id);
        Task<RespuestaDTO> ActualizarEstatusEntradaAlmacen(int Id);
        Task<int> CompruebaEstatusRequisicion(int Id);
        Task<int> CompruebaEstatusRequisicionInsumosComprados(int Id);
        Task<int> CompruebaEstatusRequisicionInsumosSurtidos(int Id);
    }
    /// <summary>
    /// Interfaz que contiene todos los campos para usar la tabla de <c>InsumoXRequisición</c>
    /// </summary>
    public interface IInsumoXRequisicionService<T> where T : DbContext
    {
        Task<RespuestaDTO> Crear(InsumoXRequisicionDTO parametro);

        Task<bool> CrearLista(List<InsumoXRequisicionDTO> parametro);
        Task<InsumoXRequisicionDTO> CrearYObtener(InsumoXRequisicionCreacionDTO parametro);
        /// <summary>
        /// Para traer los insumos de las requisiciones de manera general
        /// </summary>
        /// <returns></returns>
        Task<List<InsumoXRequisicionDTO>> ObtenTodos();
        Task<List<InsumoXRequisicionDTO>> ObtenXIdRequisicion(int IdRequisicion);
        Task<InsumoXRequisicionDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Editar(InsumoXRequisicionDTO modelo);
        Task<RespuestaDTO> Eliminar(int idInsumoRequisicion);
        Task<RespuestaDTO> EditarCantidadComprada(int Id, decimal Cantidad);
        Task<RespuestaDTO> EditarCantidadSurtida(int Id, decimal Cantidad);
        Task<RespuestaDTO> ActualizarEstatusAutorizar(int Id);
        Task<RespuestaDTO> ActualizarEstatusRemoverAutorizar(int Id);
        Task<RespuestaDTO> ActualizarEstatusCotizar(int Id);
        Task<RespuestaDTO> ActualizarEstatusCancelar(int Id);
        Task<RespuestaDTO> ActualizarEstatusComprar(int Id);
        Task<RespuestaDTO> ActualizarEstatusEntradaAlmacen(int Id);
        Task<RespuestaDTO> ActualizarEstatusInsumosSurtidos(int Id, int estatus);
        Task<RespuestaDTO> ActualizarEstatusInsumosComprados(int Id, int estatus);
        Task<int> CompruebaEstatusRequisicionInsumosComprados(int IdRequisicion);
        Task<int> CompruebaEstatusRequisicionInsumosSurtidos(int IdRequisicion);
    }
}
