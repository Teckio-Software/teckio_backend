using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;


namespace ERP_TECKIO.Servicios
{
    public class DiasConsideradosService<T> : IDiasConsideradosService<T> where T : DbContext
    {
        private readonly IGenericRepository<DiasConsiderados, T> _Repositorio;
        private readonly IMapper _Mapper;
        public DiasConsideradosService(
            IGenericRepository<DiasConsiderados, T> repositorio
            , IMapper mapper
            )
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<List<DiasConsideradosDTO>> ObtenerTodosXFSI(int IdFSI)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdFactorSalarioIntegrado == IdFSI);
                return _Mapper.Map<List<DiasConsideradosDTO>>(query);
            }
            catch
            {
                return new List<DiasConsideradosDTO>();
            }
        }

        public async Task<DiasConsideradosDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<DiasConsideradosDTO>(query);
            }
            catch
            {
                return new DiasConsideradosDTO();
            }
        }

        public async Task<DiasConsideradosDTO> CrearYObtener(DiasConsideradosDTO registro)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<DiasConsiderados>(registro));
                if (objetoCreado.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo crear");
                }
                return _Mapper.Map<DiasConsideradosDTO>(objetoCreado);
            }
            catch
            {
                return new DiasConsideradosDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(DiasConsideradosDTO registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<DiasConsiderados>(registro);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == registro.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Dias Considerados no existe";
                    return respuesta;
                }
                objetoEncontrado.Codigo = registro.Codigo;
                objetoEncontrado.Descripcion = registro.Descripcion;
                objetoEncontrado.Valor = registro.Valor;
                objetoEncontrado.ArticulosLey = registro.ArticulosLey;
                objetoEncontrado.EsLaborableOpagado = registro.EsLaborableOPagado;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo editar";
                }
                respuesta.Descripcion = "Días considerados editado correctamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de los días considerados";
                return respuesta;
            }
        }
        
        public async Task<RespuestaDTO> Eliminar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Días considerados no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Días considerados eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salio mal al eliminar los días considerados";
                return respuesta;
            }
        }
    }
}
