namespace ERP_TECKIO.Modelos
{
    public partial class Imagen: ImagenAbstract
    {
        public virtual ICollection<ParametrosImpresionPu> ParametrosImpresionPus { get; set; } = new List<ParametrosImpresionPu>();

    }

    public abstract class ImagenAbstract
    {
        public int Id { get; set; }

        public string Ruta { get; set; } = null!;

    }
}
