namespace ERP_TECKIO
{
    /// <summary>
    /// Clase EspecialIdadDTO que implementa la interfaz <seealso cref="IEspecialIdad"/>
    /// </summary>
    public class EspecialidadDTO
    {
        /// <summary>
        /// Identificador de la EspecialIdad
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Codigo de la especialIdad
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Descripción de la especialIdad
        /// </summary>
        public string Descripcion { get; set; }
    }

    /// <summary>
    /// Clase que contiene los parametros para crear un nuevo registro
    /// </summary>
    public class EspecialIdadCreacionDTO
    {
        /// <summary>
        /// Codigo de la especialIdad
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Descripción de la especialIdad
        /// </summary>
        public string Descripcion { get; set; }
    }

    /// <summary>
    /// Clase que contiene los parametros para filtrar el registro y paginarlo
    /// </summary>
    public class EspecialIdadFiltrarDTO
    {
        /// <summary>
        /// Pagina en la que se encuentra del paginado
        /// </summary>
        public int pagina { get; set; }
        /// <summary>
        /// Cantidad de datos por pagina
        /// </summary>
        public int recordsPorPagina { get; set; }
        /// <summary>
        /// Objeto para paginar los registros dentro del filtrado
        /// </summary>
        public PaginacionDTO PaginacionDTO
        {
            get { return new PaginacionDTO() { Pagina = pagina, RecordsPorPagina = recordsPorPagina }; }
        }
        /// <summary>
        /// Codigo de la especialIdad
        /// </summary>
        public string? codigo { get; set; }
        /// <summary>
        /// Descripción de la especialIdad
        /// </summary>
        public string? descripcion { get; set; }
    }
}
