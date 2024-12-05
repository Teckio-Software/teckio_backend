namespace ERP_TECKIO
{
    public class OrdenCompraDTO
    {
        public int Id { get; set; }
        public int IdRequisicion { get; set; }
        public string? NoRequisicion { get; set; }
        public int IdCotizacion { get; set; }
        public string? NoCotizacion { get; set; }
        /// <summary>
        /// Se obtiene en el mapeo de su cotización
        /// </summary>
        public decimal MontoTotal { get; set; }
        public int? IdContratista { get; set; }
        public string? RazonSocial { get; set; }
        public int IdProyecto { get; set; }
        /// <summary>
        /// Nombre del proyecto
        /// </summary>
        public string? Nombre { get; set; }
        public string? NoOrdenCompra { get; set; }
        public string? Elaboro { get; set; }
        /// <summary>
        /// 1 = Capturada, 2 = Facturada, 3 = Cancelada
        /// </summary>
        public int Estatus { get; set; }
        /// <summary>
        /// 0 = Sin aplicar, 1 = Surtido parcial, 2 = Surtido total, 3 = surtido parcial con excedentes, 4 = surtido total con excedentes
        /// </summary>
        public int EstatusAlmacen { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string? Chofer { get; set; }
        public string? Observaciones { get; set; }

        public int EstatusInsumosSurtidos { get; set; }
        public string EstatusInsumosSurtidosDescripcion {  get; set; }
    }

    public class OrdenCompraCreacionDTO
    {
        public int IdRequisicion { get; set; }
        public int IdCotizacion { get; set; }
        public int IdContratista { get; set; }
        public int IdProyecto { get; set; }
        public string? Chofer { get; set; }
        public string? Observaciones { get; set; }
        public List<InsumoXOrdenCompraCreacionDTO> ListaInsumosOrdenCompra { get; set; }
    }
}
