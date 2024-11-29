using AutoMapper;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class IndirectosXConceptoService<T> : IIndirectosXConceptoService<T> where T : DbContext
    {
        private readonly IGenericRepository<IndirectosXConcepto, T> _Repositorio;
        private readonly IMapper _Mapper;
        public IndirectosXConceptoService(IGenericRepository<IndirectosXConcepto, T> genericRepository, IMapper mapper) {
            _Repositorio = genericRepository;
            _Mapper = mapper;
        }
        public async Task<RespuestaDTO> Crear(IndirectosXConceptoDTO objeto)
        {
            var respuesta = new RespuestaDTO();
            var modelo = await _Repositorio.Crear(_Mapper.Map<IndirectosXConcepto>(objeto));
            if (modelo.Id <= 0)
            {
                respuesta.Descripcion = "No se creo";
                respuesta.Estatus = false;
                return respuesta;
            }
            respuesta.Descripcion = "Se creo el indirecto";
            respuesta.Estatus = true;
            return respuesta;
        }

        public async Task<IndirectosXConceptoDTO> CrearYObtener(IndirectosXConceptoDTO objeto)
        {
            var respuesta = await _Repositorio.Crear(_Mapper.Map<IndirectosXConcepto>(objeto));
            return _Mapper.Map<IndirectosXConceptoDTO>(respuesta);
        }

        public async Task<RespuestaDTO> Editar(IndirectosXConceptoDTO objeto)
        {
            var respuesta = new RespuestaDTO();
            var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == objeto.Id);
            if (objetoEncontrado.Id <= 0)
            {
                respuesta.Descripcion = "No se encontro el indirecto";
                respuesta.Estatus = false;
                return respuesta;
            }
            objetoEncontrado.Codigo = objeto.Codigo;
            objetoEncontrado.Descripcion = objeto.Descripcion;
            objetoEncontrado.Porcentaje = objeto.Porcentaje;
            var resultado = await _Repositorio.Editar(objetoEncontrado);
            if (!resultado)
            {
                respuesta.Descripcion = "No se edito";
                respuesta.Estatus = false;
                return respuesta;
            }
            respuesta.Descripcion = "Se edito el indirecto";
            respuesta.Estatus = true;
            return respuesta;
        }

        public async Task<RespuestaDTO> Eliminar(IndirectosXConceptoDTO objeto)
        {
            var respuesta = new RespuestaDTO();
            var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == objeto.Id);
            if (objetoEncontrado.Id <= 0)
            {
                respuesta.Descripcion = "No se encontro el indirecto";
                respuesta.Estatus = false;
                return respuesta;
            }
            var eliminar = await _Repositorio.Eliminar(objetoEncontrado);
            if (!eliminar)
            {
                respuesta.Descripcion = "No se elimino";
                respuesta.Estatus = false;
                return respuesta;
            }
            respuesta.Descripcion = "Se elimino el indirecto";
            respuesta.Estatus = true;
            return respuesta;
        }

        public async Task<IndirectosXConceptoDTO> ObtenerXId(int idIndirecto)
        {
            var objeto = await _Repositorio.Obtener(z => z.Id == idIndirecto);
            return _Mapper.Map<IndirectosXConceptoDTO>(objeto);
        }

        public async Task<List<IndirectosXConceptoDTO>> ObtenerXIdConcepto(int IdConcepto)
        {
            var lista = await _Repositorio.ObtenerTodos(z => z.IdConcepto == IdConcepto);
            return _Mapper.Map<List<IndirectosXConceptoDTO>>(lista);
        }
    }
}
