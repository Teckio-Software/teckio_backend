namespace ERP_TECKIO
{
    /// <summary>
    /// Este DTO es para la visualización de información de las existencia en almacen
    /// </summary>
    public class AlamcenExistenciaInsumoDTO
    {
        public int Id { get; set; }
        public int IdInsumo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public int IdProyecto { get; set; }
        public int IdAlmacen { get; set; }
        public string AlmacenNombre { get; set; }
        public bool EsCentral { get; set; }
        public decimal CantidadExistente { get; set; }
        public decimal CantidadInsumosAumenta { get; set; }
        public decimal CantidadInsumosRetira { get; set; }
        public bool EsNoConsumible { get; set; }
    }

}
