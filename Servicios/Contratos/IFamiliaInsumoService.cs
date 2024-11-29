using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


namespace ERP_TECKIO
{
    /// <summary>
    /// Interfaz que contiene las familias de insumo
    /// </summary>
    public interface IFamiliaInsumoService<T> where T : DbContext
    {
        Task<List<FamiliaInsumoDTO>> Lista();
        Task<FamiliaInsumoDTO> ObtenXId(int Id);
        Task<RespuestaDTO> Crear(FamiliaInsumoCreacionDTO modelo);
        Task<FamiliaInsumoDTO> CrearYObtener(FamiliaInsumoCreacionDTO modelo);
        Task<RespuestaDTO> Editar(FamiliaInsumoDTO modelo);
        Task<RespuestaDTO> Eliminar(int Id);
    }
}
