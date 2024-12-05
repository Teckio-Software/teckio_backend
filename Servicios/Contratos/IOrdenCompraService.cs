using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IOrdenCompraService<T> where T : DbContext
    {
        Task<List<OrdenCompraDTO>> ObtenXIdProyecto(int IdProyecto);
        Task<OrdenCompraDTO> ObtenXId(int Id);
        Task<List<OrdenCompraDTO>> ObtenXIdCotizacion(int IdCotizacion);
        Task<List<OrdenCompraDTO>> ObtenXIdContratista(int IdContratista);
        Task<List<OrdenCompraDTO>> ObtenXIdRequisicion(int IdRequisicion);
        Task<RespuestaDTO> Crear(OrdenCompraCreacionDTO modelo);
        Task<OrdenCompraDTO> CrearYObtener(OrdenCompraDTO modelo);
        Task<RespuestaDTO> Editar(OrdenCompraDTO modelo);
        Task<RespuestaDTO> ActualizarEstatusCancelar(int Id);
        Task<RespuestaDTO> ActualizarEstatusFacturado(int Id);
        Task<RespuestaDTO> ActualizarEstatusEntradaAlmacen(int Id);
        Task<RespuestaDTO> ActualizaOrdenCompraSurtidos(int Id, int estatus);

    }
    public interface IInsumoXOrdenCompraService<T> where T : DbContext
    {
        /// <summary>
        /// Para uso interno
        /// </summary>
        /// <returns></returns>
        Task<List<InsumoXOrdenCompraDTO>> ObtenTodos();
        /// <summary>
        /// Para controller
        /// </summary>
        /// <param name="IdOrdenCompra"></param>
        /// <returns></returns>
        Task<List<InsumoXOrdenCompraDTO>> ObtenXIdOrdenCompra(int? IdOrdenCompra);
        Task<InsumoXOrdenCompraDTO> ObtenXId(int Id);
        Task<List<InsumoXOrdenCompraDTO>> ObtenInsumosOrdenCompraXIdInsumoCotizacion(int IdCotizacion);
        Task<RespuestaDTO> Crear(InsumoXOrdenCompraCreacionDTO modelo);
        Task<InsumoXOrdenCompraDTO> CrearYObtener(InsumoXOrdenCompraDTO modelo);

        Task<bool> CrearLista(List<InsumoXOrdenCompraDTO> parametro);
        //Task<RespuestaDTO> Editar(InsumoXOrdenCompraDTO modelo);
        Task<RespuestaDTO> ActualizarEstatusCancelar(int Id);
        Task<RespuestaDTO> ActualizarEstatusFacturado(int Id);

        Task<RespuestaDTO> ActualizarCantidadRecibida(int IdInsumo, int idOrdenCompra, decimal CantidadRecibida);
    }
}
