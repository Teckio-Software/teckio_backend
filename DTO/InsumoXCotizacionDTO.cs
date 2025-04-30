

namespace ERP_TECKIO
{
    public class InsumoXCotizacionDTO
    {
        public int Id { get; set; }
        public int IdCotizacion { get; set; }
        public int IdInsumoRequisicion { get; set; }
        public int IdInsumo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal ImporteSinIva { get; set; }
        public decimal ImporteTotal { get; set; }
        /// <summary>
        /// 1 = capturada, 2 = autorizada, 3 = comprada, 4 = cancelada
        /// </summary>
        public int EstatusInsumoCotizacion { get; set; }

        public decimal Descuento {  get; set; }
    }

    public class ListaInsumoXCotizacionDTO
    {
        public int Id { get; set; }
        public int IdCotizacion { get; set; }
        public string NoCotizacion { get; set; }
        public int IdInsumoRequisicion { get; set; }
        public int IdInsumo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string UnidadCotizada { get; set; }
        public decimal CantidadCotizada { get; set; }
        public string Unidad { get; set; }
        public decimal Cantidad { get; set; }
        public string CantidadConFormato { get; set; }
        public string DescuentoConFormato { get; set; }
        public string PrecioUnitarioConFormato { get; set; }
        public string ImporteTotalConFormato { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal ImporteSinIva { get; set; }
        public decimal ImporteTotal { get; set; }
        /// <summary>
        /// 1 = capturada, 2 = autorizada, 3 = comprada, 4 = cancelada
        /// </summary>
        public int EstatusInsumoCotizacion { get; set; }
        public string EstatusInsumoCotizacionDescripcion { get; set; }

        public decimal Descuento { get; set; }

        public bool esSeleccionada { get; set; }
    }

    public class InsumoXCotizacionCreacionDTO
    {
        public int IdCotizacion { get; set; }
        public int IdInsumoRequisicion { get; set; }
        public int IdInsumo { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int TipoImpuesto { get; set; }
        public decimal Descuento { get; set; }

        public decimal PIVA {  get; set; }
    }

    public class InsumoXCotizacionEditaEstatusDTO
    {
        public int Id { get; set; }
        public int Estatus { get; set; }
    }
}
