namespace ERP_TECKIO
{
    /// <summary>
    /// Interfaz del tipo de poliza
    /// </summary>
    public interface ITipoPoliza
    {
        /// <summary>
        /// Identificador del tipo de poliza
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Código del tipo de poliza
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Descripción del tipo de poliza
        /// </summary>
        public string Descripcion { get; set; }
        public int Naturaleza { get; set; }
    }
    /////////////////////////////////////////////
    public interface ITeckioTipoPoliza
    {
        /// <summary>
        /// Identificador del tipo de poliza
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Código del tipo de poliza
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Descripción del tipo de poliza
        /// </summary>
        public string Descripcion { get; set; }
    }
}
