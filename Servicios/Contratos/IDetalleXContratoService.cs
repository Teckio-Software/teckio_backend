using Microsoft.EntityFrameworkCore;



namespace ERP_TECKIO
{
    public interface IDetalleXContratoService<T> where T : DbContext
    {
        Task<List<DetalleXContratoDTO>> ObtenerRegistrosXIdContrato(int IdContrato);
        Task<List<DetalleXContratoDTO>> ObtenerRegistrosXIdPrecioUnitario(int IdPrecioUnitario);
        Task<DetalleXContratoDTO> ObtenXId(int Id);
        Task<DetalleXContratoDTO> CrearYObtener(DetalleXContratoDTO modelo);
        Task<RespuestaDTO> Editar(DetalleXContratoDTO modelo);
        Task<bool> EditarMultiple(List<DetalleXContratoDTO> lista);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
