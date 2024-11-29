namespace ERP_TECKIO
{
    public class AlmacenEntradaDTO
    {
        public int Id { get; set; }
        public int IdAlmacen { get; set; }

        public string NombreAlmacen { get; set; }
        public int? IdContratista { get; set; }

        public string RepresentanteLegal {  get; set; }
        public string NoEntrada { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string PersonaRegistra { get; set; }
        public string? Observaciones { get; set; }
        public int Estatus { get; set; }
    }

    public class AlmacenEntradaCreacionDTO
    {
        public int IdAlmacen { get; set; }
        public int IdContratista { get; set; }
        public string? Observaciones { get; set; }
        public List<AlmacenEntradaInsumoCreacionDTO> ListaInsumosEnAlmacenEntrada { get; set; }
    }

    public class AlmacenEntradaDevolucionCreacionDTO
    {
        public int IdAlmacen { get; set; }
        public int IdContratista { get; set; }
        public string? Observaciones { get; set; }
        public List<AlmacenSalidaInsumoDTO> listaInsumosPrestamo { get; set; }
    }
}
