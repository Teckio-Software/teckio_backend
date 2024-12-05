namespace ERP_TECKIO
{
    /// <summary>
    /// Clase de la familia de insumo
    /// </summary>
    public class FamiliaInsumoDTO
    {
        /// <summary>
        /// Identificador de la familia de insumo
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Descripción de la familia de insumo
        /// </summary>
        public string Descripcion { get; set; }
    }

    /// <summary>
    /// Clase para crear una Familia de Insumos
    /// </summary>
    public class FamiliaInsumoCreacionDTO
    {
        /// <summary>
        /// Descripción de la familia de insumo
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;
    }
}
