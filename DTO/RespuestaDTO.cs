namespace ERP_TECKIO
{
    /// <summary>
    /// Clase para confirmar las acciones dentro del Teckio
    /// </summary>
    public class RespuestaDTO
    {
        /// <summary>
        /// Para saber si se completo la acción
        /// </summary>
        public bool Estatus { get; set; }
        /// <summary>
        /// Descripción del mensaje
        /// </summary>
        public string Descripcion { get; set; }
    }
}
