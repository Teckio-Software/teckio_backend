namespace ERP_TECKIO.Modelos
{
    public partial class Imagen: ImagenAbstract
    {
    }

    public abstract class ImagenAbstract
    {
        public int Id { get; set; }

        public string Ruta { get; set; } = null!;

        public bool Seleccionado { get; set; }

        public string? Tipo { get; set; }

    }
}
