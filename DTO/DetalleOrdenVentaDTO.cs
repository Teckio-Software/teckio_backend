using ERP_TECKIO.Modelos;

namespace ERP_TECKIO.DTO
{
    public class DetalleOrdenVentaDTO : DetalleOrdenVentaAbstract
    {
        public List<ImpuestoDetalleOrdenVentaDTO> ImpuestosDetalleOrdenVenta { get; set; } = new List<ImpuestoDetalleOrdenVentaDTO>();
    }
}
