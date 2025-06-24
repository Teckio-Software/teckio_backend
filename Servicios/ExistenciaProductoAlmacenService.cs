using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class ExistenciaProductoAlmacenService<T> : IExistenciaProductoAlmacenService<T> where T : DbContext
    {
        private readonly IGenericRepository<ExistenciaProductosAlmacen, T> _repository;
        private readonly IMapper _mapper;
        public ExistenciaProductoAlmacenService(
            IGenericRepository<ExistenciaProductosAlmacen, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<RespuestaDTO> Crear(ExistenciaProductoAlmacenDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<ExistenciaProductoAlmacenDTO> CrearYObtener(ExistenciaProductoAlmacenDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDTO> Editar(ExistenciaProductoAlmacenDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDTO> Eliminar(ExistenciaProductoAlmacenDTO modelo)
        {
            throw new NotImplementedException();
        }
    }
}
