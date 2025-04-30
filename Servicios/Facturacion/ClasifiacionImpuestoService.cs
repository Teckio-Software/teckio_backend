using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class ClasificacionImpuestoService<T> : IClasificacionImpuestoService<T> where T : DbContext
    {
        private readonly IGenericRepository<ClasificacionImpuesto, T> _repository;
        private readonly IMapper _mapper;

        public ClasificacionImpuestoService(
            IGenericRepository<ClasificacionImpuesto, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ClasificacionImpuestoDTO>> ObtenerTodos()
        {
            try
            {
                var query = await _repository.ObtenerTodos();
                return _mapper.Map<List<ClasificacionImpuestoDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<ClasificacionImpuestoDTO>();
            }
        }

        public async Task<ClasificacionImpuestoDTO> ObtenerXId(int Id)
        {
            try
            {
                var query = await _repository.Obtener(z => z.Id == Id);
                return _mapper.Map<ClasificacionImpuestoDTO>(query);
            }
            catch (Exception ex)
            {
                return new ClasificacionImpuestoDTO();
            }
        }

        public async Task<ClasificacionImpuestoDTO> CrearYObtener(ClasificacionImpuestoDTO registro)
        {
            var respuesta = await _repository.Crear(_mapper.Map<ClasificacionImpuesto>(registro));
            return _mapper.Map<ClasificacionImpuestoDTO>(respuesta);
        }

        public async Task<RespuestaDTO> Editar(ClasificacionImpuestoDTO registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenerXId(registro.Id);
                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Clasificacion de impuesto no existe";
                    return respuesta;
                }
                objetoEncontrado.TipoClasificacionImpuesto = registro.TipoClasificacionImpuesto;
                var modelo = _mapper.Map<ClasificacionImpuesto>(objetoEncontrado);
                respuesta.Estatus = await _repository.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Clasificacion de impuesto editado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal al editar Clasificacion de impuesto";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var objetoEncontrado = await ObtenerXId(Id);

            if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Clasificacion de impuesto no existe";
                return respuesta;
            }
            var modelo = _mapper.Map<ClasificacionImpuesto>(objetoEncontrado);
            respuesta.Estatus = await _repository.Eliminar(modelo);

            if (!respuesta.Estatus)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se pudo eliminars";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Clasificacion de impuesto eliminado";
            return respuesta;
        }
    }
}
