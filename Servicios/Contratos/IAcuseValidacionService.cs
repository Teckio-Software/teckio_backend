using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


namespace ERP_TECKIO
{
    public interface IAcuseValidacionService<TContext> where TContext : DbContext
    {
        Task<List<AcuseValidacionDTO>> ObtenTodos();
        Task<List<AcuseValidacionDTO>> ObtenXIdFactura(int IdFactura);
        Task<List<AcuseValidacionDTO>> ObtenXIdUsuario(int IdUsuario);
        Task<List<AcuseValidacionDTO>> ObtenXFolio(string Folio);
        Task<AcuseValidacionDTO> ObtenXId(int Id);
        Task<bool> Crear(AcuseValidacionDTO parametro);
        Task<AcuseValidacionDTO> CrearYObtener(AcuseValidacionDTO parametro);
        Task<bool> Editar(AcuseValidacionDTO parametro);
        Task<bool> Eliminar(int Id);
    }
}
