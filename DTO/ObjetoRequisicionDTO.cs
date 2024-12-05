
namespace ERP_TECKIO
{
    public class ObjetoRequisicionDTO
    {
        public int IdRequisicion { get; set; }
        public List<InsumosXRequisicionObjetoRequisicionDTO> InsumosXRequisicion { get; set; }
        public List<CotizacionObjetoRequisicionDTO> Cotizacion { get; set; }
    }

    public abstract class DescripcionCotizacion
    {
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal Importe { get; set; }
    }

    public abstract class DescripcionInsumo : DescripcionCotizacion
    {
        public int IdInsumo { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
    }

    public abstract class ContratistaObjetoRequisicionDTO
    {
        public int? IdContratista { get; set; }
        public string RazonSocial { get; set; }
    }

    public class InsumosXRequisicionObjetoRequisicionDTO : DescripcionInsumo
    {
        public int IdInsumoXRequisicion { set; get; }

    }

    public class CotizacionObjetoRequisicionDTO : ContratistaObjetoRequisicionDTO
    {
        public int IdCotizacion { get; set; }
        public string NoCotizacion { get; set; }
        public decimal ImporteTotal { get; set; }
        public List<InsumosXCotizacionObjetoRequisicionDTO> InsumoXCotizacion { set; get; }

    }

    public class InsumosXCotizacionObjetoRequisicionDTO : DescripcionInsumo
    {
        public int IdInsumoXCotizacion { get; set; }
        public int Estatus { get; set; }
        public List<ImpuestoInsumoCotizadoDTO> ImpuestoInsumoCotizado { set; get; }

    }
}
