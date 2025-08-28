
namespace ERP_TECKIO.Modelos
{
    public partial class ParametrosImpresionPu : ParametrosImpresionPuAbstract
    {
        public virtual Imagen? IdImagenNavigation { get; set; }
    }

    public abstract class ParametrosImpresionPuAbstract
    {
        public int Id { get; set; }

        public string EncabezadoIzquierdo { get; set; } = null!;

        public string EncabezadoCentro { get; set; } = null!;

        public string EncabezadoDerecho { get; set; } = null!;

        public string PieIzquierdo { get; set; } = null!;

        public string PieCentro { get; set; } = null!;

        public string PieDerecho { get; set; } = null!;

        public int? IdImagen { get; set; }

        public decimal? MargenSuperior { get; set; }

        public decimal? MargenInferior { get; set; }

        public decimal? MargenDerecho { get; set; }

        public decimal? MargenIzquierdo { get; set; }

        public string? Nombre { get; set; }
    }
}
