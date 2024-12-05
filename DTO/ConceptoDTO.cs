namespace ERP_TECKIO
{
    public class ConceptoDTO
    {
        /// <summary>
        /// Identificador del Concepto
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Codigo del concepto
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Descripción del concepto
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Unidad en la que se mide el Concepto
        /// </summary>
        public string Unidad { get; set; }
        public int? IdEspecialidad { get; set; }
        public string? DescripcionEspecialidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal PorcentajeIndirecto { get; set; }
        public decimal IdProyecto { get; set; }
    }

    /// <summary>
    /// Clase que contiene los parametros para crear un nuevo Concepto
    /// </summary>
    public class ConceptoCreacionDTO
    {
        /// <summary>
        /// Codigo del concepto
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Descripción del concepto
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Unidad en la que se mide el Concepto
        /// </summary>
        public string Unidad { get; set; }
        /// <summary>
        /// Identificador unico de <c>Especialidad</c> <seealso cref="ProyectoDTO"/>
        /// </summary>
        public int IdEspecialidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal IdProyecto { get; set; }
    }
}
