using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class FsrxinsummoMdOdetalleService<T> : IFsrxinsummoMdOdetalleService<T> where T : DbContext
    {
        private readonly IGenericRepository<FsrxinsummoMdOdetalle, T> _repository;
        private readonly IMapper _mapper;

        public FsrxinsummoMdOdetalleService(
            IGenericRepository<FsrxinsummoMdOdetalle, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Crear(FsrxinsummoMdOdetalleDTO objeto)
        {
            var resultado = await _repository.Crear(_mapper.Map<FsrxinsummoMdOdetalle>(objeto));
            if (resultado.Id <= 0) { 
                return false;
            }
            return true;
        }

        public Task<FsrxinsummoMdOdetalleDTO> CrearYObtener(FsrxinsummoMdOdetalleDTO objeto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(FsrxinsummoMdOdetalleDTO objeto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int IdFsrDeatalle)
        {
            throw new NotImplementedException();
        }

        public Task<FsrxinsummoMdOdetalleDTO> ObtenerXId(int IdFsrDetalle)
        {
            throw new NotImplementedException();
        }

        public async Task<List<FsrxinsummoMdOdetalleDTO>> ObtenerXIdFsr(int IdFsr)
        {
            var lista = await _repository.ObtenerTodos(z => z.IdFsrxinsummoMdO == IdFsr);
            return _mapper.Map<List<FsrxinsummoMdOdetalleDTO>>(lista);
        }
    }
}
