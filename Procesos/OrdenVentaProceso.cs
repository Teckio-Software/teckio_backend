using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.AspNetCore.Mvc;
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

            //OV (Probablemente el nombre de la empresa) + El Mes + EL número de factura.
            ordenVenta.NumeroOrdenVenta = "OV_"+(DateTime.Now.ToString("MM"))+"_"+(ordenesVenta.Count()+1).ToString();
            ordenVenta.Elaboro = usuarioNombre[0].Value;
            ordenVenta.FechaRegistro = DateTime.Now;
            ordenVenta.Estatus = 1;
            ordenVenta.EstatusSaldado = 1;
            ordenVenta.TotalSaldado = 0;
            ordenVenta.Observaciones = "";
            ordenVenta.Autorizo = "";

            decimal subtotal = 0;
            decimal descuento = 0;
            decimal totalTraslados = 0;
            decimal totalRetenciones = 0;
            foreach (var detalle in ordenVenta.DetalleOrdenVenta) {
                var Base = (detalle.Cantitdad * detalle.PrecioUnitario) - detalle.Descuento;
                subtotal += Base;
                descuento += detalle.Descuento;
                detalle.IdEstimacion = null;
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

        public async Task<RespuestaDTO> EditarOrdenVenta(OrdenVentaDTO ordenVenta)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                respuesta = await _ordenVentaService.Editar(ordenVenta);
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar editar la orden de venta";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> EliminarOrdenVenta(OrdenVentaDTO ordenVenta)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                respuesta = await _ordenVentaService.Eliminar(ordenVenta);
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar editar la orden de venta";
                return respuesta;
            }
        }

        public async Task<List<OrdenVentaDTO>> ObtenerTodos()
        {
            try
            {
                var lista = await _ordenVentaService.ObtenerTodos();
                if (lista.Count > 0)
                {
                    
                    return lista;
                }
                else
                {
                    return new List<OrdenVentaDTO>();
                }
            }
            catch
            {
                return new List<OrdenVentaDTO>();
            }
        }

        public async Task<RespuestaDTO> Autorizar(OrdenVentaDTO orden, List<System.Security.Claims.Claim> claims)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var usuarioNombre = claims.Where(z => z.Type == "username").ToList();
                orden.Autorizo = usuarioNombre[0].Value;
                orden.Estatus = 1;
                respuesta = await _ordenVentaService.Editar(orden);
                return respuesta;
            }
            catch
            {
                respuesta.Descripcion = "Ocurrio un error al intentar autorizar la orden de venta";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Cancelar(OrdenVentaDTO orden)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                orden.Estatus = 2;
                respuesta = await _ordenVentaService.Editar(orden);
                return respuesta;
            }
            catch
            {
                respuesta.Descripcion = "Ocurrio un error al intentar autorizar la orden de venta";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

    }
}
