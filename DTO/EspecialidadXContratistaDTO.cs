
namespace ERP_TECKIO
{
    /// <summary>
    /// Clase para mostrar las especialIdades por contratistas que hereda de la interfaz IEspecialIdadXContratista
    /// </summary>
    public class EspecialIdadXContratistaDTO
    {
        /// <summary>
        /// Identificador único de la especialIdad por contratista
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador único de la <see cref="EspecialidadDTO"/>
        /// </summary>
        public int IdEspecialIdad { get; set; }
        /// <summary>
        /// Identificador único del <see cref="ContratistaDTO"/>
        /// </summary>
        public int IdContratista { get; set; }
        /// <summary>
        /// Código de la especialIdad
        /// </summary>
        public string? Codigo { get; set; }
        /// <summary>
        /// Descripción de la especialIdad
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Costo de la especialIdad por el contratista
        /// </summary>
        public decimal Costo { get; set; }
        /// <summary>
        /// Unidad de la especialIdad
        /// </summary>
        public string Unidad { get; set; }
    }
    /// <summary>
    /// Clase para la creación de una especialIdad por contratista
    /// </summary>
    public class EspecialIdadXContratistaCreacionDTO
    {
        /// <summary>
        /// Identificador único de la <see cref="EspecialidadDTO"/>
        /// </summary>
        public int IdEspecialIdad { get; set; }
        /// <summary>
        /// Identificador único del <see cref="ContratistaDTO"/>
        /// </summary>
        public int IdContratista { get; set; }
        /// <summary>
        /// Costo de la especialIdad por el contratista
        /// </summary>
        public decimal Costo { get; set; }
        /// <summary>
        /// Unidad de la especialIdad
        /// </summary>
        public string Unidad { get; set; }
    }
}
