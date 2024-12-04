using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public interface IClientesService<T> where T : DbContext
    {
        Task<List<ClienteDTO>> ObtenTodos();
        Task<ClienteDTO> ObtenXId(int Id);
        Task<bool> Crear(ClienteDTO parametro);
        Task<ClienteDTO> CrearYObtener(ClienteDTO parametro);
        Task<bool> Editar(ClienteDTO modelo);
        Task<bool> Eliminar(int Id);
    }
}
