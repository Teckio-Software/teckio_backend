
namespace ERP_TECKIO
{
    public class DetalleXContratoDTO
    {
        public int Id { get; set; }
        public int IdPrecioUnitario { get; set; }
        public int IdContrato { get; set; }
        public decimal PorcentajeDestajo { get; set; }
        public decimal ImporteDestajo { get; set; }
        public decimal FactorDestajo { get; set; }
    }

    public class DetalleXContratoParaTablaDTO
    {
        public int IdDetalleXContrato { get; set; }
        public int IdPrecioUnitario { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public string CostoUnitarioConFormato { get; set; }
        public decimal Cantidad { get; set; }
        public string CantidadConFormato { get; set; }
        public decimal Importe { get; set; }
        public string ImporteConFormato { get; set; }
        public int TipoPrecioUnitario { get; set; }
        public int IdPrecioUnitarioBase { get; set; }
        public int IdContrato { get; set; }
        public decimal PorcentajeDestajo { get; set; }
        public string PorcentajeDestajoConFormato { get; set; }
        public decimal FactorDestajo { get; set; }
        public string FactorDestajoConFormato { get; set; }
        public bool PorcentajeDestajoEditado { get; set; }
        public decimal ImporteDestajo { get; set; }
        public string ImporteDestajoConFormato { get; set; }
        public decimal PorcentajeDestajoAcumulado { get; set; }
        public string PorcentajeDestajoAcumuladoConFormato { get; set; }
        public bool Expandido { get; set; }
        public List<DetalleXContratoParaTablaDTO> Hijos { get; set; }
    }

    public class DetalleXContratoAcumuladoDTO : DetalleXContratoParaTablaDTO
    {
        public List<PeriodosResumenDTO> Periodos {  get; set; }
    }

    public class DestajistasXConceptoDTO
    {
        public int IdPrecioUnitario { get; set; }
        public int IdContratista { get; set; }
        public string RazonSocial { get; set; }
        public int IdContrato { get; set; }
        public string NumeroDestajo { get; set; }
        public int IdDetalleXContrato { get; set; }
        public decimal PorcentajeDestajo { get; set; }
        public string PorcentajeDestajoConFormato { get; set; }
    }
}