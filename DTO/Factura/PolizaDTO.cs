
namespace ERP_TECKIO
{
    public abstract class PolizaAbstractDTO
    {
        public int Id { get; set; }
        public int IdTipoPoliza { get; set; }
    }
    public class PolizaDTO : PolizaAbstractDTO
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
        public List<PolizaDetalleDTO> Detalles { get; set; }
    }

    public class PolizaProveedoresDTO : PolizaAbstractDTO
    {
        public bool Estatus { get; set; }

        public int IdFactura { get; set; }
    }

        public class PolizaFolioCodigoDTO
    {
        public string? folio { get; set; }
        public string? numeroPoliza { get; set; }
    }

    public class PolizaFiltroEspecificoDTO
    {
        public int IdEmpresa { get; set; }
        public int IdTipoPoliza { get; set; }
        public int mes { get; set; }
        public int anio { get; set; }
    }

    public class PolizaFiltroIntervaloDTO
    {
        public int IdEmpresa { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
    }
}
