namespace ERP_TECKIO
{
    /// <summary>
    /// EspecialIdades por contratista
    /// </summary>
    public interface IEspecialIdadXContratista
    {
        /// <summary>
        /// Identificador de la especialIdad por contratista
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la <c>EspecialIdad</c>
        /// </summary>
        public int IdEspecialIdad { get; set; }
        /// <summary>
        /// Identificador del <c>Contratista</c>
        /// </summary>
        public int IdContratista { get; set; }
        /// <summary>
        /// Costo de la especialIdad por el contratista
        /// </summary>
        public decimal Costo { get; set; }
    }
}
