namespace ERP_TECKIO
{
    /// <summary>
    /// Este DTO sirve para la visualización de insumos en una Salida de almacén
    /// </summary>
    public class AlmacenSalidaInsumoDTO
    {
        public int Id { get; set; }
        public int? IdProyecto { get; set; }
        public int IdAlmacenSalida { get; set; }
        public string CodigoSalida { get; set; }
        public int IdInsumo { get; set; }
        //public string? CodigoInsumo { get; set; }
        public string? Descripcioninsumo { get; set; }
        public string? Unidadinsumo { get; set; }
        public decimal CantidadPorSalir { get; set; }
        /// <summary>
        /// 1 = capturado, 2 = autorizado, 3 = cancelado
        /// </summary>
        public int EstatusInsumo { get; set; }
            
        public bool EsPrestamo { get; set; }
        public bool PrestamoFinalizado { get; set; }
        public string PersonaRecibio { get; set; }
        public bool Seleccionado { get; set; }  
    }
    /// <summary>
    /// Para la asignación de los insumos en una nueva Salida de almacén
    /// </summary>
    public class AlmacenSalidaInsumoCreacionDTO
    {
        //public int IdProyecto { get; set; }
        //public int IdAlmacenSalida { get; set; }
        //public int IdInsumoExistencia { get; set; }
        public int IdSalidaAlmacen { get; set; }

        public int IdInsumo { get; set; }
        public decimal CantidadPorSalir { get; set; }
        public bool EsPrestamo { get;set; }
        /// <summary>
        /// 1 = capturado, 2 = autorizado, 3 = cancelado
        /// </summary>
        //public int Estatus { get; set; }
    }
}
