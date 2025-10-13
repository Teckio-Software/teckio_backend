using AutoMapper;
using ERP_TECKIO;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO
{
    public class AlmacenService<T> : IAlmacenService<T> where T : DbContext
    {
        private readonly IGenericRepository<Almacen, T> _Repositorio;
        private readonly IMapper _Mapper;

        public AlmacenService(IGenericRepository<Almacen, T> genericRepository, IMapper mapper)
        {
            _Repositorio = genericRepository;
            _Mapper = mapper;
        }


        public async Task<RespuestaDTO> Crear(AlmacenCreacionDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
                if (parametro.IdProyecto <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No hay un proyecto asociado";
                    return respuesta;
                }
                if (string.IsNullOrEmpty(parametro.Codigo) || string.IsNullOrEmpty(parametro.AlmacenNombre)
                    || string.IsNullOrEmpty(parametro.Responsable) || string.IsNullOrEmpty(parametro.Domicilio)
                    || string.IsNullOrEmpty(parametro.Colonia) || string.IsNullOrEmpty(parametro.Ciudad)
                    || string.IsNullOrEmpty(parametro.Telefono))
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Captura todos los campos del almacén";
                    return respuesta;
                }
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Almacen>(parametro));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Almacén creado";
                return respuesta;
        }
        public async Task<AlmacenDTO> CrearYObtener(AlmacenDTO modelo)
        {
                var creacion = await _Repositorio.Crear(_Mapper.Map<Almacen>(modelo));

                if (creacion.Id == 0) {
                return new AlmacenDTO();
            }

                return _Mapper.Map<AlmacenDTO>(creacion);
        }

        public async Task<RespuestaDTO> Editar(AlmacenDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();

            var modelo = _Mapper.Map<Almacen>(parametro);

            var objeto = await _Repositorio.Obtener(u => u.Id == modelo.Id);

            if (objeto == null)
            {
                return respuesta;
            }

            objeto.Codigo = modelo.Codigo;
            objeto.AlmacenNombre = modelo.AlmacenNombre;
            objeto.Central = modelo.Central;
            objeto.Responsable = modelo.Responsable;
            objeto.Domicilio = modelo.Domicilio;
            objeto.Colonia = modelo.Colonia;
            objeto.Ciudad = modelo.Ciudad;
            objeto.Telefono = modelo.Telefono;
            if (objeto.Central == true)
            {
                objeto.IdProyecto = null;
            }
            else
            {
                objeto.IdProyecto = parametro.IdProyecto;
            }
                respuesta.Estatus = await _Repositorio.Editar(objeto);

                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Almacén editado";
                return respuesta;

        }

        public async Task<RespuestaDTO> Eliminar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
                var objetoEncontrado = await ObtenXId(Id);

                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El almacén no existe";
                    return respuesta;
                }
                var modelo = _Mapper.Map<Almacen>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Eliminar(modelo);

                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo eliminars";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Almacén eliminado";
                return respuesta;
        }
        public async Task<List<AlmacenDTO>> ObtenerXIdProyecto(int IdProyecto)
        {
                var lista = await _Repositorio.ObtenerTodos(a => a.IdProyecto == IdProyecto);
                return _Mapper.Map<List<AlmacenDTO>>(lista.ToList());

        }

        public async Task<List<AlmacenDTO>> ObtenerXCodigo(string parametro)
        {

            var lista = await _Repositorio.ObtenerTodos(abc => abc.Codigo == parametro);
            return _Mapper.Map<List<AlmacenDTO>>(lista.ToList());
        }

        public async Task<AlmacenDTO> ObtenXId(int Id)
        {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<AlmacenDTO>(query);
        }

        public async Task<List<AlmacenDTO>> ObtenTodos()
        {
            var query = await _Repositorio.ObtenerTodos();
            return _Mapper.Map<List<AlmacenDTO>>(query);
        }

        public Task<RespuestaDTO> Crear(AlmacenDTO modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AlmacenDTO>> ObtenCentrales()
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos(a => a.IdProyecto == null);
                if (lista.Count <= 0)
                {
                    return new List<AlmacenDTO>();
                }
                else
                {
                    return _Mapper.Map<List<AlmacenDTO>>(lista);
                }
            }
            catch
            {
                return new List<AlmacenDTO>();
            }
        }

        public async Task<List<AlmacenDTO>> ObtenerCentralesYDeProyecto(int IdProyecto)
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos(a => a.IdProyecto == null || a.IdProyecto == IdProyecto);
                if (lista.Count <= 0)
                {
                    return new List<AlmacenDTO>();
                }
                else
                {
                    return _Mapper.Map<List<AlmacenDTO>>(lista);
                }
            }
            catch
            {
                return new List<AlmacenDTO>();
            }
        }
    }
}
