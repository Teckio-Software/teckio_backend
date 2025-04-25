using AutoMapper;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO
{
    public class TipoImpuestoService<TContext> : ITipoImpuestoService<TContext> where TContext : DbContext
    {
        private readonly IGenericRepository<TipoImpuesto, TContext> _Repositorio;
        private readonly IMapper _Mapper;

        public TipoImpuestoService(
            IGenericRepository<TipoImpuesto, TContext> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<bool> Crear(TipoImpuestoDTO parametro)
        {
            var objetoCreado = await _Repositorio.Crear(_Mapper.Map<TipoImpuesto>(parametro));
            if (objetoCreado.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<TipoImpuestoDTO> CrearYObtener(TipoImpuestoDTO parametro)
        {
            var objetoCreado = await _Repositorio.Crear(_Mapper.Map<TipoImpuesto>(parametro));
            if (objetoCreado.Id == 0)
                return new TipoImpuestoDTO();
            return _Mapper.Map<TipoImpuestoDTO>(objetoCreado);
        }

        public async Task<bool> Editar(TipoImpuestoDTO parametro)
        {
            var modelo = _Mapper.Map<TipoImpuesto>(parametro);
            var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
            if (objetoEncontrado == null)
            {
                return false;
            }
            objetoEncontrado.ClaveImpuesto = modelo.ClaveImpuesto;
            objetoEncontrado.DescripcionImpuesto = modelo.DescripcionImpuesto;
            var respuesta = await _Repositorio.Editar(objetoEncontrado);

            return respuesta;
        }

        public async Task<bool> Eliminar(int Id)
        {
            var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == Id);
            if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
            {
                return false;
            }
            var respuesta = await _Repositorio.Eliminar(objetoEncontrado);
            return respuesta;
        }

        public async Task<List<TipoImpuestoDTO>> ObtenTodos()
        {
            var lista = await _Repositorio.ObtenerTodos();
            return _Mapper.Map<List<TipoImpuestoDTO>>(lista);
        }

        public async Task<TipoImpuestoDTO> ObtenXClave(string Clave)
        {
            var objeto = await _Repositorio.Obtener(z => z.ClaveImpuesto == Clave);
            return _Mapper.Map<TipoImpuestoDTO>(objeto);
        }

        public async Task<List<TipoImpuestoDTO>> ObtenXDescripcion(string Descripcion)
        {
            var lista = await _Repositorio.ObtenerTodos(z => z.DescripcionImpuesto == Descripcion);
            return _Mapper.Map<List<TipoImpuestoDTO>>(lista);
        }

        public async Task<TipoImpuestoDTO> ObtenXId(int Id)
        {
            var lista = await _Repositorio.Obtener(z => z.Id == Id);
            return _Mapper.Map<TipoImpuestoDTO>(lista);
        }
    }
}
