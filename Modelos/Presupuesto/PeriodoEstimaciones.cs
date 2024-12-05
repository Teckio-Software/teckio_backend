

namespace ERP_TECKIO.Modelos
{
    public class PeriodoEstimaciones
    {
        public int Id { get; set; }

        public int IdProyecto { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaTermino { get; set; }

        public int NumeroPeriodo { get; set; }
        public bool EsCerrada { get; set; }
        public virtual ICollection<Estimaciones> Estimaciones { get; set; } = new List<Estimaciones>();
        public virtual Proyecto IdProyectoNavigation { get; set; } = null!;
    }
}
