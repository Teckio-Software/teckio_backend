using AutoMapper;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class ProgramacionEstimadaService<T> : IProgramacionEstimadaService<T> where T : DbContext
    {
        private readonly IGenericRepository<ProgramacionEstimada, T> _Repositorio;
        private readonly IMapper _Mapper;

        public ProgramacionEstimadaService(IGenericRepository<ProgramacionEstimada, T> repositorio, IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<List<ProgramacionEstimadaDTO>> ObtenerTodosXProyecto(int IdProyecto)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto);
                return _Mapper.Map<List<ProgramacionEstimadaDTO>>(query);
            }
            catch
            {
                return new List<ProgramacionEstimadaDTO>();
            }
        }

        public async Task<ProgramacionEstimadaDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<ProgramacionEstimadaDTO>(query);
            }
            catch
            {
                return new ProgramacionEstimadaDTO();
            }
        }

        public async Task<ProgramacionEstimadaDTO> CrearYObtener(ProgramacionEstimadaDTO hijo)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<ProgramacionEstimada>(hijo));
                if(objetoCreado.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo crear");
                }
                return _Mapper.Map<ProgramacionEstimadaDTO>(objetoCreado);
            }
            catch
            {
                return new ProgramacionEstimadaDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(ProgramacionEstimadaDTO registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<ProgramacionEstimada>(registro);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == registro.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La programacion estimada no existe";
                    return respuesta;
                }
                objetoEncontrado.IdProyecto = registro.IdProyecto;
                objetoEncontrado.IdPrecioUnitario = registro.IdPrecioUnitario;
                objetoEncontrado.Inicio = registro.Inicio;
                objetoEncontrado.Termino = registro.Termino;
                objetoEncontrado.IdPredecesora = registro.IdPredecesora;
                objetoEncontrado.DiasTranscurridos = registro.DiasTranscurridos;
                objetoEncontrado.Nivel = registro.Nivel;
                objetoEncontrado.IdPadre = registro.IdPadre;
                objetoEncontrado.Predecesor = registro.Predecesor;
                objetoEncontrado.IdConcepto = registro.IdConcepto;
                objetoEncontrado.Progreso = registro.Progreso;
                objetoEncontrado.Comando = registro.Comando;
                objetoEncontrado.DiasComando = registro.DiasComando;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo editar";
                }
                respuesta.Descripcion = "Programacion estimada editado correctamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del programacion estimada";
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
                    respuesta.Descripcion = "La programacion estimada no existe";
                    return respuesta;
                }
                bool eliminado = await _Repositorio.Eliminar(objetoEncontrado);
                if (!eliminado)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Programacion estimada eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación del programacion estimada";
                return respuesta;
            }
        }
    }
}
