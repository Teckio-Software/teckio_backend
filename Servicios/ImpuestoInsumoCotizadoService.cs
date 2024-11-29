using AutoMapper;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class ImpuestoInsumoCotizadoService<T> : IImpuestoInsumoCotizadoService<T> where T : DbContext
    {
        private readonly IGenericRepository<ImpuestoInsumoCotizado, T> _Repositorio;
        private readonly IMapper _Mapper;

        public ImpuestoInsumoCotizadoService(IGenericRepository<ImpuestoInsumoCotizado, T> repositorio
, IMapper mapper) {
            _Repositorio = repositorio; _Mapper = mapper;
        }
        public async Task<RespuestaDTO> Crear(ImpuestoInsumoCotizadoCreacionDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var objetoCreado = await _Repositorio.Crear(_Mapper.Map<ImpuestoInsumoCotizado>(parametro));
            if (objetoCreado.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se pudó crear";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Impuesto creado";
            return respuesta;
        }

        public async Task<RespuestaDTO> Editar(ImpuestoInsumoCotizadoDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == parametro.Id);
            if (objetoEncontrado.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se encontro";
                return respuesta;
            }
            objetoEncontrado.Porcentaje = parametro.Porcentaje;
            objetoEncontrado.Importe = parametro.Importe;
            var resultado = await _Repositorio.Editar(objetoEncontrado);
            if (!resultado) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Impuesto no editado";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Impuesto editado";
            return respuesta;
        }

        public async Task<RespuestaDTO> Eliminar(ImpuestoInsumoCotizadoDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == parametro.Id);
            if (objetoEncontrado.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Impuesto no encontrado";
                return respuesta;
            }
            var eliminarObjeto = await _Repositorio.Eliminar(objetoEncontrado);
            if (!eliminarObjeto) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Impuesto no eliminado";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Impuesto eliminado";
            return respuesta;
        }

        public async Task<List<ImpuestoInsumoCotizadoDTO>> ObtenerXIdInsumoCotizado(int idInsumoCotizado)
        {
            var objetosEncontrados = await _Repositorio.ObtenerTodos(z => z.IdInsumoCotizado == idInsumoCotizado);
            if (objetosEncontrados.Count() <= 0) {
                return new List<ImpuestoInsumoCotizadoDTO>();
            }
            return _Mapper.Map<List<ImpuestoInsumoCotizadoDTO>>(objetosEncontrados);
        }
    }
}
