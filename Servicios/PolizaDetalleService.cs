using AutoMapper;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class PolizaDetalleService<T> : IPolizaDetalleService<T> where T : DbContext
    {
        private readonly IGenericRepository<PolizaDetalle, T> _Repositorio;
        private readonly IMapper _Mapper;

        public PolizaDetalleService(
            IGenericRepository<PolizaDetalle, T> repositorio
            , IMapper mapper)
            {
            _Repositorio = repositorio;
            _Mapper = mapper;
            }

        public async Task<RespuestaDTO> Crear(PolizaDetalleDTO parametros)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<PolizaDetalle>(parametros));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear el detalle de poliza";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Detalle de poliza creado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la creación del detalle de poliza";
                return respuesta;
            }
        }

        public async Task<List<PolizaDetalleDTO>> ObtenTodosXIdPoliza(int IdPoliza)
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos(z => z.IdPoliza == IdPoliza);
                return _Mapper.Map<List<PolizaDetalleDTO>>(lista);
            }
            catch
            {
                return new List<PolizaDetalleDTO>();
            }
        }

        public async Task<PolizaDetalleDTO> ObtenXId(int id)
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos(z => z.Id == id);
                return _Mapper.Map<PolizaDetalleDTO>(lista);
            }
            catch
            {
                return new PolizaDetalleDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(PolizaDetalleDTO poliza)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<PolizaDetalle>(poliza);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == modelo.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El detalle de poliza no existe";
                    return respuesta;
                }
                objetoEncontrado.IdPoliza = poliza.IdPoliza;
                objetoEncontrado.Concepto = poliza.Concepto;
                objetoEncontrado.Debe = poliza.Debe;
                objetoEncontrado.Haber = poliza.Haber;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Detalle de poliza editado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del detalle de poliza";
                return respuesta;
            }
        }
    }
}
