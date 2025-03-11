using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class FsrxinsummoMdOService<T> : IFsrxinsummoMdOService<T> where T : DbContext
    {
        private readonly IGenericRepository<FsrxinsummoMdO, T> _repository;
        private readonly IMapper _mapper;

        public FsrxinsummoMdOService(
            IGenericRepository<FsrxinsummoMdO, T> repository,
            IMapper mapper
            ) { 
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<FsrxinsummoMdODTO> CrearYObtener(FsrxinsummoMdODTO objeto)
        {
            var crear = await _repository.Crear(_mapper.Map<FsrxinsummoMdO>(objeto));
            if (crear.Id <= 0) { 
                return new FsrxinsummoMdODTO();
            }
            return _mapper.Map<FsrxinsummoMdODTO>(crear);
        }

        public async Task<bool> Editar(FsrxinsummoMdODTO objeto)
        {
            var objetoEncontrado = await _repository.Obtener(z => z.Id == objeto.Id);
            if (objetoEncontrado.Id <= 0) {
                return false;
            }

            objetoEncontrado.Fsr = objeto.Fsr;
            objetoEncontrado.CostoDirecto = objeto.CostoDirecto;
            objetoEncontrado.CostoFinal = objeto.CostoFinal;

            var editar = await _repository.Editar(objetoEncontrado);
            if (!editar) {
                return false;
            }
            return true;    
        }

        public Task<bool> Eliminar(int IdFsi)
        {
            throw new NotImplementedException();
        }

        public Task<FsrxinsummoMdODTO> ObtenerXId(int IdFsi)
        {
            throw new NotImplementedException();
        }

        public async Task<FsrxinsummoMdODTO> ObtenerXIdInsumo(int IdInsumo)
        {
            var objetoEncontrado = await _repository.Obtener(z => z.IdInsumo == IdInsumo);
            return _mapper.Map<FsrxinsummoMdODTO>(objetoEncontrado);
        }

        public Task<List<FsrxinsummoMdODTO>> ObtenerXIdProyecto(int IdProyecto)
        {
            throw new NotImplementedException();
        }
    }
}
