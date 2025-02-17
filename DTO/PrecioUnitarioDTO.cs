
namespace ERP_TECKIO

{
    public abstract class PrecioUnitarioAbstractaDTO
    {
        public int Id { get; set; }
        public int IdProyecto { get; set; }
        public decimal Cantidad { get; set; }
        public string CantidadConFormato { get; set; }
        public bool CantidadEditado { get; set; }
        public decimal CantidadExcedente { get; set; }
        public string CantidadExcedenteConFormato { get; set; }
        public int TipoPrecioUnitario { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal PorcentajeIndirecto { get; set; }
        public string? PorcentajeIndirectoConFormato { get; set; }
        public string? CostoUnitarioConFormato { get; set; }
        public bool CostoUnitarioEditado { get; set; }
        public int Nivel { get; set; }
        public int NoSerie { get; set; }
        public int IdPrecioUnitarioBase { get; set; }
        public bool EsDetalle { get; set; }
        public int IdConcepto { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public string? Unidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string PrecioUnitarioConFormato { get; set; }
        public bool PrecioUnitarioEditado { get; set; }
        public decimal Importe { get; set; }
        public string ImporteConFormato { get; set; }
        public decimal ImporteSeries { get; set; }
        public string ImporteSeriesConFormato { get; set; }
        public bool Expandido { get; set; }
    }

    public class PrecioUnitarioDTO : PrecioUnitarioAbstractaDTO
    {
        public List<PrecioUnitarioDTO>? Hijos { get; set; }
    }

    public class PrecioUnitarioCopiaDTO : PrecioUnitarioAbstractaDTO
    {
        public List<PrecioUnitarioCopiaDTO>? Hijos { get; set; }
        public bool Seleccionado { get; set; }
    }

    public class DatosParaCopiarDTO
    {
        public List<PrecioUnitarioCopiaDTO>? Registros { get; set; }
        public int IdPrecioUnitarioBase { get; set; }
        public int IdProyecto { get; set; }
    }

    public class DatosParaCopiarArmadoDTO
    {
        public List<PrecioUnitarioDetalleCopiaDTO>? Registros { get; set; }
        public int IdPrecioUnitarioBase { get; set; }
        public int IdProyecto { get; set; }
    }

    public class DatosAuxiliaresPrecioUnitarioDTO
    {
        public decimal Total { get; set; }
        public List<InsumoDTO> Insumos { get; set; }
        public List<PrecioUnitarioDetalleDTO> Detalles { get; set; }
    }
}
