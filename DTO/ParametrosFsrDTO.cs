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
    }
}
