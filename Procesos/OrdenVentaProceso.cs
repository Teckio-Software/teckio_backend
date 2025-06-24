using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class OrdenVentaProceso<T> where T : DbContext
    {
        private readonly IOrdenVentaService<T> _ordenVentaService;
        private readonly IDetalleOrdenVentaService<T> _detalleOrdenVentaService;
        private readonly IImpuestoDetalleOrdenVentaService<T> _impuestoDetalleOrdenVentaService;
        public OrdenVentaProceso(
            IOrdenVentaService<T> ordenVentaService,
            IDetalleOrdenVentaService<T> detalleOrdenVentaService,
            IImpuestoDetalleOrdenVentaService<T> impuestoDetalleOrdenVentaService
            ) { 
            _ordenVentaService = ordenVentaService;
            _detalleOrdenVentaService = detalleOrdenVentaService;
            _impuestoDetalleOrdenVentaService = impuestoDetalleOrdenVentaService;
        }
    }
}
