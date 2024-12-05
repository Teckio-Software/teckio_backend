namespace ERP_TECKIO
{
    public class ComprasDTO
    {
        public int IdCompra { get; set; }
        public int IdContratista { get; set; }
        public int IdRequisicion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public bool EsCompraDirecta { get; set; }
        public bool EsOrdenCompra { get; set; }
        public List<ComprasInsumosDTO> CompraInsumos { get; set; }
    }

    public class ComprasInsumosDTO
    {
        public int IdInsumo { get; set; }
        public string CodigoInsumo { get; set; }
        public string DescripcionInsumo { get; set; }
        public string UnidadInsumo { get; set; }
        public decimal Cantidad { get; set; }
    }
    /// <summary>
    /// Este DTO es para recuperar la información de los 
    /// </summary>
    public class InsumoCompraParaAlmacenDTO
    {
        public int IdContratista { get; set; }
        public string RazonSocialContratista { get; set; }
        public int IdCompra { get; set; }
        public int EstatusCompra { get; set; }
        public int IdRequisicion { get; set; }
        public int IdInsumoComprado { get; set; }
        public int IdInsumo { get; set; }
        public string CodigoInsumo { get; set; }
        public string DescripcionInsumo { get; set; }
        public string UnidadInsumo { get; set; }
        public decimal Cantidad { get; set; }
        public string NoRequisicion { get; set; }
        public decimal CantidadRecibIda { get; set; }
        /// <summary>
        /// Se usa para ver el típo de compra <c>Compra directa (1) u orden de compra (2)</c>
        /// </summary>
        public int TipoCompra { get; set; }
        public int IdProyecto { get; set; }
        public decimal CantidadInsumoRequisicion { get; set; }
        public decimal CantidadInsumoCompradoRequisicion { get; set; }
        public decimal CantidadInsumoRecibIdoRequisicion { get; set; }
    }
    /// <summary>
    /// Este DTO es para recuperar la información de las ordenes de compra 
    /// </summary>
    public class InsumoOrdenCompraDTO
    {
        public int IdContratista { get; set; }
        public string RazonSocialContratista { get; set; }
        public int IdCompra { get; set; }
        public int EstatusCompra { get; set; }
        public int IdRequisicion { get; set; }
        public int IdInsumoComprado { get; set; }
        public int IdInsumo { get; set; }
        public string CodigoInsumo { get; set; }
        public string DescripcionInsumo { get; set; }
        public string UnidadInsumo { get; set; }
        public decimal Cantidad { get; set; }
        public string NoRequisicion { get; set; }
        public decimal CantidadRecibIda { get; set; }
        /// <summary>
        /// Se usa para ver el típo de compra <c>Compra directa (1) u orden de compra (2)</c>
        /// </summary>
        public int TipoCompra { get; set; }
        public int IdProyecto { get; set; }
    }


    public class CompraDTO
    {
        public int Id { get; set; }
        public int IdProyecto { get; set; }
        public int IdContratista { get; set; }
        public int IdRequisicion { get; set; }
        public int IdCotizacion { get; set; }
        public bool EsCompraDirecta { get; set; }
        public string NoCompra { get; set; }
        public string Elaboro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Chofer { get; set; }
        public string Observaciones { get; set; }
        public decimal ImporteSinIva { get; set; }
        public decimal MontoDescuento { get; set; }
        public decimal ImporteConIva { get; set; }
    }
    public class CompraCreacionDTO
    {
        public int IdProyecto { get; set; }
        public int IdContratista { get; set; }
        public int IdRequisicion { get; set; }
        public int IdCotizacion { get; set; }
        public bool EsCompraDirecta { get; set; }
        public string NoCompra { get; set; }
        public string Elaboro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Chofer { get; set; }
        public string Observaciones { get; set; }
        public decimal ImporteSinIva { get; set; }
        public decimal MontoDescuento { get; set; }
        public decimal ImporteConIva { get; set; }
    }

    public class InsumoXCompraDTO
    {
        public int Id { get; set; }
        public int IdCompra { get; set; }
        public int IdInsumoXRequisicion { get; set; }
        public int IdInsumoXCotizacion { get; set; }
        public int IdInsumo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario  { get; set; }
        public decimal ImporteSinIva { get; set; }
        public decimal Iva { get; set; }
        public decimal Isr { get; set; }
        public decimal Ieps { get; set; }
        public decimal Isan { get; set; }
        public decimal ImporteConIva { get; set; }
    }

    public class InsumoXCompraCreacionDTO
    {
        public int IdCompra { get; set; }
        public int IdInsumoXRequisicion { get; set; }
        public int IdInsumoXCotizacion { get; set; }
        public int IdInsumo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal ImporteSinIva { get; set; }
        public decimal Iva { get; set; }
        public decimal Isr { get; set; }
        public decimal Ieps { get; set; }
        public decimal Isan { get; set; }
        public decimal ImporteConIva { get; set; }
    }
}
