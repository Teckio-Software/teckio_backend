namespace ERP_TECKIO.DTO
{
    public class FsixinsummoMdODTO
    {
        public int Id { get; set; }

        public decimal DiasNoLaborales { get; set; }

        public decimal DiasPagados { get; set; }

        public decimal Fsi { get; set; }

        public int IdInsumo { get; set; }

        public int IdProyecto { get; set; }
    }

    public class EstructuraFsixinsummoMdODTO : FsixinsummoMdODTO
    {
        public List<FsixinsummoMdOdetalleDTO> detalles { get; set; } = new List<FsixinsummoMdOdetalleDTO>();
    }
}
