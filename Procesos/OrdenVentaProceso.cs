using ERP_TECKIO.DTO;
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

        public async Task<RespuestaDTO> CrearOrdenVenta(OrdenVentaDTO ordenVenta, List<System.Security.Claims.Claim> claims) { 
            var respuesta = new RespuestaDTO();

            var usuarioNombre = claims.Where(z => z.Type == "username").ToList();
            var ordenesVenta = await _ordenVentaService.ObtenerTodos();

            ordenVenta.NumeroOrdenVenta = "OV_"+(ordenesVenta.Count()+1).ToString();
            ordenVenta.Autorizo = usuarioNombre[0].Value;
            ordenVenta.FechaRegistro = DateTime.Now;
            ordenVenta.Estatus = 1;
            ordenVenta.EstatusSaldado = 1;
            ordenVenta.TotalSaldado = 0;
            ordenVenta.Observaciones = "";

            decimal subtotal = 0;
            decimal descuento = 0;
            decimal totalTraslados = 0;
            decimal totalRetenciones = 0;
            foreach (var detalle in ordenVenta.DetalleOrdenVenta) {
                var Base = (detalle.Cantitdad * detalle.PrecioUnitario) - detalle.Descuento;
                subtotal += Base;
                descuento += detalle.Descuento;
                foreach (var impuesto in detalle.ImpuestosDetalleOrdenVenta) {
                    impuesto.ImporteTotal = impuesto.TasaCuota * Base;
                    if (impuesto.IdCategoriaImpuesto == 1) {
                        totalTraslados += impuesto.ImporteTotal;
                    }
                    if (impuesto.IdCategoriaImpuesto == 2)
                    {
                        totalRetenciones += impuesto.ImporteTotal;
                    }
                }
            }
            ordenVenta.Descuento = descuento;
            ordenVenta.Subtotal = subtotal;
            ordenVenta.ImporteTotal = subtotal + totalTraslados - totalRetenciones;

            var nuevaOrdenVenta = await _ordenVentaService.CrearYObtener(ordenVenta);
            if (nuevaOrdenVenta.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se genero la orden de venta";
                return respuesta;
            }

            foreach (var detalle in ordenVenta.DetalleOrdenVenta)
            {
                detalle.IdOrdenVenta = nuevaOrdenVenta.Id;
                detalle.ImporteTotal = detalle.Cantitdad * detalle.PrecioUnitario;
                var nuevoDetalle = await _detalleOrdenVentaService.CrearYObtener(detalle);
                if (nuevoDetalle.Id <= 0) {
                    continue;
                }

                foreach (var impuesto in detalle.ImpuestosDetalleOrdenVenta)
                {
                    var crearImpuesto = await _impuestoDetalleOrdenVentaService.Crear(impuesto);
                }
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se genero la orden de venta";
            return respuesta;
        }
    }
}
