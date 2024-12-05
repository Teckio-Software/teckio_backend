
namespace ERP_TECKIO
{
    /// <summary>
    /// Clase <c>RequisicionDTO</c> que implementa la interfaz <seealso cref="IRequisicion"/>
    /// </summary>
    public class RequisicionDTO
    {
        /// <summary>
        /// Identificador de la Requisición
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de <see cref="Proyecto"/>
        /// </summary>
        public int IdProyecto { get; set; }
        /// <summary>
        /// Numero de la requisición
        /// </summary>
        public string NoRequisicion { get; set; }
        /// <summary>
        /// Nombre de la persona que solicita la requisición
        /// </summary>
        public string PersonaSolicitante { get; set; }
        /// <summary>
        /// Observaciones de la requisición
        /// </summary>
        public string? Observaciones { get; set; }
        /// <summary>
        /// Fecha de creación de la requisición
        /// </summary>
        public DateTime FechaRegistro { get; set; }
        /// <summary>
        /// Estatus de la requisición
        /// </summary>
        public int EstatusRequisicion { get; set; }
        /// <summary>
        /// Estatus de la requisición
        /// </summary>
        public int EstatusInsumosComprados { get; set; }
        public string EstatusInsumosCompradosDescripcion { get; set; }
        /// <summary>
        /// Estatus de la requisición
        /// </summary>
        public int EstatusInsumosSurtIdos { get; set; }
        public string EstatusInsumosSurtIdosDescripcion { get; set; }
        public string? Residente { get; set; }
    }

    public class ListaRequisicionDTO : RequisicionDTO
    {

    }
    /// <summary>
    /// Este DTO es para dar de alta nuevas requisiciones
    /// </summary>
    public class RequisicionCreacionDTO
    {
        /// <summary>
        /// Identificador de <see cref="Proyecto"/>
        /// </summary>
        public int IdProyecto { get; set; }
        /// <summary>
        /// Observaciones de la requisición
        /// </summary>
        public string? Observaciones { get; set; }
        /// <summary>
        /// Lista <c>ListaInsumosRequisicion</c> del tipo <seealso cref="InsumoXRequisicionCreacionDTO"/>
        /// la cual es un objeto que contiene los datos para almacenar un registro del tipo
        /// <seealso cref="InsumoXRequisicionDTO"/>
        /// </summary>
        public string? Residente { get; set; }

        public List<InsumoXRequisicionCreacionDTO> ListaInsumosRequisicion { get; set; }
    }

    public class RequisicionBuscarDTO
    {
        public int IdProyecto { get; set; }
        public string NoRequisicion { get; set; }
    }
    /// <summary>
    /// Este DTO es para las busquedas de la pantalla principal
    /// </summary>
    public class RequisicionBusquedaExtensaDTO
    {
        /// <summary>
        /// Pagina en la que se encuentra el paginado
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
            get { return new PaginacionDTO() { Pagina = Convert.ToInt32(pagina), RecordsPorPagina = Convert.ToInt32(recordsPorPagina) }; }
        }
        public int IdProyecto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool EsBusquedaPorFechas { get; set; }
        public int Estatus { get; set; }
    }

}
