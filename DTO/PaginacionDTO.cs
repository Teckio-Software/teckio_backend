namespace ERP_TECKIO {
    /// <summary>
    /// Clase <c>PaginacionDTO</c> que se usa para insertar los parametros de paginación
    /// </summary>
    public class PaginacionDTO
    {
        /// <summary>
        /// Pagina que por defecto inicia en la 1
        /// </summary>
        public int Pagina { get; set; } = 1;
        /// <summary>
        /// Cantidad de registros a mostrar que por defecto inicia en uno
        /// </summary>
        private int recordsPorPagina = 10;
        /// <summary>
        /// Cantidad maxima de registros a mostrar por pagina que es 50
        /// </summary>
        private readonly int CantidadMaximaRecordsPorPagina = 50;
        /// <summary>
        /// Cantidad de registros por pagina
        /// </summary>
        public int RecordsPorPagina
        {
            get
            {
                return recordsPorPagina;
            }
            set
            {
                recordsPorPagina = (value > CantidadMaximaRecordsPorPagina) ? CantidadMaximaRecordsPorPagina : value;
            }
        }
    }

}

