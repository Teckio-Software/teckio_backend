using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface ICostoHorarioFijoXPrecioUnitarioDetalleService<T> where T : DbContext
    {
        Task<CostoHorarioFijoXPrecioUnitarioDetalleDTO> ObtenTodosXIdPrecioUnitarioDetalle(int IdPrecioUnitarioDetalle);
        Task<CostoHorarioFijoXPrecioUnitarioDetalleDTO> ObtenXId(int Id);
        Task<CostoHorarioFijoXPrecioUnitarioDetalleDTO> CrearYObtener(CostoHorarioFijoXPrecioUnitarioDetalleDTO parametro);
        Task<RespuestaDTO> Editar(CostoHorarioFijoXPrecioUnitarioDetalleDTO modelo);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
