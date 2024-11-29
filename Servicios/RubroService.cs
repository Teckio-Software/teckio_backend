using AutoMapper;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class RubroService<T> : IRubroService<T> where T : DbContext
    {
        private readonly IGenericRepository<Rubro, T> _Repositorio;
        private readonly IMapper _Mapper;

        public RubroService(
            IGenericRepository<Rubro, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(RubroCreacionDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Rubro>(modelo));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Rubro creado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salio mal con la creación del Rubro";
                return respuesta;
            }
        }

        public async Task<RubroDTO> ObtenXId(int Id)
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos(z => z.Id == Id);
                return _Mapper.Map<RubroDTO>(lista);
            }
            catch
            {
                return new RubroDTO();
            }
        }

        public async Task<List<RubroDTO>> ObtenTodos()
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<RubroDTO>>(lista);
            }
            catch
            {
                return new List<RubroDTO>();
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
                    respuesta.Descripcion = "El rubro no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Rubro eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salio mal al eliminar el rubro";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Editar(RubroDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<Rubro>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == modelo.Id);
                if(objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El rubro no existe";
                    return respuesta;
                }
                objetoEncontrado.Descripcion = modelo.Descripcion;
                objetoEncontrado.NaturalezaRubro = modelo.NaturalezaRubro;
                objetoEncontrado.TipoReporte = modelo.TipoReporte;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Rubro editada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del rubro";
                return respuesta;
            }
        }
    }
}
