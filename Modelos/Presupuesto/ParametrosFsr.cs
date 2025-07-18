namespace ERP_TECKIO.Modelos.Presupuesto
{
    public class ParametrosFsr : ParametrosFsrAbstract
    {
        public Proyecto IdProyectoNavigation { get; set; }

    }

    public abstract class ParametrosFsrAbstract
    {
        public int Id { get; set; }

        public int IdProyecto { get; set; }

        public decimal RiesgoTrabajo { get; set; }

        public decimal CuotaFija { get; set; }

        public decimal AplicacionExcedente { get; set; }

        public decimal PrestacionDinero { get; set; }

        public decimal GastoMedico { get; set; }

        public decimal InvalidezVida { get; set; }

        public decimal Retiro { get; set; }

        public decimal PrestaconSocial { get; set; }

        public decimal Infonavit { get; set; }

        public decimal UMA { get; set; }

    }
}
