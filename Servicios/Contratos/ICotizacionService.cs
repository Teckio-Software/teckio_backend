using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface ICotizacionService<T> where T : DbContext
    {
        Task<List<CotizacionDTO>> ObtenXIdProyecto(int IdProyecto);
        Task<List<CotizacionDTO>> ObtenXIdContratista(int IdContratista);
        Task<List<CotizacionDTO>> ObtenXIdRequision(int IdRequisicion);
        Task<CotizacionDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(CotizacionCreacionDTO modelo);
        Task<CotizacionDTO> CrearYObtener(CotizacionDTO modelo);
        Task<RespuestaDTO> Editar(CotizacionDTO modelo);
        Task<RespuestaDTO> ActualizarEstatusAutorizar(int Id);
        Task<RespuestaDTO> ActualizarEstatusRemoverAutorizar(int Id);
        Task<RespuestaDTO> ActualizarEstatusCancelar(int Id);
        Task<RespuestaDTO> ActualizarEstatusComprar(int Id);
        Task<RespuestaDTO> ActualizarEstatusCotizacion(int Id, int estatus);
    }

    public interface IInsumoXCotizacionService<T> where T : DbContext
    {
        Task<List<InsumoXCotizacionDTO>> ObtenXIdCotizacion(int IdCotizacion);
        Task<List<InsumoXCotizacionDTO>> ObtenTodos();
        Task<InsumoXCotizacionDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(InsumoXCotizacionCreacionDTO parametro);
        Task<bool> CrearLista(List<InsumoXCotizacionDTO> parametro);
        Task<InsumoXCotizacionDTO> CrearYObtener(InsumoXCotizacionDTO parametro);
        Task<RespuestaDTO> Editar(InsumoXCotizacionDTO modelo);
        Task<RespuestaDTO> ActualizarEstatusAutorizar(int Id);
        Task<RespuestaDTO> ActualizarEstatusRemoverAutorizar(int Id);
        Task<RespuestaDTO> ActualizarEstatusCancelar(int Id);
        Task<RespuestaDTO> VerificaEstatusAutorizado(int Id);
        Task<RespuestaDTO> ActualizaEstatusComprado(int Id);
        Task<int> CompruebaEstatusCotizacionInsumosComprados(int IdCotizacion);
    }
}
