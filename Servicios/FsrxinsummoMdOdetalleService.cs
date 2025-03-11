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

        public async Task<bool> Editar(FsrxinsummoMdOdetalleDTO objeto)
        {
            var objetoEncontrado = await _repository.Obtener(z => z.Id == objeto.Id);
            if (objetoEncontrado.Id <= 0) {
                return false;
            }
            objetoEncontrado.Descripcion = objeto.Descripcion;
            objetoEncontrado.ArticulosLey = objeto.ArticulosLey;
            objetoEncontrado.Codigo = objeto.Codigo;
            objetoEncontrado.PorcentajeFsr = objeto.PorcentajeFsr;
            var editar = await _repository.Editar(objetoEncontrado);
            if (!editar) {
                return false;
            }
            return true;
        }

        public async Task<bool> Eliminar(int IdFsrDeatalle)
        {
            var objetoEncontrado = await _repository.Obtener(z => z.Id == IdFsrDeatalle);
            if (objetoEncontrado.Id <= 0)
            {
                return false;
            }

            var eliminar = await _repository.Eliminar(objetoEncontrado);
            if (!eliminar)
            {
                return false;
            }
            return true;
        }

        public async Task<FsrxinsummoMdOdetalleDTO> ObtenerXId(int IdFsrDetalle)
        {
            var detalle = await _repository.Obtener(z => z.Id == IdFsrDetalle);
            return _mapper.Map<FsrxinsummoMdOdetalleDTO>(detalle);
        }

        public async Task<List<FsrxinsummoMdOdetalleDTO>> ObtenerXIdFsr(int IdFsr)
        {
            var lista = await _repository.ObtenerTodos(z => z.IdFsrxinsummoMdO == IdFsr);
            return _mapper.Map<List<FsrxinsummoMdOdetalleDTO>>(lista);
        }
    }
}
