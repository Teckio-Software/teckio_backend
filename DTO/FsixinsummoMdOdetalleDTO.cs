namespace ERP_TECKIO.DTO
{
    public class FsixinsummoMdOdetalleDTO
    {
        public int Id { get; set; }

        public string Codigo { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public string ArticulosLey { get; set; } = null!;

        public decimal Dias { get; set; }

        public bool? EsLaborableOpagado { get; set; }

        public int IdFsixinsummoMdO { get; set; }

        public int IdInsumo { get; set; }
        public int IdProyecto { get; set; }
    }
}
