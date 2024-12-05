using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IImpuestoInsumoCotizadoService<T> where T : DbContext
    {
        Task<RespuestaDTO> Crear(ImpuestoInsumoCotizadoCreacionDTO parametro);
        Task<RespuestaDTO> Editar(ImpuestoInsumoCotizadoDTO parametro);
        Task<RespuestaDTO> Eliminar(ImpuestoInsumoCotizadoDTO parametro);
        Task<List<ImpuestoInsumoCotizadoDTO>> ObtenerXIdInsumoCotizado(int idInsumoCotizado);
    }
}
