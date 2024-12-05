
namespace ERP_TECKIO
{
    /// <summary>
    /// Clase para mostrar la relación de los insumos por contratista que hereda de la interfaz IInsumoXContratista
    /// Se usa para visualizar los datos en una vista en el frontend
    /// </summary>
    public class InsumoXContratistaDTO
    {
        /// <summary>
        /// Identificador único del insumo por contratista
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador único del <see cref="InsumoDTO"/>
        /// </summary>
        public int IdInsumo { get; set; }
        /// <summary>
        /// Identificador único del <see cref="ContratistaDTO"/>
        /// </summary>
        public int IdContratista { get; set; }
        /// <summary>
        /// Código del insumo
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Descripción del insumo
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Unidad del insumo
        /// <example><code>m3</code></example>
        /// </summary>
        public string Unidad { get; set; }
        /// <summary>
        /// Costo que lleva el contratista para el insumo
        /// </summary>
        public decimal Costo { get; set; }
    }
    /// <summary>
    /// Clase para la creación de la relación de un insumo por contratista
    /// </summary>
    public class InsumoXContratistaCreacionDTO
    {
        /// <summary>
        /// Identificador único del <c>Insumo</c>
        /// </summary>
        public int IdInsumo { get; set; }
        /// <summary>
        /// Identificador único del <c>Contratista</c>
        /// </summary>
        public int IdContratista { get; set; }
        /// <summary>
        /// Código del insumo
        /// </summary>
        public string? Codigo { get; set; }
        /// <summary>
        /// Descripción del insumo
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Unidad del insumo
        /// </summary>
        public string? Unidad { get; set; }
        /// <summary>
        /// Costo que lleva el contratista para el insumo
        /// </summary>
        public decimal Costo { get; set; }
    }
}
