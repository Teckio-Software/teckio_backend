namespace ERP_TECKIO
{
    public class CompraDirectaDTO
    {
        public int Id { get; set; }
        public int IdProyecto { get; set; }
        public int IdContratista { get; set; }
        public string? RazonSocialContratista { get; set; }
        public int IdRequisicion { get; set; }
        public string? NoCompraDirecta { get; set; }
        public string? Elaboro { get; set; }
        public int Estatus { get; set; }
        public int EstatusAlmacen { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string? Chofer { get; set; }
        public string? Observaciones { get; set; }
        public decimal ImporteSinIva { get; set; }
        public decimal MontoDescuento { get; set; }
        public decimal ImporteConIva { get; set; }
    }

    public class CompraDirectaCreacionDTO
    {
        public int IdProyecto { get; set; }
        public int IdContratista { get; set; }
        public int IdRequisicion { get; set; }
        public string? NoCompraDirecta { get; set; }
        public string? Elaboro { get; set; }
        public int Estatus { get; set; }
        public int EstatusAlmacen { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string? Chofer { get; set; }
        public string? Observaciones { get; set; }
        public decimal ImporteSinIva { get; set; }
        public decimal MontoDescuento { get; set; }
        public decimal ImporteConIva { get; set; }
        public List<InsumoXCompraDirectaCreacionDTO> ListaInsumos { get; set; }
    }
}
