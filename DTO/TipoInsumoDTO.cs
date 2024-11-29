namespace ERP_TECKIO
{
    /// <summary>
    /// Para el tipo de insumo
    /// </summary>
    public class TipoInsumoDTO
    {
        /// <summary>
        /// Identificador del tipo de insumo
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Descripción del tipo de insumo
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;
    }

    /// <summary>
    /// Para crear un tipo de insumo
    /// </summary>
    public class TipoInsumoCreacionDTO
    {
        /// <summary>
        /// Descripción del tipo de insumo
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;
    }
}
