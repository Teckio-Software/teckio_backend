using ERP_TECKIO.Modelos;

namespace ERP_TECKIO
{
    public class ContratistaDTO
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string Rfc { get; set; }
        public bool EsProveedorServicio { get; set; }
        public bool EsProveedorMaterial { get; set; }
        public string RepresentanteLegal { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Domicilio { get; set; }
        public string? NExterior { get; set; }
        public string? Colonia { get; set; }
        public string? Municipio { get; set; }
        public string CodigoPostal { get; set; }
        public int? IdCuentaContable { get; set; }
        public int? IdIvaAcreditableContable { get; set; }
        public int? IdIvaPorAcreditar { get; set; }
        public int? IdCuentaAnticipos { get; set; }
        public int? IdCuentaRetencionISR { get; set; }
        public int? IdCuentaRetencionIVA { get; set; }
        public int? IdEgresosIvaExento { get; set; }
        public int? IdEgresosIvaGravable { get; set; }
        public int? IdIvaAcreditableFiscal { get; set; }
    }
}
