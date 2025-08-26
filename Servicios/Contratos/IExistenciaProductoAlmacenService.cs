using ERP_TECKIO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos
{
    public interface IExistenciaProductoAlmacenService<T> where T : DbContext
    {
        Task<RespuestaDTO> Crear(ExistenciaProductoAlmacenDTO modelo);
        Task<ExistenciaProductoAlmacenDTO> CrearYObtener(ExistenciaProductoAlmacenDTO modelo);
        Task<RespuestaDTO> Editar(ExistenciaProductoAlmacenDTO modelo);
        Task<RespuestaDTO> Eliminar(ExistenciaProductoAlmacenDTO modelo);

        Task<ExistenciaProductoAlmacenDTO> ObtenerExistencia(int idAlmacen, int idProdYSer);
    }
}
