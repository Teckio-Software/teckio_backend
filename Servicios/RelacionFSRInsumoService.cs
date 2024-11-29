using AutoMapper;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class RelacionFSRInsumoService<T> : IRelacionFSRInsumoService<T> where T : DbContext
    {
        private readonly IGenericRepository<RelacionFSRInsumo, T> _Repositorio;
        private readonly IMapper _Mapper;
        public RelacionFSRInsumoService(
            IGenericRepository<RelacionFSRInsumo, T> repositorio
            , IMapper mapper
            )
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<List<RelacionFSRInsumoDTO>> ObtenerTodosXProyecto(int IdProyecto)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto);
                return _Mapper.Map<List<RelacionFSRInsumoDTO>>(query);
            }
            catch
            {
                return new List<RelacionFSRInsumoDTO>();
            }
        }

        public async Task<RelacionFSRInsumoDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<RelacionFSRInsumoDTO>(query);
            }
            catch
            {
                return new RelacionFSRInsumoDTO();
            }
        }

        public async Task<RelacionFSRInsumoDTO> CrearYObtener(RelacionFSRInsumoDTO registro)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<RelacionFSRInsumo>(registro));
                if (objetoCreado.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo crear");
                }
                return _Mapper.Map<RelacionFSRInsumoDTO>(objetoCreado);
            }
            catch
            {
                return new RelacionFSRInsumoDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(RelacionFSRInsumoDTO registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<RelacionFSRInsumo>(registro);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == registro.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La relaciónFSRInsumo no existe";
                    return respuesta;
                }
                objetoEncontrado.SueldoBase = registro.SueldoBase;
                objetoEncontrado.Prestaciones = registro.Prestaciones;
                objetoEncontrado.IdInsumo = registro.IdInsumo;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo editar";
                }
                respuesta.Descripcion = "RelaciónFSRInsumo editado correctamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de la relaciónFSRInsumo";
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
                    respuesta.Descripcion = "La relaciónFSRInsumo no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "RelaciónFSRInsumo eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salio mal al eliminar la relaciónFSRInsumo";
                return respuesta;
            }
        }
    }
}