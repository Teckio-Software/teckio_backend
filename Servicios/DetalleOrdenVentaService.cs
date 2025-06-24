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
        public Task<RespuestaDTO> Crear(DetalleOrdenVentaDTO modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<DetalleOrdenVentaDTO> CrearYObtener(DetalleOrdenVentaDTO modelo)
        {
            var respuesta = await _repository.Crear(_mapper.Map<DetalleOrdenVentum>(modelo));
            return _mapper.Map<DetalleOrdenVentaDTO>(respuesta);
        }

        public Task<RespuestaDTO> Editar(DetalleOrdenVentaDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDTO> Eliminar(DetalleOrdenVentaDTO modelo)
        {
            throw new NotImplementedException();
        }
    }
}
