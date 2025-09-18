namespace ERP_TECKIO.DTO
{
    public class TranspasoAlmacenDTO
    {
        public int IdAlmacenOrigen { get; set; }
        public int IdAlmacenDestino { get; set; }
        public List<ExistenciaTranspasoDTO> Insumos { get; set; }
    }
}
