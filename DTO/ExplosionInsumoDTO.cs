namespace ERP_TECKIO
{
    /// <summary>
    /// Clase ExplosionInsumoDTO que implementa la interfaz <seealso cref="IExplosionInsumo"/>
    /// </summary>
    public class ExplosionInsumoDTO
    {
        /// <summary>
        /// Identificador de la Explosion de Insumo
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Codigo de la Explosión de Insumos
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Identificador del Concepto <seealso cref="ConceptoDTO"/>
        /// </summary>
        public int IdConcepto { get; set; }
        /// <summary>
        /// Codigo del Concepto
        /// </summary>
        public string ConceptoCodigo { get; set; }
        /// <summary>
        /// Descripcion del concepto
        /// </summary>
        public string ConceptoDescripcion { get; set; }
        /// <summary>
        /// Unidad del Concepto
        /// </summary>
        public string ConceptoUnidad { get; set; }
        /// <summary>
        /// Identificador del Insumo
        /// </summary>
        public int IdInsumo { get; set; }
        /// <summary>
        /// Codigo del insumo
        /// </summary>
        public string InsumoCodigo { get; set; }
        /// <summary>
        /// Descripción del Insumo
        /// </summary>
        public string InsumoDescripcion { get; set; }
        /// <summary>
        /// Unidad del Insumo
        /// </summary>
        public string InsumoUnidad { get; set; }
        /// <summary>
        /// Al solo poder almacenar un campo ya sea Insumo o Concepto
        /// el valor del codigo de dicho Insumo o Concepto se muestra
        /// en este campo
        /// </summary>
        public string ciCodigo { get; set; }
        /// <summary>
        /// Al solo poder almacenar un campo ya sea Insumo o Concepto
        /// el valor de la Descripcion de dicho Insumo o Concepto se muestra
        /// en este campo
        /// </summary>
        public string cIdescripcion { get; set; }
        /// <summary>
        /// Al solo poder almacenar un campo ya sea Insumo o Concepto
        /// el valor de la Unidad de dicho Insumo o Concepto se muestra
        /// en este campo
        /// </summary>
        public string ciUnidad { get; set; }
        /// <summary>
        /// Identificador del <seealso cref="ProyectoDTO"/>
        /// </summary>
        public int IdProyecto { get; set; }
        /// <summary>
        /// Nombre del <c>Proyecto</c> <seealso cref="ProyectoDTO"/>
        /// </summary>
        public string ProyectoNombre { get; set; }
        /// <summary>
        /// Costo de cada Insumo/Concepto
        /// </summary>
        public decimal CostoUnitario { get; set; }
        /// <summary>
        /// Cantidad de Insumos/Conceptos
        /// </summary>
        public decimal Cantidad { get; set; }
        /// <summary>
        /// Identificador del dato base de esta misma tabla
        /// </summary>
        public int IdPrecioBase { get; set; }
        /// <summary>
        /// Nivel del dato base de esta misma tabla
        /// </summary>
        public int Nivel { get; set; }
        /// <summary>
        /// Total del costo unitario por la Cantidad
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// Lista que carga los Hijos a partir del nivel
        /// </summary>
        public List<ExplosionInsumoHijoDTO> ListaHijosExplosionInsumos { get; set; }
    }

    /// <summary>
    /// Clase que contiene los parametros para crear un nuevo Registro
    /// </summary>
    public class ExplosionInsumoCreacionDTO
    {
        /// <summary>
        /// Código de la Explosion de Insumo
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Identifiador del <seealso cref="ConceptoDTO"/> que pertenece a la explosion
        /// </summary>
        public int IdConcepto { get; set; }
        /// Identifiador del <seealso cref="InsumoDTO"/> que pertenece a la explosion
        public int IdInsumo { get; set; }
        /// Identifiador del <seealso cref="ProyectoDTO"/> al que pertenece la explosion
        public int IdProyecto { get; set; }
        /// <summary>
        /// Costo del insumo o Concepto
        /// </summary>
        public decimal CostoUnitario { get; set; }
        /// <summary>
        /// Cantidad del insumo o Concepto
        /// </summary>
        public decimal Cantidad { get; set; }
        /// <summary>
        /// Identificador del precibo base de la misma tabla
        /// </summary>
        public int IdPrecioBase { get; set; }
        /// <summary>
        /// Nivel del registro de la tabla
        /// </summary>
        public int Nivel { get; set; }
        /// <summary>
        /// Total de la compra
        /// </summary>
        public decimal Total { get; set; }
    }

    /// <summary>
    /// Clase que permite mostrar los hijos a partir del nivel
    /// de la tabla de explosion de insumos
    /// </summary>
    public class ExplosionInsumoHijoDTO
    {
        /// <summary>
        /// Identificador de la Explosion de Insumo
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Codigo de la Explosión de Insumos
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Identificador del Concepto <seealso cref="ConceptoDTO"/>
        /// </summary>
        public int IdConcepto { get; set; }
        /// <summary>
        /// Codigo del Concepto
        /// </summary>
        public string ConceptoCodigo { get; set; }
        /// <summary>
        /// Descripcion del concepto
        /// </summary>
        public string ConceptoDescripcion { get; set; }
        /// <summary>
        /// Unidad del Concepto
        /// </summary>
        public string ConceptoUnidad { get; set; }
        /// <summary>
        /// Identificador del Insumo
        /// </summary>
        public int IdInsumo { get; set; }
        /// <summary>
        /// Codigo del insumo
        /// </summary>
        public string InsumoCodigo { get; set; }
        /// <summary>
        /// Descripción del Insumo
        /// </summary>
        public string InsumoDescripcion { get; set; }
        /// <summary>
        /// Unidad del Insumo
        /// </summary>
        public string InsumoUnidad { get; set; }
        /// <summary>
        /// Al solo poder almacenar un campo ya sea Insumo o Concepto
        /// el valor del codigo de dicho Insumo o Concepto se muestra
        /// en este campo
        /// </summary>
        public string ciCodigo { get; set; }
        /// <summary>
        /// Al solo poder almacenar un campo ya sea Insumo o Concepto
        /// el valor de la Descripcion de dicho Insumo o Concepto se muestra
        /// en este campo
        /// </summary>
        public string cIdescripcion { get; set; }
        /// <summary>
        /// Al solo poder almacenar un campo ya sea Insumo o Concepto
        /// el valor de la Unidad de dicho Insumo o Concepto se muestra
        /// en este campo
        /// </summary>
        public string ciUnidad { get; set; }
        /// <summary>
        /// Identificador del <seealso cref="ProyectoDTO"/>
        /// </summary>
        public int IdProyecto { get; set; }
        /// <summary>
        /// Nombre del <c>Proyecto</c> <seealso cref="ProyectoDTO"/>
        /// </summary>
        public string ProyectoNombre { get; set; }
        /// <summary>
        /// Costo de cada Insumo/Concepto
        /// </summary>
        public decimal CostoUnitario { get; set; }
        /// <summary>
        /// Cantidad de Insumos/Conceptos
        /// </summary>
        public decimal Cantidad { get; set; }
        /// <summary>
        /// Identificador del dato base de esta misma tabla
        /// </summary>
        public int IdPrecioBase { get; set; }
        /// <summary>
        /// Nivel del dato base de esta misma tabla
        /// </summary>
        public int Nivel { get; set; }
        /// <summary>
        /// Total del costo unitario por la Cantidad
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// Lista que carga los Hijos a partir del nivel
        /// </summary>
        public List<ExplosionInsumoHijoDTO> ListaHijosExplosionInsumos { get; set; }
    }

    /// <summary>
    /// Clase <c>ExplosionInsumoHijoCreacionDTO</c> que permite crear un hijo de InsumoCreacion
    /// </summary>
    public class ExplosionInsumoHijoCreacionDTO
    {
        /// <summary>
        /// Código de la Explosion de Insumo
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Identifiador del <seealso cref="ConceptoDTO"/> que pertenece a la explosion
        /// </summary>
        public int IdConcepto { get; set; }
        /// Identifiador del <seealso cref="InsumoDTO"/> que pertenece a la explosion
        public int IdInsumo { get; set; }
        /// Identifiador del <seealso cref="ProyectoDTO"/> al que pertenece la explosion
        public int IdProyecto { get; set; }
        /// <summary>
        /// Costo del insumo o Concepto
        /// </summary>
        public decimal CostoUnitario { get; set; }
        /// <summary>
        /// Cantidad del insumo o Concepto
        /// </summary>
        public decimal Cantidad { get; set; }
        /// <summary>
        /// Identificador del precibo base de la misma tabla
        /// </summary>
        public int IdPrecioBase { get; set; }
        /// <summary>
        /// Nivel del registro de la tabla
        /// </summary>
        public int Nivel { get; set; }
        /// <summary>
        /// Total de la compra
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// Lista que carga los Hijos a partir del nivel
        /// </summary>
        public List<ExplosionInsumoHijoDTO> ListaExplosionInsumo { get; set; }
    }
    /// <summary>
    /// Este DTO se usa para la busqueda en uan nueva requisición
    /// </summary>
    public class InsumoProyectoBusquedaDTO
    {
        /// <summary>
        /// Identificador único del proyecto
        /// </summary>
        public int IdProyecto { get; set; }
        /// <summary>
        /// Descripción del insumo
        /// </summary>
        public string descripcionInsumo { get; set; }
    }
}
