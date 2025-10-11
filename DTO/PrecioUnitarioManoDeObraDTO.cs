namespace ERP_TECKIO.DTO
{
    public class PrecioUnitarioManoDeObraConjuntoDTO
    {
        public List<PrecioUnitarioManoDeObraDTO> PreciosUnitarios { get; set; } = new List<PrecioUnitarioManoDeObraDTO>();
        public decimal Total {  get; set; }
        public string TotalConFormato { get; set; } = "";
    }

    public class PrecioUnitarioManoDeObraDTO
    {
        public string? Codigo { get; set; } = "";
        public string? Descripcion { get; set; } = "";
        public decimal TotalDePU { get; set; }
        public string TotalConFormatoDePU { get; set; } = "";
        public List<InsumoParaExplosionDTO> Detalles { get; set; } = new List<InsumoParaExplosionDTO>();
    }
}
