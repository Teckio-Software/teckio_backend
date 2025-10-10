using ERP_TECKIO.Modelos.Presupuesto;

namespace ERP_TECKIO.DTO
{
    public class ParametrosFsrDTO : ParametrosFsrAbstract
    {
    }

    public class ParametrosFsrXInsumoDTO : ParametrosFsrDTO
    {
        public int IdInsumo { get; set; }
        public string Descripcion { get; set; }
        public decimal CostoBase { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal Importe { get; set; }
        public decimal CesantiaEdad { get; set; }
        public decimal SalarioBaseCotizacion { get; set; }
        public decimal SumaPrestaciones { get; set; }
        public decimal SumaSOBRECostoBase { get; set; }
        public decimal FsrInsumo { get; set; }
        public string? CostoBaseConFormato { get; set; }
        public string? CostoUnitarioConFormato { get; set; }
        public string? SBCFormato { get; set; }
        public string? RiesgoTrabajoConFormato { get; set; }
        public string? CuotaFijaFormato { get; set; }
        public string? AplicacionExcedenteConFormato { get; set; }
        public string? PrestacionDineroConFormato { get; set; }
        public string? GastoMedicoConFormato { get; set; }
        public string? InvalidezVidaConFormato { get; set; }
        public string? RetiroConFormato { get; set; }
        public string? CesantiaConFormato { get; set; }
        public string? PrestacionSocialConFormato { get; set; }
        public string? InfonavitConFormato { get; set; }
        public string? SumaDePrestacionesConFormato { get; set; }
        public string? SPSBCConFormato { get; set; }
        public string? FSRConFormato { get; set; }
    }
}
