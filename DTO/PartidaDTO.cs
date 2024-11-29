namespace ERP_TECKIO
{
    /// <summary>
    /// Clase PartIdaDTO que implementa la interfaz <seealso cref="IPartIda"/>
    /// </summary>
    public class PartIdaDTO
    {
        /// <summary>
        /// Identificador de PartIdas
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Descripción de la partIda
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Observaciones de la partIda
        /// </summary>
        public string Observaciones { get; set; }
        /// <summary>
        /// Nivel de la partIda
        /// </summary>
        public int Nivel { get; set; }
    }

    /// <summary>
    /// Clase que contiene los parametros para crear un nuevo registro
    /// en la tabla de <c>PartIdas</c>
    /// </summary>
    public class PartIdaCreacionDTO
    {
        /// <summary>
        /// Descripción de la partIda
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Observaciones de la partIda
        /// </summary>
        public string Observaciones { get; set; }
        /// <summary>
        /// Nivel de la partIda
        /// </summary>
        public int Nivel { get; set; }
    }
}
