
namespace ERP_TECKIO.Modelos
{
    public partial class DetalleXContrato
    {
        public int Id { get; set; }

        public int IdPrecioUnitario { get; set; }

        public int IdContrato { get; set; }

        public decimal PorcentajeDestajo { get; set; }

        public decimal ImporteDestajo { get; set; }

        public decimal? FactorDestajo { get; set; }

        public virtual Contrato IdContratoNavigation { get; set; } = null!;

        public virtual PrecioUnitario IdPrecioUnitarioNavigation { get; set; } = null!;
    }
}