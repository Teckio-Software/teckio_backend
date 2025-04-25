using ERP_TECKIO.DTO.Factura;
using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface IArchivoService<TContext> where TContext : DbContext
    {
        Task<List<ArchivoDTO>> ObtenTodos();
        Task<List<ArchivoDTO>> ObtenXNombre(string Nombre);
        Task<ArchivoDTO> ObtenXId(int Id);
        Task<List<ArchivoDTO>> ObtenXContenido(string contenido);
        Task<bool> Crear(ArchivoDTO parametro);
        Task<ArchivoDTO> CrearYObtener(ArchivoDTO parametro);
        Task<bool> Editar(ArchivoDTO parametro);
        Task<bool> Eliminar(int Id);
    }
}
