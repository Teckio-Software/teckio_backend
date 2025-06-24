namespace ERP_TECKIO.DTO
{
    public class OrdenVentaDTO : OrdenVentaAbstract
    {
        public List<DetalleOrdenVentaDTO> listaDetalles { get; set; } = new List<DetalleOrdenVentaDTO>();

    }
}
