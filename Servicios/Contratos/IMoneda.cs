using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO
{
    public interface IMoneda<T> where T : DbContext
    {
        Task<List<MonedaDTO>> ObtenTodos();
    }
}
