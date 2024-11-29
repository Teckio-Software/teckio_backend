namespace ERP_TECKIO
{
    /// <summary>
    /// Esta clase se usa para la relación de las compras, ya sea ordenes o compras directas
    /// </summary>
    public class ComprasContratistasDTO
    {
        public int IdContratista { get; set; }
        public string RazonSocialContratista { get; set; }
        public int IdCompraDirecta { get; set; }
        public int IdOrdenCompra { get; set; }
        public bool EsCompraDirecta { get; set; }
        public bool EsOrdenDeCompra { get; set; }
    }
}
