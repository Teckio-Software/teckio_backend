using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class IndirectosService<T> : IIndirectosService<T> where T : DbContext
    {
        private readonly IGenericRepository<Indirectos, T> _Repositorio;
        private readonly IMapper _Mapper;
        public IndirectosService(IGenericRepository<Indirectos, T> genericRepository, IMapper mapper)
        {
            _Repositorio = genericRepository;
            _Mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(IndirectosDTO objeto)
        {
            var respuesta = new RespuestaDTO();
            var modelo = await _Repositorio.Crear(_Mapper.Map<Indirectos>(objeto));
            if (modelo.Id <= 0) {
                respuesta.Descripcion = "No se creo";
                respuesta.Estatus = false;
                return respuesta;
            }
            respuesta.Descripcion = "Se creo el indirecto";
            respuesta.Estatus = true;
            return respuesta;
        }

        public async Task<IndirectosDTO> CrearYObtener(IndirectosDTO objeto)
        {
            var respuesta = await _Repositorio.Crear(_Mapper.Map<Indirectos>(objeto));
            return _Mapper.Map<IndirectosDTO>(respuesta);
        }

        public async Task<RespuestaDTO> Editar(IndirectosDTO objeto)
        {
            var respuesta = new RespuestaDTO();
            var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == objeto.Id);
            if (objetoEncontrado.Id <= 0) {
                respuesta.Descripcion = "No se encontro el indirecto";
                respuesta.Estatus = false;
                return respuesta;
            }
            objetoEncontrado.Codigo = objeto.Codigo;
            objetoEncontrado.Descripcion = objeto.Descripcion;
            objetoEncontrado.Porcentaje = objeto.Porcentaje;
            var resultado = await _Repositorio.Editar(objetoEncontrado);
            if (!resultado)
            {
                respuesta.Descripcion = "No se edito";
                respuesta.Estatus = false;
                return respuesta;
            }
            respuesta.Descripcion = "Se edito el indirecto";
            respuesta.Estatus = true;
            return respuesta;
        }

        public async Task<RespuestaDTO> Eliminar(IndirectosDTO objeto)
        {
            var respuesta = new RespuestaDTO();
            var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == objeto.Id);
            if (objetoEncontrado.Id <= 0)
            {
                respuesta.Descripcion = "No se encontro el indirecto";
                respuesta.Estatus = false;
                return respuesta;
            }
            var eliminar = await _Repositorio.Eliminar(objetoEncontrado);
            if (!eliminar)
            {
                respuesta.Descripcion = "No se elimino";
                respuesta.Estatus = false;
                return respuesta;
            }
            respuesta.Descripcion = "Se elimino el indirecto";
            respuesta.Estatus = true;
            return respuesta;
        }

        public async Task<IndirectosDTO> ObtenerXId(int idIndirecto)
        {
            var objeto = await _Repositorio.Obtener(z => z.Id == idIndirecto);
            return _Mapper.Map<IndirectosDTO>(objeto);
        }

        public async Task<List<IndirectosDTO>> ObtenerXIdConjunto(int IdConjunto)
        {
            var lista = await _Repositorio.ObtenerTodos(z => z.IdConjuntoIndirectos == IdConjunto);
            return _Mapper.Map<List<IndirectosDTO>>(lista);
        }
    }
}
