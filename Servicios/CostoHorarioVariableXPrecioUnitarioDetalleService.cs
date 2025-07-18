using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos.Presupuesto;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class CostoHorarioVariableXPrecioUnitarioDetalleService<T> : ICostoHorarioVariableXPrecioUnitarioDetalleService<T> where T : DbContext
    {
        private readonly IGenericRepository<CostoHorarioVariableXPrecioUnitarioDetalle, T> _Repositorio;
        private readonly IMapper _Mapper;

        public CostoHorarioVariableXPrecioUnitarioDetalleService(IGenericRepository<CostoHorarioVariableXPrecioUnitarioDetalle, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<CostoHorarioVariableXPrecioUnitarioDetalleDTO> CrearYObtener(CostoHorarioVariableXPrecioUnitarioDetalleDTO registro)
        {
            var objetoCreado = await _Repositorio.Crear(_Mapper.Map<CostoHorarioVariableXPrecioUnitarioDetalle>(registro));
            if (objetoCreado.Id == 0)
            {
                return new CostoHorarioVariableXPrecioUnitarioDetalleDTO();
            }
            return _Mapper.Map<CostoHorarioVariableXPrecioUnitarioDetalleDTO>(objetoCreado);
        }

        public async Task<RespuestaDTO> Editar(CostoHorarioVariableXPrecioUnitarioDetalleDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<CostoHorarioVariableXPrecioUnitarioDetalle>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El Costo Fijo no existe";
                    return respuesta;
                }
                objetoEncontrado.IdPrecioUnitarioDetalle = modelo.IdPrecioUnitarioDetalle;
                objetoEncontrado.TipoCostoVariable = modelo.TipoCostoVariable;
                objetoEncontrado.Rendimiento = modelo.Rendimiento;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Costo Fijo editado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del Costo Fijo";
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
                    respuesta.Descripcion = "El Costo Fijo no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Costo Fijo eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación del Costo Fijo";
                return respuesta;
            }
        }

        public async Task<CostoHorarioVariableXPrecioUnitarioDetalleDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<CostoHorarioVariableXPrecioUnitarioDetalleDTO>(query);
            }
            catch
            {
                return new CostoHorarioVariableXPrecioUnitarioDetalleDTO();
            }
        }

        public async Task<List<CostoHorarioVariableXPrecioUnitarioDetalleDTO>> ObtenTodosXIdPrecioUnitarioDetalle(int IdPrecioUnitarioDetalle)
        {
            var registros = await _Repositorio.ObtenerTodos(z => z.IdPrecioUnitarioDetalle == IdPrecioUnitarioDetalle);
            return _Mapper.Map<List<CostoHorarioVariableXPrecioUnitarioDetalleDTO>>(registros);
        }
    }
}
