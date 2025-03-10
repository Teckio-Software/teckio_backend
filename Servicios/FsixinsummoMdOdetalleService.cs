using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class FsixinsummoMdOdetalleService<T> : IFsixinsummoMdOdetalleService<T> where T : DbContext
    {
        private readonly IGenericRepository<FsixinsummoMdOdetalle, T> _repository;
        private readonly IMapper _mapper;

        public FsixinsummoMdOdetalleService(
            IGenericRepository<FsixinsummoMdOdetalle, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Crear(FsixinsummoMdOdetalleDTO objeto)
        {
            var resultado = await _repository.Crear(_mapper.Map<FsixinsummoMdOdetalle>(objeto));
            if (resultado.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public Task<FsixinsummoMdOdetalleDTO> CrearYObtener(FsixinsummoMdOdetalleDTO objeto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(FsixinsummoMdOdetalleDTO objeto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int IdFsiDeatalle)
        {
            throw new NotImplementedException();
        }

        public Task<FsixinsummoMdOdetalleDTO> ObtenerXId(int IdFsiDetalle)
        {
            throw new NotImplementedException();
        }

        public async Task<List<FsixinsummoMdOdetalleDTO>> ObtenerXIdFsi(int IdFsi)
        {
            var lista = await _repository.ObtenerTodos(z => z.IdFsixinsummoMdO == IdFsi);
            return _mapper.Map<List<FsixinsummoMdOdetalleDTO>>(lista);
        }
    }
}
