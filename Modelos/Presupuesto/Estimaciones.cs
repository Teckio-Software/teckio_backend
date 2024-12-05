
namespace ERP_TECKIO.Modelos
{
    public class Estimaciones
    {
        public int Id { get; set; }

        public int IdPrecioUnitario { get; set; }

        public int IdPadre { get; set; }

        public int IdConcepto { get; set; }

        public int IdProyecto { get; set; }

        public decimal ImporteDeAvance { get; set; }

        public decimal PorcentajeAvance { get; set; }

        public decimal CantidadAvance { get; set; }

        public decimal ImporteDeAvanceAcumulado { get; set; }

        public decimal PorcentajeAvanceAcumulado { get; set; }

        public decimal CantidadAvanceAcumulado { get; set; }

        public int IdPeriodo { get; set; }
        public int TipoPrecioUnitario { get; set; }

        public virtual Concepto IdConceptoNavigation { get; set; } = null!;

        public virtual PeriodoEstimaciones IdPeriodoNavigation { get; set; } = null!;

        public virtual PrecioUnitario IdPrecioUnitarioNavigation { get; set; } = null!;

        public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
    }
}
