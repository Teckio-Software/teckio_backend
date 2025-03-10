namespace ERP_TECKIO.DTO
{
    public class FsrxinsummoMdODTO
    {
        public int Id { get; set; }

        public decimal CostoDirecto { get; set; }

        public decimal CostoFinal { get; set; }

        public decimal Fsr { get; set; }

        public int IdInsumo { get; set; }

        public int IdProyecto { get; set; }
    }

    public class EstructuraFsrxinsummoMdODTO : FsrxinsummoMdODTO
    {
        public List<FsrxinsummoMdOdetalleDTO> detalles { get; set; } = new List<FsrxinsummoMdOdetalleDTO>();
    }
}
