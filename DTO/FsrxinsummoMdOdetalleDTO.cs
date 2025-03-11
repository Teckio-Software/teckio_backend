namespace ERP_TECKIO.DTO
{
    public class FsrxinsummoMdOdetalleDTO
    {
        public int Id { get; set; }

        public string Codigo { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public string ArticulosLey { get; set; } = null!;

        public decimal PorcentajeFsr { get; set; }

        public int IdFsrxinsummoMdO { get; set; }
        public int IdInsumo { get; set; }
        public int IdProyecto { get; set; }
    }
}
