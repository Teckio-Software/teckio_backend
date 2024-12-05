using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class TipoInsumoService<T> : ITipoInsumoService<T> where T: DbContext
    {
        private readonly IGenericRepository<TipoInsumo, T> _Repositorio;
        //private readonly PROCOMIContext _Context;
        private readonly IMapper _Mapper;

        public TipoInsumoService(IGenericRepository<TipoInsumo, T> repositorio
            //, PROCOMIContext context
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            //_Context = context;
            _Mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(TipoInsumoCreacionDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<TipoInsumo>(modelo));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Tipo de insumo creado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la creación del tipo de insumo";
                return respuesta;
            }
        }

        public async Task<TipoInsumoDTO> CrearYObtener(TipoInsumoCreacionDTO modelo)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<TipoInsumo>(modelo));
                if (objetoCreado.Id == 0)
                    return new TipoInsumoDTO();
                return _Mapper.Map<TipoInsumoDTO>(objetoCreado);
            }
            catch
            {
                return new TipoInsumoDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(TipoInsumoDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<TipoInsumo>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El tipo de insumo no existe";
                    return respuesta;
                }
                objetoEncontrado.Descripcion = modelo.Descripcion;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Tipo de insumo editado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del tipo de insumo";
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
                    respuesta.Descripcion = "El tipo de insumo no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Tipo de insumo eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación del tipo de insumo";
                return respuesta;
            }
        }

        public async Task<List<TipoInsumoDTO>> Lista()
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<TipoInsumoDTO>>(query);
            }
            catch
            {
                return new List<TipoInsumoDTO>();
            }
        }

        public async Task<TipoInsumoDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.Id == Id);
                return _Mapper.Map<TipoInsumoDTO>(query);
            }
            catch
            {
                return new TipoInsumoDTO();
            }
        }
    }
}
