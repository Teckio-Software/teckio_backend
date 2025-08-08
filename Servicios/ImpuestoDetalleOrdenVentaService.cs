using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class ImpuestoDetalleOrdenVentaService<T> : IImpuestoDetalleOrdenVentaService<T> where T : DbContext
    {
        private readonly IGenericRepository<ImpuestoDetalleOrdenVentum, T> _repository;
        private readonly IMapper _mapper;

        public ImpuestoDetalleOrdenVentaService(
            IGenericRepository<ImpuestoDetalleOrdenVentum, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<RespuestaDTO> Crear(ImpuestoDetalleOrdenVentaDTO modelo)
        {
            var objeto = await _repository.Crear(_mapper.Map<ImpuestoDetalleOrdenVentum>(modelo));
            var respuesta = new RespuestaDTO();
            if (objeto.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creó el registro";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se creó el registro";
            return respuesta;
        }

        public Task<OrdenVentaDTO> CrearYObtener(ImpuestoDetalleOrdenVentaDTO modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<RespuestaDTO> Editar(ImpuestoDetalleOrdenVentaDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _repository.Obtener(i => i.Id == modelo.Id);
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontró el impuesto";
                    return respuesta;
                }
                objetoEncontrado.IdDetalleOrdenVenta = modelo.IdDetalleOrdenVenta;
                objetoEncontrado.IdTipoImpuesto = modelo.IdTipoImpuesto;
                objetoEncontrado.IdTipoFactor = modelo.IdTipoFactor;
                objetoEncontrado.IdCategoriaImpuesto = modelo.IdCategoriaImpuesto;
                objetoEncontrado.TasaCuota = modelo.TasaCuota;
                objetoEncontrado.ImporteTotal = modelo.ImporteTotal;

                respuesta.Estatus = await _repository.Editar(objetoEncontrado);
                if (respuesta.Estatus)
                {
                    respuesta.Descripcion = "Impuesto editado exitosamente";
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al intentar editar el impuesto";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Descripcion = "Ocurrio un error al intentar editar el impuesto";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(ImpuestoDetalleOrdenVentaDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _repository.Obtener(i => i.Id == modelo.Id);
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontró el impuesto";
                    return respuesta;
                }

                respuesta.Estatus = await _repository.Eliminar(objetoEncontrado);
                if (respuesta.Estatus)
                {
                    respuesta.Descripcion = "Impuesto eliminado exitosamente";
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al intentar eliminar el impuesto";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Descripcion = "Ocurrio un error al intentar eliminar el impuesto";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<List<ImpuestoDetalleOrdenVentaDTO>> ObtenerXIdDetalle(int id)
        {
            try
            {
                var lista = await _repository.ObtenerTodos(i => i.IdDetalleOrdenVenta == id);
                if (lista.Count > 0)
                {
                    return _mapper.Map<List<ImpuestoDetalleOrdenVentaDTO>>(lista);
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
    }
}
