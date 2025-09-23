using ERP_TECKIO.DTO.Documentacion;
using ERP_TECKIO.Modelos.Documentacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Contratos.Documentacion
{
    public interface IGlosarioService<T> where T : DbContext
    {
        public Task<GlosarioDTO> CrearYObtener(GlosarioDTO glosarioDTO); 
        public Task<RespuestaDTO> Editar(GlosarioDTO glosarioDTO);
        public Task<RespuestaDTO> Eliminar(int IdGlosario);
        public Task<GlosarioDTO> ObtenerXId(int IdGlosario);
        public Task<List<GlosarioDTO>> ObtenerTodos();
    }
}
