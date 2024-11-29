using AutoMapper;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class FactorSalarioRealDetalleService<T> : IFactorSalarioRealDetalleService<T> where T : DbContext
    {
        private readonly IGenericRepository<FactorSalarioRealDetalle, T> _Repositorio;
        private readonly IMapper _Mapper;
        public FactorSalarioRealDetalleService(
            IGenericRepository<FactorSalarioRealDetalle, T> repositorio
            , IMapper mapper
            )
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<List<FactorSalarioRealDetalleDTO>> ObtenerTodosXFSR(int IdFactorSalarioReal)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdFactorSalarioReal == IdFactorSalarioReal);
                return _Mapper.Map<List<FactorSalarioRealDetalleDTO>>(query);
            }
            catch
            {
                return new List<FactorSalarioRealDetalleDTO>();
            }
        }

        public async Task<FactorSalarioRealDetalleDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<FactorSalarioRealDetalleDTO>(query);
            }
            catch
            {
                return new FactorSalarioRealDetalleDTO();
            }
        }

        public async Task<FactorSalarioRealDetalleDTO> CrearYObtener(FactorSalarioRealDetalleDTO registro)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<FactorSalarioRealDetalle>(registro));
                if (objetoCreado.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo crear");
                }
                return _Mapper.Map<FactorSalarioRealDetalleDTO>(objetoCreado);
            }
            catch
            {
                return new FactorSalarioRealDetalleDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(FactorSalarioRealDetalleDTO registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<FactorSalarioRealDetalle>(registro);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == registro.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El FSRDetalle no existe";
                    return respuesta;
                }
                objetoEncontrado.Codigo = registro.Codigo;
                objetoEncontrado.Descripcion = registro.Descripcion;
                objetoEncontrado.PorcentajeFsrdetalle = registro.PorcentajeFsrdetalle;
                objetoEncontrado.ArticulosLey = registro.ArticulosLey;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo editar";
                }
                respuesta.Descripcion = "FSRDetalle editado correctamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del FSRDetalle";
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
                    respuesta.Descripcion = "El FSRDetalle no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "FSRDetalle eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salio mal al eliminar el FSRDetalle";
                return respuesta;
            }
        }
    }
}
