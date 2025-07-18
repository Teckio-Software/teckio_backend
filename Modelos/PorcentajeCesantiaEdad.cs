namespace ERP_TECKIO.Modelos
{
    public class PorcentajeCesantiaEdad : PorcentajeCesantiaEdadAbstract
    {
        public Proyecto IdProyectoNavigation { get; set; }

    }

    public abstract class PorcentajeCesantiaEdadAbstract
    {
        public int Id { get; set; }

        public int IdProyecto { get; set; }

        public decimal RangoUMA { get; set; }

        public decimal Porcentaje { get; set; }

    }
}
