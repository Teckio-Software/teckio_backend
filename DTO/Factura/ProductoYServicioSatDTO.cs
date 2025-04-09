namespace ERP_TECKIO.DTO.Factura
{
    public class ProductoYServicioSatDTO
    {
        public int Id { get; set; }

        public string Clave { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public bool Tipo { get; set; }
    }
}
