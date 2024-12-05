
namespace ERP_TECKIO.Modelos
{
    public abstract class PolizaAbstract
    {
        public int Id { get; set; }
        public int IdTipoPoliza { get; set; }
    }
    public class Poliza : PolizaAbstract
    {
        public string? Folio { get; set; }
        public string? NumeroPoliza { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaPoliza { get; set; }
        public string? Concepto { get; set; }
        public int Estatus { get; set; }
        public string? Observaciones { get; set; }
        public int OrigenDePoliza { get; set; }
        public bool EsPolizaCierre { get; set; }
        public virtual ICollection<PolizaDetalle> PolizaDetalles { get; set; } = new List<PolizaDetalle>();
        public virtual ICollection<MovimientoBancario> MovimientosBancarios { get; set; } = new List<MovimientoBancario>();
        public virtual TipoPoliza IdTipoPolizaNavigation { get; set; } = null!;
    }
    public class PolizaProveedores : PolizaAbstract
    {
        public bool Estatus { get; set; }

        public int IdFactura { get; set; }

        public virtual Factura IdFacturaNavigation { get; set; } = null!;

        public virtual TipoPolizaProveedores IdTipoPolizaNavigation { get; set; } = null!;

    }
}
