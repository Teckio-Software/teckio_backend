

namespace ERP_TECKIO
{
    public class CotizacionDTO
    {
        public int Id { get; set; }
        public int IdProyecto { get; set; }
        public int IdRequisicion { get; set; }
        public int? IdContratista { get; set; }
        public string RepresentanteLegal { get; set; }
        public string NoCotizacion { get; set; }
        public string? Observaciones { get; set; }
        public DateTime FechaRegistro { get; set; }
        /// <summary>
        /// Correo de la persona que autoriza la cotización
        /// </summary>
        public string? PersonaAutorizo { get; set; }
        /// <summary>
        /// Correo de la persona que compra la cotización
        /// </summary>
        public string? PersonaCompra { get; set; }
        /// <summary>
        /// 1 = capturada, 2 = autorizada, 3 = comprada, 4 = cancelada
        /// </summary>
        public int EstatusCotizacion { get; set; }
        public string EstatusCotizacionDescripcion { get; set; }

        /// <summary>
        /// 1 = capturada, 2 = autorizada, 3 = comprada, 4 = cancelada
        /// </summary>
        public int EstatusInsumosComprados { get; set; }
        public string EstatusInsumosCompradosDescripcion { get; set; }
    }

    public class CotizacionCreacionDTO
    {
        public int IdProyecto { get; set; }
        public int IdRequisicion { get; set; }
        public int IdContratista { get; set; }
        public string? Observaciones { get; set; }
        public List<InsumoXCotizacionCreacionDTO> ListaInsumosCotizacion { get; set; }
    }

    public class CotizacionEditaEstatusDTO
    {
        public int Id { get; set; }
        public int Estatus { get; set; }
    }
}
