
namespace ERP_TECKIO
{
    public class InsumoXOrdenCompraDTO
    {
        public int Id { get; set; }
        public int IdOrdenCompra { get; set; }
        public int IdInsumoXCotizacion { get; set; }
        public int IdInsumo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public decimal Cantidad { get; set; }
        public string CantidadConFormato { get; set; }
        public string PrecioUnitarioConFormato { get; set; }
        public string ImporteSinIvaConFormato { get; set; }
        public decimal CantidadRecibida { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal ImporteSinIva { get; set; }
        public decimal ImporteConIva { get; set; }
        public int EstatusInsumoOrdenCompra { get; set; }
        public string EstatusInsumoOrdenCompraDescripcion { get; set; }

        public string NoOrdenCompra { get; set; }
        //public decimal InsumosSurtIdos { get; set; }
    }

    public class InsumoXOrdenCompraCreacionDTO
    {
        public int IdOrdenCompra { get; set; }
        public int IdInsumoXCotizacion { get; set; }
        public int IdInsumo { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal ImporteSinIva { get; set; }
        public decimal ImporteConIva { get; set; }
    }
}
