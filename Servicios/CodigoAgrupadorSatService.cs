using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class CodigoAgrupadorSatService<T> : ICodigoAgrupadorService<T> where T : DbContext
    {
        private readonly IGenericRepository<CodigoAgrupadorSat, T> _Repositorio;
        private readonly IMapper _Mapper;

        public CodigoAgrupadorSatService(
            IGenericRepository<CodigoAgrupadorSat, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(CodigoAgrupadorSatCreacionDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<CodigoAgrupadorSat>(modelo));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear el código agrupador";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Código agrupador creado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la creación del código agrupador";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Editar(CodigoAgrupadorSatDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<CodigoAgrupadorSat>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == modelo.Id);
                if(objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El código agrupador no existe";
                    return respuesta;
                }
                objetoEncontrado.Descripcion = parametro.Descripcion;
                objetoEncontrado.Nivel = parametro.Nivel;
                objetoEncontrado.Codigo = parametro.Codigo;
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Codigo agrupador editado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del código agrupador";
                return respuesta;
            }
        }

        public async Task<List<CodigoAgrupadorSatDTO>> ObtenTodos()
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<CodigoAgrupadorSatDTO>>(lista);
            }
            catch
            {
                return new List<CodigoAgrupadorSatDTO>();
            }
        }

        public async Task<CodigoAgrupadorSatDTO> ObtenXId(int id)
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos(z => z.Id == id);
                return _Mapper.Map<CodigoAgrupadorSatDTO>(lista);
            }
            catch
            {
                return new CodigoAgrupadorSatDTO();
            }
        }

        public async Task<RespuestaDTO> Eliminar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == Id);
                if(objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El código agrupador no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "El código agrupador no se puede eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Código agrupador eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal con la eliminación del código agrupador del cliente";
                return respuesta;
            }
        }
    }
}
