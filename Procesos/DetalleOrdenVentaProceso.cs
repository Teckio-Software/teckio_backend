using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class DetalleOrdenVentaProceso<T> where T : DbContext
    {
        private readonly IDetalleOrdenVentaService<T> _service;
        private readonly IOrdenVentaService<T> _ordenVentaService;
        public DetalleOrdenVentaProceso(IDetalleOrdenVentaService<T> service, IOrdenVentaService<T> ordenVentaService)
        {
            _service = service;
            _ordenVentaService = ordenVentaService;
        }

        public async Task<List<DetalleOrdenVentaDTO>> ObtenerXOrdenVenta(int id)
        {
            try
            {
                var lista = await _service.ObtenerXIdOrdenVenta(id);
                return lista;
            }
            catch
            {
                return new List<DetalleOrdenVentaDTO>();
            }
        }

        public async Task<RespuestaDTO> EditarDetalle(DetalleOrdenVentaDTO detalle)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                //Validación con respecto a la orden de venta.
                var ordenVenta = await _ordenVentaService.ObtenerOrdenVentaXId(detalle.IdOrdenVenta);
                if (ordenVenta.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontro la orden de venta";
                    return respuesta;
                }

                if (ordenVenta.Id != 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pueden editar detalles de una orden de venta si esta cancelada, autorizada o pagada";
                    return respuesta;
                }

                respuesta = await _service.Editar(detalle);
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar editar la descripción";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> EliminarDetalle(DetalleOrdenVentaDTO detalle)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                //Validación con respecto a la orden de venta.
                var ordenVenta = await _ordenVentaService.ObtenerOrdenVentaXId(detalle.IdOrdenVenta);
                if (ordenVenta.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontro la orden de venta";
                    return respuesta;
                }

                if (ordenVenta.Id != 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pueden agregar detalles a una orden de venta si esta cancelada, autorizada o pagada";
                    return respuesta;
                }

                respuesta = await _service.Eliminar(detalle);
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar eliminar la descripción";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Crear(DetalleOrdenVentaDTO detalle)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                //Validación con respecto a la orden de venta.
                var ordenVenta = await _ordenVentaService.ObtenerOrdenVentaXId(detalle.IdOrdenVenta);
                if (ordenVenta.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontro la orden de venta";
                    return respuesta;
                }

                if (ordenVenta.Id != 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pueden agregar detalles a una orden de venta si esta cancelada, autorizada o pagada";
                    return respuesta;
                }

                respuesta = await _service.Crear(detalle);
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar crear la descripción";
                return respuesta;
            }
        }
    }
}
