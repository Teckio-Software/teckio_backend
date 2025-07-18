using ERP_TECKIO.Modelos.Presupuesto;

namespace ERP_TECKIO.DTO
{
    public class CostoHorarioVariableXPrecioUnitarioDetalleDTO : CostoHorarioVariableXPrecioUnitarioDetalleAbstract
    {
        public int IdPrecioUnitario { get; set; }
        public int IdInsumo { get; set; }
        public bool EsCompuesto { get; set; }
        public decimal CostoUnitario { get; set; } //Viene de Insumo
        public string? CostoUnitarioConFormato { get; set; } //Se genera
        public bool CostoUnitarioEditado { get; set; } //Se genera
        public decimal CostoBase { get; set; }
        public string? CostoBaseConFormato { get; set; }
        public decimal Cantidad { get; set; }
        public string? CantidadConFormato { get; set; } //Se genera
        public bool CantidadEditado { get; set; } //Se genera
        public decimal CantidadExcedente { get; set; } //Se genera
        public int IdPrecioUnitarioDetallePerteneciente { get; set; }
        public string? Codigo { get; set; } //Insumo
        public string? Descripcion { get; set; } //Insumo
        public string? Unidad { get; set; } //Insumo
        public int IdTipoInsumo { get; set; } //Insumo
        public int? IdFamiliaInsumo { get; set; } //Insumo
        public decimal Importe { get; set; }
        public string? ImporteConFormato { get; set; } //Se genera
    }
}
