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

        public Task<RespuestaDTO> Editar(ImpuestoDetalleOrdenVentaDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDTO> Eliminar(ImpuestoDetalleOrdenVentaDTO modelo)
        {
            throw new NotImplementedException();
        }
    }
}
