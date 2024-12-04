using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class EstimacionesService<T> : IEstimacionesService<T> where T : DbContext
    {
        private readonly IGenericRepository<Estimaciones, T> _Repositorio;
        private readonly IMapper _Mapper;

        public EstimacionesService(IGenericRepository<Estimaciones, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<List<EstimacionesDTO>> ObtenTodosXIdPeriodo(int IdPeriodo)
        {
            var query = await _Repositorio.ObtenerTodos(z => z.IdPeriodo == IdPeriodo);
            return _Mapper.Map<List<EstimacionesDTO>>(query);
        }

        public async Task<EstimacionesDTO> ObtenXId(int id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == id);
                return _Mapper.Map<EstimacionesDTO>(query);
            }
            catch
            {
                return new EstimacionesDTO();
            }
        }

        public async Task<EstimacionesDTO> CrearYObtener(EstimacionesDTO registro)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Estimaciones>(registro));
                if (objetoCreado.Id == 0)
                    throw new TaskCanceledException("No se pudó crear");
                return _Mapper.Map<EstimacionesDTO>(objetoCreado);
            }
            catch
            {
                return new EstimacionesDTO();
            }
        }

        public async Task<bool> Editar(EstimacionesDTO registro)
        {
            var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == registro.Id);
            objetoEncontrado.ImporteDeAvance = registro.ImporteDeAvance;
            objetoEncontrado.PorcentajeAvance = registro.PorcentajeAvance;
            objetoEncontrado.CantidadAvance = registro.CantidadAvance;
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
                    respuesta.Descripcion = "Estimacion no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Estimacion eliminada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación de la estimacion";
                return respuesta;
            }
        }

        public async Task<bool> EliminarMultiple(int IdPeriodo)
        {
            var query = await _Repositorio.ObtenerTodos(z => z.IdPeriodo == IdPeriodo);
            await _Repositorio.EliminarMultiple(query);
            return true;
        }
    }
}
