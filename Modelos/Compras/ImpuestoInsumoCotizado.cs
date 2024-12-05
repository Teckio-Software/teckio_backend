
namespace ERP_TECKIO.Modelos
{
    public partial class ImpuestoInsumoCotizado
    {
        public int Id { get; set; }

        public int IdInsumoCotizado { get; set; }

        public int IdImpuesto { get; set; }

        public decimal Porcentaje { get; set; }

        public decimal Importe { get; set; }

        public virtual TipoImpuesto IdImpuestoNavigation { get; set; } = null!;

        public virtual InsumoXCotizacion IdInsumoCotizadoNavigation { get; set; } = null!;
    }
}
