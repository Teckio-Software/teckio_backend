
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;

namespace ERP_TECKIO
{
    public interface IImpuestoService<T> where T : DbContext
    {
        Task<List<ImpuestoDTO>> ObtenerTodos();
    }
}
