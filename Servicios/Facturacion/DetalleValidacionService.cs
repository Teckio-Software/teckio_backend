using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class DetalleValidacionService<T> : IDetalleValidacionService<T> where T : DbContext
    {
        private readonly IGenericRepository<DetalleValidacion, T> _repository;
        private readonly IMapper _mapper;

        public DetalleValidacionService(
            IGenericRepository<DetalleValidacion, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<DetalleValidacionDTO>> ObtenTodos()
        {
            try
            {
                var query = await _repository.ObtenerTodos();
                return _mapper.Map<List<DetalleValidacionDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<DetalleValidacionDTO>();
            }
        }

        public async Task<DetalleValidacionDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _repository.Obtener(z => z.Id == Id);
                return _mapper.Map<DetalleValidacionDTO>(query);
            }
            catch (Exception ex)
            {
                return new DetalleValidacionDTO();
            }
        }

        public async Task<List<DetalleValidacionDTO>> ObtenSoloCancelados()
        {
            try
            {
                var query = await _repository.ObtenerTodos(z => z.CodigoValidacion == "032");
                return _mapper.Map<List<DetalleValidacionDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<DetalleValidacionDTO>();
            }
        }

        public async Task<List<DetalleValidacionDTO>> ObtenXIdAcuseValidacion(int IdAcuseValidacion)
        {
            try
            {
                var query = await _repository.ObtenerTodos(z => z.IdAcuseValidacion == IdAcuseValidacion);
                return _mapper.Map<List<DetalleValidacionDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<DetalleValidacionDTO>();
            }
        }

        public async Task<List<DetalleValidacionDTO>> ObtenXIdCatalogoValidacion(string CodigoCatalogoValidacion)
        {
            try
            {
                var query = await _repository.ObtenerTodos(z => z.CodigoValidacion == CodigoCatalogoValidacion);
                return _mapper.Map<List<DetalleValidacionDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<DetalleValidacionDTO>();
            }
        }

        public async Task<DetalleValidacionDTO> CrearYObtener(DetalleValidacionDTO registro)
        {
            var respuesta = await _repository.Crear(_mapper.Map<DetalleValidacion>(registro));
            return _mapper.Map<DetalleValidacionDTO>(respuesta);
        }

        public async Task<RespuestaDTO> Editar(DetalleValidacionDTO registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenerXId(registro.Id);
                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Detalle validacion no existe";
                    return respuesta;
                }
                objetoEncontrado.IdAcuseValidacion = registro.IdAcuseValidacion;
                objetoEncontrado.CodigoValidacion = registro.CodigoValidacion;
                var modelo = _mapper.Map<DetalleValidacion>(objetoEncontrado);
                respuesta.Estatus = await _repository.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Detalle validacion editado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal al editar Detalle validacion";
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
                respuesta.Descripcion = "Detalle validacion no existe";
                return respuesta;
            }
            var modelo = _mapper.Map<DetalleValidacion>(objetoEncontrado);
            respuesta.Estatus = await _repository.Eliminar(modelo);

            if (!respuesta.Estatus)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se pudo eliminars";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Detalle validacion eliminado";
            return respuesta;
        }
    }
}
