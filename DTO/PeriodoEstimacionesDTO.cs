
namespace ERP_TECKIO
{
    public class PeriodoEstimacionesDTO
    {
        public int Id { get; set; }
        public int IdProyecto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public int NumeroPeriodo { get; set; }
        public string DescripcionPeriodo { get; set; }
        public bool EsCerrada { get; set; }
    }

    public class PeriodosResumenDTO : PeriodoEstimacionesDTO
    {
        public decimal Importe { get; set; } 
        public string ImporteConFormato { get; set; }
        public decimal Avance { get; set; }
        public string AvanceConFormato { get; set; }
    }
}
