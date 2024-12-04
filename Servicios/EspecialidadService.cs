using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class EspecialidadService<T> : IEspecialidadService<T> where T : DbContext
    {
        private readonly IGenericRepository<Especialidad, T> _Repositorio;
        private readonly IMapper _Mapper;

        public EspecialidadService(IGenericRepository<Especialidad, T> respositorio
            , IMapper mapper)
        {
            _Repositorio = respositorio;
            _Mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(EspecialidadDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Especialidad>(modelo));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Especialidad creada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la creación de la especialidad";
                return respuesta;
            }
        }

        public async Task<EspecialidadDTO> CrearYObtener(EspecialidadDTO modelo)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Especialidad>(modelo));
                if (objetoCreado.Id == 0)
                    return new EspecialidadDTO();
                return _Mapper.Map<EspecialidadDTO>(objetoCreado);
            }
            catch
            {
                return new EspecialidadDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(EspecialidadDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<Especialidad>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La especialidad no existe";
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
                respuesta.Descripcion = "Especialidad editada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de la especialidad";
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
                    respuesta.Descripcion = "La especialidad no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Especialidad eliminada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación de la especialidad";
                return respuesta;
            }
        }

        public async Task<List<EspecialidadDTO>> ObtenerTodos()
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<EspecialidadDTO>>(lista);
            }
            catch
            {
                return new List<EspecialidadDTO>();
            }
        }

        public async Task<EspecialidadDTO> ObtenXId(int Id)
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos(z => z.Id == Id);
                return _Mapper.Map<EspecialidadDTO>(lista);
            }
            catch
            {
                return new EspecialidadDTO();
            }
        }
    }
}
