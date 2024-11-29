namespace ERP_TECKIO
{
    public class RubroDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;
        public int NaturalezaRubro { get; set; }
        public int TipoReporte { get; set; }
        public int Posicion { get; set; }
    }

    public class RubroCreacionDTO
    {
        public string Descripcion { get; set; }
        public int NaturalezaRubro { get; set; } //1 = Deudor, 2 = Acreedor
        public int TipoReporte { get; set; } //1 = reporte estado de resultados, 2 = reporte estado de posición financiera
        public int Posicion { get; set; }
    }
}
