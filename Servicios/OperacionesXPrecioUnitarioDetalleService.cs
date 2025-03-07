using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Modelos.Presupuesto;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class OperacionesXPrecioUnitarioDetalleService<T>: IOperacionesXPrecioUnitarioDetalleService<T> where T : DbContext
    {
        private readonly IGenericRepository<OperacionesXPrecioUnitarioDetalle, T> _Repositorio;
        private readonly IMapper _Mapper;

        public OperacionesXPrecioUnitarioDetalleService(
            IGenericRepository<OperacionesXPrecioUnitarioDetalle, T> repositorio
            //, PROCOMIContext context
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            //_Context = context;
            _Mapper = mapper;
        }

        public async Task<List<OperacionesXPrecioUnitarioDetalleDTO>> ObtenerXIdPrecioUnitarioDetalle(int Id)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdPrecioUnitarioDetalle == Id);
                return _Mapper.Map<List<OperacionesXPrecioUnitarioDetalleDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<OperacionesXPrecioUnitarioDetalleDTO>();
            }
        }

        public async Task<OperacionesXPrecioUnitarioDetalleDTO> ObtenerXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<OperacionesXPrecioUnitarioDetalleDTO>(query);
            }
            catch (Exception ex)
            {
                return new OperacionesXPrecioUnitarioDetalleDTO();
            }
        }

        public async Task<OperacionesXPrecioUnitarioDetalleDTO> CrearYObtener(OperacionesXPrecioUnitarioDetalleDTO registro)
        {
            var respuesta = await _Repositorio.Crear(_Mapper.Map<OperacionesXPrecioUnitarioDetalle>(registro));
            return _Mapper.Map<OperacionesXPrecioUnitarioDetalleDTO>(respuesta);
        }

        public async Task<RespuestaDTO> Editar(OperacionesXPrecioUnitarioDetalleDTO registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenerXId(registro.Id);
                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                objetoEncontrado.Descripcion = registro.Descripcion;
                objetoEncontrado.Operacion = registro.Operacion;
                objetoEncontrado.Resultado = registro.Resultado;
                var modelo = _Mapper.Map<OperacionesXPrecioUnitarioDetalle>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Insumo editado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal al editar el insumo de la entrada del almacén";
                return respuesta;
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
                    respuesta.Descripcion = "La operación no existe";
                    return respuesta;
                }
                bool eliminado = await _Repositorio.Eliminar(objetoEncontrado);
                if (!eliminado)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo eliminar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Operación eliminada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación de operación";
                return respuesta;

            }
        }

        public async Task<RespuestaDTO> EliminarXIdPrecioUnitarioDetalle(int IdPrecioUnitarioDetalle)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.ObtenerTodos(z => z.Id == IdPrecioUnitarioDetalle);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La operación no existe";
                    return respuesta;
                }
                bool eliminado = await _Repositorio.EliminarMultiple(objetoEncontrado);
                if (!eliminado)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo eliminar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Operación eliminada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación de operación";
                return respuesta;

            }
        }
    }
}
