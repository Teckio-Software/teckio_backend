using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IAlmacenSalidaService<T> where T : DbContext
    {
        Task<List<AlmacenSalidaDTO>> ObtenTodos();
        Task<List<AlmacenSalidaDTO>> ObtenXIdAlmacen(int IdAlmacen);
        Task<List<AlmacenSalidaDTO>> ObtenXIdProyecto(int IdProyecto);
        Task<AlmacenSalidaDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(AlmacenSalidaCreacionDTO parametro);
        Task<AlmacenSalidaDTO> CrearYObtener(AlmacenSalidaDTO parametro);
        Task<AlmacenSalidaDTO> CrearYObtener(AlmacenSalidaCreacionDTO parametro);
        Task<RespuestaDTO> Editar(AlmacenSalidaDTO parametro);
        Task<RespuestaDTO> Autorizar(int Id);
        Task<RespuestaDTO> Cancelar(int Id);
    }

    public interface IInsumoXAlmacenSalidaService<T> where T : DbContext
    {
        Task<List<AlmacenSalidaInsumoDTO>> ObtenTodos();
        Task<List<AlmacenSalidaInsumoDTO>> ObtenXIdAlmacenSalida(int IdAlmacenSalida);
        Task<AlmacenSalidaInsumoDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(AlmacenSalidaInsumoCreacionDTO parametro);
        Task<AlmacenSalidaInsumoDTO> CrearYObtener(AlmacenSalidaInsumoDTO parametro);
        Task<RespuestaDTO> Editar(AlmacenSalidaInsumoDTO parametro);
        Task<RespuestaDTO> Autorizar(int Id);
        Task<RespuestaDTO> Cancelar(int Id);
        Task<bool> CrearLista(List<AlmacenSalidaInsumoDTO> parametro);
        Task<bool> EditarLista(List<AlmacenSalidaInsumoDTO> parametro);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    public interface ITeckioAlmacenSalidaService
    {
        Task<List<AlmacenSalidaDTO>> ObtenTodos();
        Task<List<AlmacenSalidaDTO>> ObtenXIdAlmacen(int IdAlmacen);
        Task<List<AlmacenSalidaDTO>> ObtenXIdProyecto(int IdProyecto);
        Task<AlmacenSalidaDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(AlmacenSalidaCreacionDTO parametro);
        Task<AlmacenSalidaDTO> CrearYObtener(AlmacenSalidaCreacionDTO parametro);
        Task<RespuestaDTO> Editar(AlmacenSalidaDTO parametro);
        Task<RespuestaDTO> Autorizar(int Id);
        Task<RespuestaDTO> Cancelar(int Id);
    }

    public interface ITeckioInsumoXAlmacenSalidaService
    {
        Task<List<AlmacenSalidaInsumoDTO>> ObtenTodos();
        Task<List<AlmacenSalidaInsumoDTO>> ObtenXIdAlmacenSalida(int IdAlmacenSalida);
        Task<AlmacenSalidaInsumoDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(AlmacenSalidaInsumoCreacionDTO parametro);
        Task<AlmacenSalidaInsumoDTO> CrearYObtener(AlmacenSalidaInsumoCreacionDTO parametro);
        Task<RespuestaDTO> Editar(AlmacenSalidaInsumoDTO parametro);
        Task<RespuestaDTO> Autorizar(int Id);
        Task<RespuestaDTO> Cancelar(int Id);
    }
}
