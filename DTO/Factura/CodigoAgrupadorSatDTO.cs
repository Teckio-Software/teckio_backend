namespace ERP_TECKIO
{
    /// <summary>
    /// Clase DTO para pasar la información en el front end que se implementa con la interfaz <see cref="ICodigoAgrupador"/>
    /// </summary>
    public class CodigoAgrupadorSatDTO
    {
        /// <summary>
        /// Identificador único del código agrupador del SAT
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nivel del código agrupador
        /// </summary>
        public int Nivel { get; set; }
        /// <summary>
        /// Código agrupador
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Descripción del código agrupador
        /// </summary>
        public string Descripcion { get; set; }
    }

    public class CodigoAgrupadorSatCreacionDTO
    {
        /// <summary>
        /// Nivel del código agrupador
        /// </summary>
        public int Nivel { get; set; }
        /// <summary>
        /// Código agrupador
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Descripción del código agrupador
        /// </summary>
        public string Descripcion { get; set; }
    }
}
