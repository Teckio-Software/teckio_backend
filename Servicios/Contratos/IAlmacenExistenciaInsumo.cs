

namespace ERP_TECKIO
{
    public interface IAlmacenExistenciaInsumo
    {
        Task<List<AlmacenExistenciaInsumoDTO>> ObtenTodos();
        Task<List<AlmacenExistenciaInsumoDTO>> ObtenXIdAlmacen(int IdAlmacen);
        Task<AlmacenExistenciaInsumoDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(AlmacenEntradaCreacionDTO parametro);
        Task<AlmacenExistenciaInsumoDTO> CrearYObtener(AlmacenEntradaCreacionDTO parametro);
        Task<RespuestaDTO> Editar(AlmacenEntradaDTO modelo);
        Task<RespuestaDTO> Cancelar(int Id);
    }

    ////////////////////////////////////////////////////////////////////////////////////////
    public interface ITeckioAlmacenExistenciaInsumo
    {
        Task<List<AlmacenExistenciaInsumoDTO>> ObtenTodos();
        Task<List<AlmacenExistenciaInsumoDTO>> ObtenXIdAlmacen(int IdAlmacen);
        Task<AlmacenExistenciaInsumoDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(AlmacenEntradaCreacionDTO parametro);
        Task<AlmacenExistenciaInsumoDTO> CrearYObtener(AlmacenEntradaCreacionDTO parametro);
        Task<RespuestaDTO> Editar(AlmacenEntradaDTO modelo);
        Task<RespuestaDTO> Cancelar(int Id);
    }
}
