using AutoMapper;
using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using SistemaERP.BLL.Contrato.Gantt;


using SistemaERP.DTO.Presupuesto.Gantt;
using SistemaERP.Model.Procomi.Proyecto;


namespace SistemaERP.BLL.Servicios.Gantt
{
    public class DependenciaProgramacionEstimadaService<T> : IDependenciaProgramacionEstimadaService<T> where T : DbContext
    {
        private readonly IGenericRepository<DependenciaProgramacionEstimada, T> _Repositorio;
        private readonly IMapper _Mapper;

        public DependenciaProgramacionEstimadaService(
            IGenericRepository<DependenciaProgramacionEstimada, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<List<DependenciaProgramacionEstimadaDTO>> ObtenerXIdProgramacionEstimadaGantt(int IdPE)
        {
            var datos = await _Repositorio.ObtenerTodos(z => z.IdProgramacionEstimadaGantt == IdPE);
            var registros = _Mapper.Map<List<DependenciaProgramacionEstimadaDTO>>(datos);
            return registros;
        }

        public async Task<DependenciaProgramacionEstimadaDTO> ObtenerXId(int Id)
        {
            var datos = await _Repositorio.Obtener(z => z.Id == Id);
            var registros = _Mapper.Map<DependenciaProgramacionEstimadaDTO>(datos);
            return registros;
        }

        public async Task<DependenciaProgramacionEstimadaDTO> CrearYObtener(DependenciaProgramacionEstimadaDTO registro)
        {
            var registroMappeado = _Mapper.Map<DependenciaProgramacionEstimada>(registro);
            var crearRegistros = await _Repositorio.Crear(registroMappeado);
            return _Mapper.Map<DependenciaProgramacionEstimadaDTO>(crearRegistros);
        }

        public async Task<RespuestaDTO> Editar(DependenciaProgramacionEstimadaDTO registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<DependenciaProgramacionEstimada>(registro);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == modelo.Id);
                if(objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La dependencia no existe";
                    return respuesta;
                }

                objetoEncontrado.IdProgramacionEstimadaGantt = modelo.IdProgramacionEstimadaGantt;
                if (respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se puede editar la dependencia";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Dependencia editada";
                return respuesta;
            }
            catch(Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = ex.Message;
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(int id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La dependencia no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (respuesta.Estatus = false)
                {
                    respuesta.Descripcion = "No se pudo eliminar dependencia";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Algo salió mal en la eliminación de dependencia";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación de dependencia";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> EliminarMultiple(List<DependenciaProgramacionEstimadaDTO> registros)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdProgramacionEstimadaGantt == registros[0].IdProgramacionEstimadaGantt);
                var resultado = await _Repositorio.EliminarMultiple(query);
                if (!resultado)
                {
                    respuesta.Descripcion = "No se pueden eliminar las dependencias";
                    respuesta.Estatus = resultado;
                }
                respuesta.Descripcion = "Los registros han sido eliminados";
                respuesta.Estatus = true;
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Descripcion = ex.Message;
                respuesta.Estatus = false;
                return respuesta;
            }
        }
    }
}
