using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO.Servicios.Contratos.Facturacion
{
    public interface ITipoFactorService<TContext> where TContext : DbContext
    {
        Task<List<TipoFactorDTO>> ObtenerTodos();
        Task<TipoFactorDTO> ObtenerXId(int Id);
        Task<TipoFactorDTO> ObtenerXDescripcion(string descripcion);
        Task<TipoFactorDTO> CrearYObtener(TipoFactorDTO registro);
        Task<RespuestaDTO> Editar(TipoFactorDTO registro);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
