using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    /// <summary>
    /// Interfaz que contiene todos los campos para usar la tabla de Banco
    /// </summary>
    public interface IBancoService<T> where T : DbContext
    {
        Task<List<BancoDTO>> ObtenTodos();
        Task<BancoDTO> ObtenXId(int id);

        Task<BancoDTO> ObtenXClave(string clave);

        Task<bool> Crear(BancoDTO banco);

        Task<BancoDTO> CrearYObtener(BancoDTO banco);

        Task<bool> Editar(BancoDTO banco);

        Task<bool> Eliminar(int id);
    }
    
}
