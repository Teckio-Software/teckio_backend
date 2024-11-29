namespace ERP_TECKIO
{
    public class AlmacenSalidaDTO
    {
        public int Id { get; set; }
        public string CodigoSalida { get; set; }
        public int IdAlmacen { get; set; }
        public string AlmacenNombre { get; set; }
        //public bool EsAlmacenCentral { get; set; }
        public DateTime FechaRegistro { get; set; }
        //public DateTime FechaAutorizacionSalida { get; set; }
        //public DateTime FechaEntregaSalida { get; set; }
        //public int IdProyecto { get; set; }
        //public string NombreProyecto { get; set; }
        public string? CodigoCreacion { get; set; }
        public string? Observaciones { get; set; }
        /// <summary>
        /// 1 = Capturado, 2 = Autorizado, 3 = Cancelado
        /// </summary>
        public int Estatus { get; set; }
        public string? PersonaSurtio { get; set; }
        public string? PersonaRecibio { get; set; }

        public string? Autorizo { get; set; }
        public bool EsBaja { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class AlmacenSalidaCreacionDTO
    {
        public int IdAlmacen { get; set; }
        public string? Observaciones { get; set; }
        public string? PersonaRecibio { get; set; }
        public int IdProyecto { get; set; }
        public bool EsBaja { get; set; } 

        public List<AlmacenSalidaInsumoCreacionDTO> ListaAlmacenSalidaInsumoCreacion { get; set; }
    }

    public class InsumosExistenciaDTO
    {
        public int IdInsumo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public decimal CantidadDisponible { get; set; }
    }
}
