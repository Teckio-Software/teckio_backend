using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class ConceptoService<T> : IConceptoService<T> where T : DbContext
    {
        private readonly IGenericRepository<Concepto, T> _Repositorio;
        private readonly IMapper _Mapper;

        public ConceptoService(IGenericRepository<Concepto, T> respositorio
            , IMapper mapper)
        {
            _Repositorio = respositorio;
            _Mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(ConceptoDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Concepto>(modelo));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Concepto creado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la creación del concepto";
                return respuesta;
            }
        }

        public async Task<ConceptoDTO> CrearYObtener(ConceptoDTO modelo)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Concepto>(modelo));
                if (objetoCreado.Id == 0)
                    return new ConceptoDTO();
                return _Mapper.Map<ConceptoDTO>(objetoCreado);
            }
            catch
            {
                return new ConceptoDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(ConceptoDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<Concepto>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La concepto no existe";
                    return respuesta;
                }
                objetoEncontrado.Descripcion = modelo.Descripcion;
                objetoEncontrado.Codigo = modelo.Codigo;
                objetoEncontrado.Unidad = modelo.Unidad;
                objetoEncontrado.IdEspecialidad = modelo.IdEspecialidad;
                objetoEncontrado.CostoUnitario = modelo.CostoUnitario;
                objetoEncontrado.PorcentajeIndirecto = modelo.PorcentajeIndirecto;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Concepto editado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del concepto";
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
                    respuesta.Descripcion = "El Concepto no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Concepto eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación del concepto";
                return respuesta;
            }
        }

        public async Task<List<ConceptoDTO>> ObtenerTodos(int IdProyecto)
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto);
                return _Mapper.Map<List<ConceptoDTO>>(lista);
            }
            catch
            {
                return new List<ConceptoDTO>();
            }
        }

        public async Task<ConceptoDTO> ObtenXId(int Id)
        {
            try
            {
                var lista = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<ConceptoDTO>(lista);
            }
            catch
            {
                return new ConceptoDTO();
            }
        }
    }
}
