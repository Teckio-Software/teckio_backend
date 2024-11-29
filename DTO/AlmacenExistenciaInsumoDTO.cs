namespace ERP_TECKIO
{
    /// <summary>
    /// Este DTO es para la visualización de información de las existencia en almacen
    /// </summary>
    public class AlmacenExistenciaInsumoDTO
    {
        public int Id { get; set; }
        public int IdInsumo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public string TipoExistencia { get; set; }
        public int IdProyecto { get; set; }
        public int IdAlmacen { get; set; }
        public string AlmacenNombre { get; set; }
        public bool EsCentral { get; set; }
        public decimal CantidadInsumosAumenta { get; set; }
        public decimal CantidadInsumosRetira { get; set; }
        public decimal CantidadInsumos { get; set; }
        public bool EsNoConsumible { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
    public class AlmacenExistenciaInsumoCreacionDTO
    {
        public int IdInsumo { get; set; }
        public int IdProyecto { get; set; }
        public int IdAlmacen { get; set; }
        public decimal CantidadInsumosAumenta { get; set; }
        public decimal CantidadInsumosRetira { get; set; } = 0;
        public bool EsNoConsumible { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
    /// <summary>
    /// Este DTO es para la actualización de las existencias en almacen
    /// </summary>
    public class ExistenciaActualizacionDTO
    {
        public int Id { get; set; }
        public int IdInsumo { get; set; }
        public decimal CantidadExistencia { get; set; }
        public int IdAlmacen { get; set; }
    }

}
