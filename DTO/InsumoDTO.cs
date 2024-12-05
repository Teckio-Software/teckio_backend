

namespace ERP_TECKIO
{
    /// <summary>
    /// Clase <c>InsumoDTO</c> que implementa la interfaz <seealso cref="IInsumo"/>
    /// </summary>
    public class InsumoDTO
    {
        /// <summary>
        /// Identificador del Insumo
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Descripción del insumo
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Unidad en la que se mide el insumo
        /// </summary>
        public string Unidad { get; set; }
        /// <summary>
        /// Codigo del insumo
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Identificador unico del <c>TipoInsumo</c> <seealso cref="TipoInsumoDTO"/>
        /// </summary>
        public int idTipoInsumo { get; set; }
        /// <summary>
        /// Identificador unico del <c>FamiliaInsumo</c> <seealso cref="FamiliaInsumoDTO"/>
        /// </summary>
        public int? idFamiliaInsumo { get; set; }
        /// <summary>
        /// Descripcion del tipo de insumo cuyo id sea <c>idTipoInsumo</c> <seealso cref="TipoInsumoDTO"/>
        /// </summary>
        public string DescripcionTipoInsumo { get; set; } = string.Empty;
        /// <summary>
        /// Descripcion de la familia de insumo cuyo id sea <c>idFamiliaInsumo</c> <seealso cref="FamiliaInsumoCreacionDTO"/>
        /// </summary>
        public string DescripcionFamiliaInsumo { get; set; } = string.Empty;
        public decimal CostoUnitario { get; set; }
        public int IdProyecto { get; set; }
    }

    /// <summary>
    /// Clase que contiene los parametros necesarios para crear un nuevo <c>Insumo</c>
    /// </summary>
    public class InsumoCreacionDTO
    {
        /// <summary>
        /// Codigo del insumo
        /// </summary>
        public string Codigo { get; set; } = string.Empty;
        /// <summary>
        /// Descripción del insumo
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;
        /// <summary>
        /// Unidad en la que se mide el insumo
        /// </summary>
        public string Unidad { get; set; } = string.Empty;
        /// <summary>
        /// Identificador unico del <c>TipoInsumo</c> <seealso cref="TipoInsumoDTO"/>
        /// </summary>
        public int idTipoInsumo { get; set; }
        /// <summary>
        /// Identificador unico del <c>FamiliaInsumo</c> <seealso cref="FamiliaInsumoDTO"/>
        /// </summary>
        public int? idFamiliaInsumo { get; set; }
        /// <summary>
        /// Código por el que se busca el nuevo insumo (new guid)
        /// </summary>
        public string? CodigoBusqueda { get; set; }
        public decimal CostoUnitario { get; set; }
        public int IdProyecto { get; set; }
    }

    /// <summary>
    /// Clase que contiene los parametros para filtrar el registro y paginarlo
    /// </summary>
    public class InsumoFiltrarDTO
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
            get { return new PaginacionDTO() { Pagina = pagina, RecordsPorPagina = recordsPorPagina }; }
        }
        /// <summary>
        /// Código del Insumo
        /// </summary>
        public string? codigo { get; set; }
        /// <summary>
        /// Descripción del Insumo
        /// </summary>
        public string? descripcion { get; set; }
        /// <summary>
        /// Unidad del Isumo
        /// </summary>
        public string? unidad { get; set; }
    }
    /// <summary>
    /// Clase para la creación y devolución de insumo que no estaba presupuestado
    /// </summary>
    public class InsumoCreacionDevolucionCodigoBusquedaDTO
    {
        /// <summary>
        /// Codigo del insumo
        /// </summary>
        public string Codigo { get; set; } = string.Empty;
        /// <summary>
        /// Descripción del insumo
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;
        /// <summary>
        /// Unidad en la que se mide el insumo
        /// </summary>
        public string Unidad { get; set; } = string.Empty;
        /// <summary>
        /// Identificador unico del <c>TipoInsumo</c> <seealso cref="TipoInsumoDTO"/>
        /// </summary>
        public int idTipoInsumo { get; set; }
        /// <summary>
        /// Identificador unico del <c>FamiliaInsumo</c> <seealso cref="FamiliaInsumoDTO"/>
        /// </summary>
        public int idFamiliaInsumo { get; set; }
        /// <summary>
        /// Código por el que se busca el nuevo insumo (new guid)
        /// </summary>
        public string? CodigoBusqueda { get; set; }
    }

    public class InsumoParaExplosionDTO : InsumoDTO
    {
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }
        public string CostoUnitarioConFormato{ get; set; }
        public string CantidadConFormato { get; set; }
        public string ImporteConFormato { get; set; }
    }

    public class IdsProyectosParaMigrarInsumoDTO
    {
        public int IdProyectoActual { get; set; }
        public int IdProyectoAMigrar { get; set; }
    }
}
