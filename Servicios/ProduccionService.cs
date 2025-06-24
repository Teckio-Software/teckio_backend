using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class ProduccionService<T> : IProduccionService<T> where T : DbContext
    {
        private readonly IGenericRepository<Produccion, T> _repository;
        private readonly IMapper _mapper;
        public ProduccionService(
            IGenericRepository<Produccion, T> repository,
            IMapper mapper
            ) {
            _repository = repository;
            _mapper = mapper;
        }
        public Task<RespuestaDTO> Crear(ProduccionDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<ProduccionDTO> CrearYObtener(ProduccionDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDTO> Editar(ProduccionDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDTO> Eliminar(ProduccionDTO modelo)
        {
            throw new NotImplementedException();
        }
    }
}
