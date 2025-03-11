using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class FsixinsummoMdOService<T> : IFsixinsummoMdOService<T> where T : DbContext
    {
        private readonly IGenericRepository<FsixinsummoMdO, T> _repository;
        private readonly IMapper _mapper;

        public FsixinsummoMdOService(
            IGenericRepository<FsixinsummoMdO, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }
        public Task<FsixinsummoMdODTO> CrearYObtener(FsixinsummoMdODTO objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Editar(FsixinsummoMdODTO objeto)
        {
            var objetoEncontrado = await _repository.Obtener(z => z.Id == objeto.Id);
            if (objetoEncontrado.Id <= 0)
            {
                return false;
            }

            objetoEncontrado.Fsi = objeto.Fsi;
            objetoEncontrado.DiasPagados = objeto.DiasPagados;
            objetoEncontrado.DiasNoLaborales = objeto.DiasNoLaborales;

            var editar = await _repository.Editar(objetoEncontrado);
            if (!editar)
            {
                return false;
            }
            return true;
        }

        public Task<bool> Eliminar(int IdFsi)
        {
            throw new NotImplementedException();
        }

        public Task<FsixinsummoMdODTO> ObtenerXId(int IdFsi)
        {
            throw new NotImplementedException();
        }

        public async Task<FsixinsummoMdODTO> ObtenerXIdInsumo(int IdInsumo)
        {
            var objetoEncontrado = await _repository.Obtener(z => z.IdInsumo == IdInsumo);
            return _mapper.Map<FsixinsummoMdODTO>(objetoEncontrado);
        }

        public Task<List<FsixinsummoMdODTO>> ObtenerXIdProyecto(int IdProyecto)
        {
            throw new NotImplementedException();
        }
    }
}
