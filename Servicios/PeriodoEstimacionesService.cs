using AutoMapper;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class PeriodoEstimacionesService<T> : IPeriodoEstimacionesService<T> where T : DbContext
    {
        private readonly IGenericRepository<PeriodoEstimaciones, T> _Repositorio;
        private readonly IMapper _Mapper;

        public PeriodoEstimacionesService(IGenericRepository<PeriodoEstimaciones, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<List<PeriodoEstimacionesDTO>> ObtenTodosXIdProyecto(int IdProyecto)
        {
            var query = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto);
            return _Mapper.Map<List<PeriodoEstimacionesDTO>>(query);
        }

        public async Task<PeriodoEstimacionesDTO> ObtenXId(int id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == id);
                return _Mapper.Map<PeriodoEstimacionesDTO>(query);
            }
            catch
            {
                return new PeriodoEstimacionesDTO();
            }
        }

        public async Task<PeriodoEstimacionesDTO> CrearYObtener(PeriodoEstimacionesDTO registro)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<PeriodoEstimaciones>(registro));
                if (objetoCreado.Id == 0)
                    throw new TaskCanceledException("No se pudó crear");
                return _Mapper.Map<PeriodoEstimacionesDTO>(objetoCreado);
            }
            catch
            {
                return new PeriodoEstimacionesDTO();
            }
        }

        public async Task<bool> Editar(PeriodoEstimacionesDTO registro)
        {
            var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == registro.Id);
            objetoEncontrado.EsCerrada = registro.EsCerrada;
            objetoEncontrado.FechaInicio = registro.FechaInicio;
            objetoEncontrado.FechaTermino = registro.FechaTermino;
            objetoEncontrado.NumeroPeriodo = registro.NumeroPeriodo;
            var registroEditado = await _Repositorio.Editar(objetoEncontrado);
            return registroEditado;
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
                    respuesta.Descripcion = "El periodo no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Periodo eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación del periodo";
                return respuesta;
            }
        }
    }
}
