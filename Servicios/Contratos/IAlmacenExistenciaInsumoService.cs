using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IAlmacenExistenciaInsumoService<T> where T : DbContext
    {
        Task<RespuestaDTO> CreaExistenciaInsumoEntrada(AlmacenExistenciaInsumoCreacionDTO parametro);
        Task<bool> Crear(AlmacenExistenciaInsumoCreacionDTO parametro);
        Task<AlmacenExistenciaInsumoDTO> CreaExistenciaInsumoEntradaYRegresa(AlmacenExistenciaInsumoCreacionDTO parametro);
        Task<RespuestaDTO> ActualizaExistenciaInsumoEntrada(int Id, decimal Cantidad);
        Task<RespuestaDTO> ActualizaExistenciaInsumoSalida(int Id, decimal Cantidad);
        Task<List<AlmacenExistenciaInsumoDTO>> ObtenTodos();
        Task<AlmacenExistenciaInsumoDTO> ObtenXId(int Id);
        Task<List<AlmacenExistenciaInsumoDTO>> ObtenXIdAlmacen(int IdAlmacen);
        Task<List<AlmacenExistenciaInsumoDTO>> ObtenXIdProyecto(int IdProyecto);
        Task<AlmacenExistenciaInsumoDTO> ObtenInsumoXIdAlmacenEIdInsumo(int IdInsumo, int IdAlmacen);
        Task<AlmacenExistenciaInsumoDTO> ObtenInsumoXIdProyectoEIdInsumo(int IdInsumo, int IdProyecto);
        Task<bool> CrearLista(List<AlmacenExistenciaInsumoCreacionDTO> parametro);

    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    public interface ITeckioAlmacenExistenciaInsumoService
    {
        Task<RespuestaDTO> CreaExistenciaInsumoEntrada(AlmacenExistenciaInsumoCreacionDTO parametro);
        Task<AlmacenExistenciaInsumoDTO> CreaExistenciaInsumoEntradaYRegresa(AlmacenExistenciaInsumoCreacionDTO parametro);
        Task<RespuestaDTO> ActualizaExistenciaInsumoEntrada(int Id, decimal Cantidad);
        Task<RespuestaDTO> ActualizaExistenciaInsumoSalida(int Id, decimal Cantidad);
        Task<List<AlmacenExistenciaInsumoDTO>> ObtenTodos();
        Task<AlmacenExistenciaInsumoDTO> ObtenXId(int Id);
        Task<List<AlmacenExistenciaInsumoDTO>> ObtenXIdAlmacen(int IdAlmacen);
        Task<List<AlmacenExistenciaInsumoDTO>> ObtenXIdProyecto(int IdProyecto);
        Task<AlmacenExistenciaInsumoDTO> ObtenInsumoXIdAlmacenEIdInsumo(int IdInsumo, int IdAlmacen);
        Task<AlmacenExistenciaInsumoDTO> ObtenInsumoXIdProyectoEIdInsumo(int IdInsumo, int IdProyecto);
    }
}
