using AutoMapper;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class ConjuntoIndirectosService<T> : IConjuntoIndirectosService<T> where T : DbContext
    {
        private readonly IGenericRepository<ConjuntoIndirectos, T> _Repositorio;
        private readonly IMapper _Mapper;
        public ConjuntoIndirectosService(IGenericRepository<ConjuntoIndirectos, T> genericRepository, IMapper mapper) {
            _Repositorio = genericRepository;
            _Mapper = mapper;
        }
        public async Task<ConjuntoIndirectosDTO> CrearYObtener(ConjuntoIndirectosDTO objeto)
        {
            var respuesta = await _Repositorio.Crear(_Mapper.Map<ConjuntoIndirectos>(objeto));
            return _Mapper.Map<ConjuntoIndirectosDTO>(respuesta);
        }

        public async Task<RespuestaDTO> Editar(ConjuntoIndirectosDTO conjuntoIndirectos)
        {
            var respuesta = new RespuestaDTO();
            var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == conjuntoIndirectos.Id);
            if (objetoEncontrado.Id <= 0)
            {
                respuesta.Descripcion = "No se encontro el conjunto";
                respuesta.Estatus = false;
                return respuesta;
            }
            objetoEncontrado.TipoCalculo = conjuntoIndirectos.TipoCalculo;
            objetoEncontrado.Porcentaje = conjuntoIndirectos.Porcentaje;
            var resultado = await _Repositorio.Editar(objetoEncontrado);
            if (!resultado)
            {
                respuesta.Descripcion = "No se edito";
                respuesta.Estatus = false;
                return respuesta;
            }
            respuesta.Descripcion = "Se edito el conjunto";
            respuesta.Estatus = true;
            return respuesta;
        }

        public async Task<ConjuntoIndirectosDTO> ObtenerXId(int Id)
        {
            var objeto = await _Repositorio.Obtener(z => z.Id == Id);
            return _Mapper.Map<ConjuntoIndirectosDTO>(objeto);
        }

        public async Task<ConjuntoIndirectosDTO> ObtenerXIdProyecto(int IdProyecto)
        {
            var objeto = await _Repositorio.Obtener(z => z.IdProyecto == IdProyecto);
            return _Mapper.Map<ConjuntoIndirectosDTO>(objeto);
        }
    }
}
