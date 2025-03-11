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

        public async Task<bool> Editar(FsixinsummoMdOdetalleDTO objeto)
        {
            var objetoEncontrado = await _repository.Obtener(z => z.Id == objeto.Id);
            if (objetoEncontrado.Id <= 0)
            {
                return false;
            }
            objetoEncontrado.Descripcion = objeto.Descripcion;
            objetoEncontrado.ArticulosLey = objeto.ArticulosLey;
            objetoEncontrado.Codigo = objeto.Codigo;
            objetoEncontrado.Dias = objeto.Dias;
            var editar = await _repository.Editar(objetoEncontrado);
            if (!editar)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Eliminar(int IdFsiDeatalle)
        {
            var objetoEncontrado = await _repository.Obtener(z => z.Id == IdFsiDeatalle);
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

        public async Task<FsixinsummoMdOdetalleDTO> ObtenerXId(int IdFsiDetalle)
        {
            var detalle = await _repository.Obtener(z => z.Id == IdFsiDetalle);
            return _mapper.Map<FsixinsummoMdOdetalleDTO>(detalle);
        }

        public async Task<List<FsixinsummoMdOdetalleDTO>> ObtenerXIdFsi(int IdFsi)
        {
            var lista = await _repository.ObtenerTodos(z => z.IdFsixinsummoMdO == IdFsi);
            return _mapper.Map<List<FsixinsummoMdOdetalleDTO>>(lista);
        }
    }
}
