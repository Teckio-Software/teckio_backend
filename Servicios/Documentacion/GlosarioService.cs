using AutoMapper;
using ERP_TECKIO.DTO.Documentacion;
using ERP_TECKIO.Modelos.Documentacion;
using ERP_TECKIO.Servicios.Contratos.Documentacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Documentacion
{
    public class GlosarioService<T> : IGlosarioService<T> where T : DbContext
    {
        private readonly IGenericRepository<Glosario, T> _glosarioRepository;
        private readonly IMapper _mapper;
        public GlosarioService(IGenericRepository<Glosario, T> glosarioRepository, IMapper mapper)
        {
            _glosarioRepository = glosarioRepository;
            _mapper = mapper;
        }
        public async Task<GlosarioDTO> CrearYObtener(GlosarioDTO glosarioDTO)
        {
            var model = _mapper.Map<Glosario>(glosarioDTO);
            var glosario = await _glosarioRepository.Crear(model);

            return _mapper.Map<GlosarioDTO>(glosario);
        }

        public async Task<RespuestaDTO> Editar(GlosarioDTO glosarioDTO)
        {
            var respuesta = new RespuestaDTO();

            if (glosarioDTO.EsBase)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se puede editar un término base";
                return respuesta;
            }

            var glosario = await _glosarioRepository.Obtener(z => z.Id == glosarioDTO.Id);

            if (glosario == null)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El glosario no existe";
                return respuesta;
            }

            glosario.Termino = glosarioDTO.Termino;
            glosario.Definicion = glosarioDTO.Definicion;

            var editado = await _glosarioRepository.Editar(glosario);
            if (!editado)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se pudo editar";
                return respuesta;
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Glosario editado";

            return respuesta;

        }

        public async Task<RespuestaDTO> Eliminar(int IdGlosario)
        {
            var respuesta = new RespuestaDTO();

            var buscarGlosario = await _glosarioRepository.Obtener(z => z.Id == IdGlosario);

            if (buscarGlosario.EsBase)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se puede eliminar un término base";
                return respuesta;
            }

            if(buscarGlosario == null)
            {
                
                respuesta.Estatus = false;
                respuesta.Descripcion = "El término no existe";

                return respuesta;
            }
            
            var glosarioElimado  = await _glosarioRepository.Eliminar(buscarGlosario);

            if (!glosarioElimado)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se pudo eliminar";
                return respuesta;
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Glosario eliminado";
            return respuesta;
        }

        public async Task<List<GlosarioDTO>> ObtenerTodos()
        {
            var glosarios = await _glosarioRepository.ObtenerTodos();

            return _mapper.Map<List<GlosarioDTO>>(glosarios);
        }

        public async Task<GlosarioDTO> ObtenerXId(int IdGlosario)
        {
            var glosario = await _glosarioRepository.Obtener(z => z.Id == IdGlosario);

            return _mapper.Map<GlosarioDTO>(glosario);
        }
    }
}
