
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO
{
    public interface IImpuestoService<T> where T : DbContext
    {
        Task<List<ImpuestoDTO>> ObtenerTodos();
    }
}
