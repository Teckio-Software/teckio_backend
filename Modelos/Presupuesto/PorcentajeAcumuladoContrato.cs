
namespace ERP_TECKIO.Modelos
{
    public partial class PorcentajeAcumuladoContrato
    {
        public int Id { get; set; }

        public decimal PorcentajeDestajoAcumulado { get; set; }

        public int IdPrecioUnitario { get; set; }

        public virtual PrecioUnitario IdPrecioUnitarioNavigation { get; set; } = null!;
    }

}
