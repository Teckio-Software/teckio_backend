


namespace ERP_TECKIO
{
    /// <summary>
    /// Interfaz que contiene todos los campos para usar la tabla de Explosión de Insumos
    /// </summary>
    public interface IExplosionInsumoService
    {
        Task<List<ExplosionInsumoDTO>> Lista();
        Task<ExplosionInsumoDTO> ObtenXId(int Id);
        Task<ExplosionInsumoDTO> Crear(ExplosionInsumoCreacionDTO modelo);
        Task<bool> Editar(ExplosionInsumoDTO modelo);
        Task<bool> Eliminar(int Id);
    }
}
