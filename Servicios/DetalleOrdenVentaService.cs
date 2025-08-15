using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class DetalleOrdenVentaService<T> : IDetalleOrdenVentaService<T> where T : DbContext
    {
        private readonly IGenericRepository<DetalleOrdenVentum, T> _repository;
        private readonly IMapper _mapper;

        public DetalleOrdenVentaService(
            IGenericRepository<DetalleOrdenVentum, T> repository,
            IMapper mapper
            ) {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<RespuestaDTO> Crear(DetalleOrdenVentaDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = _mapper.Map<DetalleOrdenVentum>(modelo);
                var objetoCreado = await _repository.Crear(objeto);
                if (objetoCreado.Id > 0)
                {
                    respuesta.Descripcion = "Detalle de orden de venta creado exitosamente";
                    respuesta.Estatus = true;
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al intentar crear el detalle de orden de venta";
                    respuesta.Estatus = false;
                }
                return respuesta;
            }
            catch
            {
                respuesta.Descripcion = "Ocurrio un error al intentar crear el detalle de venta";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<DetalleOrdenVentaDTO> CrearYObtener(DetalleOrdenVentaDTO modelo)
        {
            var respuesta = await _repository.Crear(_mapper.Map<DetalleOrdenVentum>(modelo));
            return _mapper.Map<DetalleOrdenVentaDTO>(respuesta);
        }

        public async Task<RespuestaDTO> Editar(DetalleOrdenVentaDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(d => d.Id == modelo.Id);
                objeto.IdOrdenVenta = modelo.IdOrdenVenta;
                objeto.IdProductoYservicio = modelo.IdProductoYservicio;
                objeto.IdEstimacion = modelo.IdEstimacion;
                objeto.Cantitdad = modelo.Cantitdad;
                objeto.PrecioUnitario = modelo.PrecioUnitario;
                objeto.Descuento = modelo.Descuento;
                objeto.ImporteTotal = modelo.ImporteTotal;
                respuesta.Estatus = await _repository.Editar(objeto);
                if (respuesta.Estatus)
                {
                    respuesta.Descripcion = "Detalle de orden de venta editado exitosamente";
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al intentar editar el detalle de venta";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Descripcion = "Ocurrio un error al intentar editar el detalle de venta";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(DetalleOrdenVentaDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(d => d.Id == modelo.Id);
                respuesta.Estatus = await _repository.Eliminar(objeto);
                if (respuesta.Estatus)
                {
                    respuesta.Descripcion = "Detalle de orden de venta eliminada exitosamente";
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al intentar elminar el detalle de venta";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Descripcion = "Ocurrio un error al intentar eliminar el detalle de venta";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<List<DetalleOrdenVentaDTO>> ObtenerXIdOrdenVenta(int id)
        {
            try
            {
                var lista = await _repository.ObtenerTodos(d => d.IdOrdenVenta == id);
                if (lista.Count > 0)
                {
                    return _mapper.Map<List<DetalleOrdenVentaDTO>>(lista);
                }
                else
                {
                    return new List<DetalleOrdenVentaDTO>();
                }
            }
            catch
            {
                return new List<DetalleOrdenVentaDTO>();
            }
        }
    }
}
