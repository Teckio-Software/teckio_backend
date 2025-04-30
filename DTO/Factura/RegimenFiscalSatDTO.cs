namespace ERP_TECKIO.DTO.Factura
{
    public class RegimenFiscalSatDTO
    {
        public int Id { get; set; }

        public string Clave { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public int TipoRegimenFiscal { get; set; }
    }
}
