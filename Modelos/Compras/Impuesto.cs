
namespace ERP_TECKIO.Modelos
{
    public partial class Impuesto
    {
        public int Id { get; set; }

        public int Clave { get; set; }

        public string TipoImpuesto { get; set; } = null!;

        public virtual ICollection<ImpuestoInsumoCotizado> ImpuestoInsumoCotizados { get; set; } = new List<ImpuestoInsumoCotizado>();
    }
}
