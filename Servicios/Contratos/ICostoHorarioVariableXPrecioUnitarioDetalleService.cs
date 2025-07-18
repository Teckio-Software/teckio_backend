using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface ICostoHorarioVariableXPrecioUnitarioDetalleService<T> where T : DbContext
    {
        Task<List<CostoHorarioVariableXPrecioUnitarioDetalleDTO>> ObtenTodosXIdPrecioUnitarioDetalle(int IdPrecioUnitarioDetalle);
        Task<CostoHorarioVariableXPrecioUnitarioDetalleDTO> ObtenXId(int Id);
        Task<CostoHorarioVariableXPrecioUnitarioDetalleDTO> CrearYObtener(CostoHorarioVariableXPrecioUnitarioDetalleDTO parametro);
        Task<RespuestaDTO> Editar(CostoHorarioVariableXPrecioUnitarioDetalleDTO modelo);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}

