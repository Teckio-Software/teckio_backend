using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Modelos.Presupuesto;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class CostoHorarioFijoXPrecioUnitarioDetalleService<T> : ICostoHorarioFijoXPrecioUnitarioDetalleService<T> where T : DbContext
    {
        private readonly IGenericRepository<CostoHorarioFijoXPrecioUnitarioDetalle, T> _Repositorio;
        private readonly IMapper _Mapper;

        public CostoHorarioFijoXPrecioUnitarioDetalleService(IGenericRepository<CostoHorarioFijoXPrecioUnitarioDetalle, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<CostoHorarioFijoXPrecioUnitarioDetalleDTO> CrearYObtener(CostoHorarioFijoXPrecioUnitarioDetalleDTO registro)
        {
            var objetoCreado = await _Repositorio.Crear(_Mapper.Map<CostoHorarioFijoXPrecioUnitarioDetalle>(registro));
            if(objetoCreado.Id == 0)
            {
                return new CostoHorarioFijoXPrecioUnitarioDetalleDTO();
            }
            return _Mapper.Map<CostoHorarioFijoXPrecioUnitarioDetalleDTO>(objetoCreado);
        }

        public async Task<RespuestaDTO> Editar(CostoHorarioFijoXPrecioUnitarioDetalleDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<CostoHorarioFijoXPrecioUnitarioDetalle>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El Costo Fijo no existe";
                    return respuesta;
                }
                objetoEncontrado.Inversion = modelo.Inversion;
                objetoEncontrado.InteresAnual = modelo.InteresAnual;
                objetoEncontrado.HorasUso = modelo.HorasUso;
                objetoEncontrado.VidaUtil = modelo.VidaUtil;
                objetoEncontrado.PorcentajeReparacion = modelo.PorcentajeReparacion;
                objetoEncontrado.PorcentajeSeguroAnual= modelo.PorcentajeSeguroAnual;
                objetoEncontrado.GastoAnual = modelo.GastoAnual;
                objetoEncontrado.MesTrabajoReal = modelo.MesTrabajoReal;
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

        public async Task<CostoHorarioFijoXPrecioUnitarioDetalleDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<CostoHorarioFijoXPrecioUnitarioDetalleDTO>(query);
            }
            catch
            {
                return new CostoHorarioFijoXPrecioUnitarioDetalleDTO();
            }
        }

        public async Task<CostoHorarioFijoXPrecioUnitarioDetalleDTO> ObtenTodosXIdPrecioUnitarioDetalle(int IdPrecioUnitarioDetalle)
        {
            var registros = await _Repositorio.Obtener(z => z.IdPrecioUnitarioDetalle == IdPrecioUnitarioDetalle);
            return _Mapper.Map<CostoHorarioFijoXPrecioUnitarioDetalleDTO>(registros);
        }
    }
}
