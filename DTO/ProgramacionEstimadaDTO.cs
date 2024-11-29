namespace ERP_TECKIO
{
    public class ProgramacionEstimadaDTO
    {
        public int Id { get; set; }
        public int IdProyecto { get; set; }
        public bool EsSabado { get; set; }
        public bool EsDomingo { get; set; }
        public int NoSerie { get; set; }
        public int IdPrecioUnitario { get; set; }
        public decimal? Cantidad { get; set; }
        public int TipoPrecioUnitario { get; set; }
        public DateTime Inicio { get; set; }
        public long InicioParaGantt { get; set; }
        public DateTime Termino { get; set; }
        public long TerminoParaGantt { get; set; }
        public int IdPredecesora { get; set; }
        public int DiasTranscurridos { get; set; }
        public int Nivel { get; set; }
        public int IdPadre { get; set; }
        public string Predecesor { get; set; }
        public int IdConcepto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public decimal? CostoUnitario { get; set; }
        public decimal? Importe { get; set; }
        public decimal Progreso { get; set; }
        public int Comando { get; set; }
        public bool Expandido { get; set; }
        public int Numerador { get; set; }
        public int DiasComando { get; set; }
        public List<int> sucesoras { get; set; }
        public List<ProgramacionEstimadaDTO> Hijos { get; set; }
    }

    public class registrosParaEnumerarDTO
    {
        public List<ProgramacionEstimadaDTO> registros { get; set; }
        public int numerador { get; set; }
    }
}
