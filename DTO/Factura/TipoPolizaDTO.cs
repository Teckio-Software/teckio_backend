
namespace ERP_TECKIO
{
    /// <summary>
    /// Clase <c>TipoPolizaDTO</c> que implementa la interfaz <seealso cref="ITipoPoliza"/>
    /// </summary>
    public abstract class TipoPolizaAbstractDTO
    {
        /// <summary>
        /// Identificador del tipo de poliza
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Descripción del tipo de poliza
        /// </summary>
        public string Descripcion { get; set; }
    }
    public partial class TipoPolizaDTO : TipoPolizaAbstractDTO
    {
        
   
        /// <summary>
        /// Código del tipo de poliza
        /// </summary>
        public string Codigo { get; set; }
        public int Naturaleza { get; set; }
    }

    /// <summary>
    /// Clase que contiene todos los atributos para crear un Tipo de Poliza
    /// </summary>
    public class TipoPolizaCreacionDTO
    {
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

}
