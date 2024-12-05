
namespace ERP_TECKIO
{
    public class ReporteDestajoDTO
    {
        public int IdPeriodoEstimacion { get; set; }
        public int IdContratista { get; set; }
        public int IdContrato { get; set; }
        public int IdPrecioUnitario { get; set; }
        public int IdProyecto { get; set; }
        public string Descripcion { get; set; }
        public decimal PorcentajeDestajo { get; set; }
        public string PorcentajeDestajoConFormato { get; set; }
        public decimal PorcentajeEstimacion { get; set; }
        public string porcentajeEstimacionConFormato { get; set; }
        public decimal Importe { get; set; }
        public string ImporteConFormato { get; set; }
        public decimal ImporteDestajo { get; set; }
        public string ImporteDestajoConFormato { get; set; }
        public decimal PorcentajePago { get; set; }
        public string PorcentajePagoConFormato { get; set; }
        public decimal Acumulado { get; set; }
        public string AcumuladoConFormato { get; set; }
    }

    public class ObjetoDestajoacumuladoDTO
    {
        public List<DetalleXContratoParaTablaDTO> destajos { get; set; }
        public List<PeridoAcumuladosDTO> periodos { get; set; }

    }

    public class PeridoAcumuladosDTO : PeriodoEstimacionesDTO
    {
        public List<AcumuladosDTO> detalles { get; set; } = new List<AcumuladosDTO>();

    }

    public class AcumuladosDTO
    {
        public int Id { get; set; }
        public decimal Importe { get; set; }
        public string ImporteConFormato { get; set; }
        public decimal Avance { get; set; }
        public string AvanceConFormato { get; set; }
    }



    public class ObjetoDestajoTotalDTO
    {
        public List<ContaratistaImporteDTO> contratistas { get; set; } = new List<ContaratistaImporteDTO>();
        public List<PeriodosTotalesDTO> periodos { get; set; } = new List<PeriodosTotalesDTO>();
    }

    public class ContaratistaImporteDTO
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public decimal Importe { get; set; }
        public string ImporteConFormato { get; set; }

    }

    public class PeriodosTotalesDTO : PeriodoEstimacionesDTO
    {
        public List<TotalesDTO> totales { get; set; } = new List<TotalesDTO>();
    }

    public class TotalesDTO
    {
        public int IdContratista { get; set; }
        public decimal Importe { get; set; }
        public string ImporteConFormato { get; set; }
        public decimal Avance { get; set; }
        public string AvanceConFormato { get; set; }
    }

}
