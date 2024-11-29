using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


namespace ERP_TECKIO
{
    public interface IAlmacenEntradaService<T> where T : DbContext
    {
        Task<List<AlmacenEntradaDTO>> ObtenTodos();
        Task<List<AlmacenEntradaDTO>> ObtenXIdProyecto(int IdProyecto);
        Task<List<AlmacenEntradaDTO>> ObtenXIdRequisicion(int idRequsicion);
        Task<List<AlmacenEntradaDTO>> ObtenXIdAlmacen(int IdAlmacen);
        Task<AlmacenEntradaDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(AlmacenEntradaCreacionDTO parametro);
        Task<AlmacenEntradaDTO> CrearYObtener(AlmacenEntradaDTO parametro);
        Task<AlmacenEntradaDTO> CrearYObtener(AlmacenEntradaCreacionDTO parametro);
        Task<RespuestaDTO> Editar(AlmacenEntradaDTO modelo);
        Task<RespuestaDTO> Cancelar(int Id);
    }

    public interface IInsumoXAlmacenEntradaService<T> where T : DbContext
    {
        Task<List<AlmacenEntradaInsumoDTO>> ObtenTodos();
        Task<List<AlmacenEntradaInsumoDTO>> ObtenXIdAlmacenEntrada(int IdAlmacenEntrada);
        Task<AlmacenEntradaInsumoDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(AlmacenEntradaInsumoCreacionDTO parametro);
        Task<AlmacenEntradaInsumoDTO> CrearYObtener(AlmacenEntradaInsumoDTO parametro);
        Task<RespuestaDTO> Editar(AlmacenEntradaInsumoDTO parametro);
        Task<RespuestaDTO> Cancelar(int Id);
        Task<bool> CrearLista(List<AlmacenEntradaInsumoDTO> parametro);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    public interface ITeckioAlmacenEntradaService
    {
        Task<List<AlmacenEntradaDTO>> ObtenTodos();
        Task<List<AlmacenEntradaDTO>> ObtenXIdAlmacen(int IdAlmacen);
        Task<AlmacenEntradaDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(AlmacenEntradaCreacionDTO parametro);
        Task<AlmacenEntradaDTO> CrearYObtener(AlmacenEntradaCreacionDTO parametro);
        Task<RespuestaDTO> Editar(AlmacenEntradaDTO modelo);
        Task<RespuestaDTO> Cancelar(int Id);
    }

    public interface ITeckioInsumoXAlmacenEntradaService
    {
        Task<List<AlmacenEntradaInsumoDTO>> ObtenTodos();
        Task<List<AlmacenEntradaInsumoDTO>> ObtenXIdAlmacenEntrada(int IdAlmacenEntrada);
        Task<AlmacenEntradaInsumoDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(AlmacenEntradaInsumoCreacionDTO parametro);
        Task<AlmacenEntradaInsumoDTO> CrearYObtener(AlmacenEntradaInsumoCreacionDTO parametro);
        Task<RespuestaDTO> Editar(AlmacenEntradaInsumoDTO parametro);
        Task<RespuestaDTO> Cancelar(int Id);
    }
}
