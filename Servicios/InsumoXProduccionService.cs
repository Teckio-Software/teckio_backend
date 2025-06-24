using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class InsumoXProduccionService<T> : IInsumoXProduccionService<T> where T : DbContext
    {
        private readonly IGenericRepository<InsumoXproduccion, T> _repository;
        private readonly IMapper _mapper;
        public InsumoXProduccionService(
            IGenericRepository<InsumoXproduccion, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }
        public Task<RespuestaDTO> Crear(InsumoXProduccionDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<InsumoXProduccionDTO> CrearYObtener(InsumoXProduccionDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDTO> Editar(InsumoXProduccionDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDTO> Eliminar(InsumoXProduccionDTO modelo)
        {
            throw new NotImplementedException();
        }
    }
}
