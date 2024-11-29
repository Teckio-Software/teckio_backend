using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;

namespace ERP_TECKIO
{
    public interface IMoneda<T> where T : DbContext
    {
        Task<List<MonedaDTO>> ObtenTodos();
    }
}
