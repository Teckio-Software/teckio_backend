using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class EntradaProduccionAlmacenService<T> : IEntradaProduccionAlmacenService<T> where T : DbContext
    {
        private readonly IGenericRepository<EntradaProduccionAlmacen, T> _repository;
        private readonly IMapper _mapper;
        public EntradaProduccionAlmacenService(
            IGenericRepository<EntradaProduccionAlmacen, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<RespuestaDTO> Crear(EntradaProduccionAlmacenDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<EntradaProduccionAlmacenDTO> CrearYObtener(EntradaProduccionAlmacenDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDTO> Editar(EntradaProduccionAlmacenDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDTO> Eliminar(EntradaProduccionAlmacenDTO modelo)
        {
            throw new NotImplementedException();
        }
    }
}
