using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Procesos
{
    public class ImpuestoDetalleOrdenVentaProceso<T> where T : DbContext
    {
        private readonly IImpuestoDetalleOrdenVentaService<T> _service;

        public ImpuestoDetalleOrdenVentaProceso(IImpuestoDetalleOrdenVentaService<T> service)
        {
            _service = service;
        }

        public async Task<List<ImpuestoDetalleOrdenVentaDTO>> ObtenerXIdDetalle(int id)
        {
            try
            {
                var lista = await _service.ObtenerXIdDetalle(id);
                if (lista.Count > 0)
                {
                    return lista;
                }
                else
                {
                    return new List<ImpuestoDetalleOrdenVentaDTO>();
                }
            }
            catch
            {
                return new List<ImpuestoDetalleOrdenVentaDTO>();
            }
        }

        public async Task<RespuestaDTO> Editar(ImpuestoDetalleOrdenVentaDTO impuesto)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                respuesta = await _service.Editar(impuesto);
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar editar el impuesto";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(ImpuestoDetalleOrdenVentaDTO impuesto)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                respuesta = await _service.Eliminar(impuesto);
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar eliminar el impuesto";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Crear(ImpuestoDetalleOrdenVentaDTO impuesto)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                respuesta = await _service.Crear(impuesto);
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar crear el impuesto";
                return respuesta;
            }
        }
    }
}
