using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class TipoPolizaService<T> : ITipoPolizaService<T> where T : DbContext
    {
        private readonly IGenericRepository<TipoPoliza, T> _Repositorio;
        private readonly IMapper _Mapper;

        public TipoPolizaService(IGenericRepository<TipoPoliza, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(TipoPolizaCreacionDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<TipoPoliza>(modelo));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Tipo de poliza creado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la creación del tipo de poliza";
                return respuesta;
            }
        }

        public async Task<TipoPolizaDTO> CrearYObtener(TipoPolizaCreacionDTO modelo)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<TipoPoliza>(modelo));
                if (objetoCreado.Id == 0)
                    return new TipoPolizaDTO();
                return _Mapper.Map<TipoPolizaDTO>(objetoCreado);
            }
            catch
            {
                return new TipoPolizaDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(TipoPolizaDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<TipoPoliza>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El tipo de poliza no existe";
                    return respuesta;
                }
                objetoEncontrado.Codigo = modelo.Codigo;
                objetoEncontrado.Descripcion = modelo.Descripcion;
                objetoEncontrado.Naturaleza = modelo.Naturaleza;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Tipo de poliza editado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del tipo de poliza";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == Id);

                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El tipo de poliza no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Tipo de poliza eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación del tipo de poliza";
                return respuesta;
            }
        }

        public async Task<TipoPolizaDTO> ObtenXId(int Id)
        {
            try
            {
                var lista = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<TipoPolizaDTO>(lista);
            }
            catch
            {
                return new TipoPolizaDTO();
            }
        }

        public async Task<List<TipoPolizaDTO>> ObtenTodos()
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<TipoPolizaDTO>>(lista);
            }
            catch
            {
                return new List<TipoPolizaDTO>();
            }
        }
    }
}
