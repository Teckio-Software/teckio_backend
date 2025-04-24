using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class AcuseValidacionService<T> : IAcuseValidacionService<T> where T : DbContext
    {
        private readonly IGenericRepository<AcuseValidacion, T> _repository;
        private readonly IMapper _mapper;

        public AcuseValidacionService(
            IGenericRepository<AcuseValidacion, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<AcuseValidacionDTO>> ObtenTodos()
        {
            try
            {
                var query = await _repository.ObtenerTodos();
                return _mapper.Map<List<AcuseValidacionDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<AcuseValidacionDTO>();
            }
        }

        public async Task<AcuseValidacionDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _repository.Obtener(z => z.Id == Id);
                return _mapper.Map<AcuseValidacionDTO>(query);
            }
            catch (Exception ex)
            {
                return new AcuseValidacionDTO();
            }
        }

        public async Task<List<AcuseValidacionDTO>> ObtenXIdFactura(int IdFactura)
        {
            try
            {
                var query = await _repository.Obtener(z => z.IdFactura == IdFactura);
                return _mapper.Map<List<AcuseValidacionDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<AcuseValidacionDTO>();
            }
        }

        public async Task<List<AcuseValidacionDTO>> ObtenXIdUsuario(int IdUsuario)
        {
            try
            {
                var query = await _repository.Obtener(z => z.IdUsuario == IdUsuario);
                return _mapper.Map<List<AcuseValidacionDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<AcuseValidacionDTO>();
            }
        }

        public async Task<List<AcuseValidacionDTO>> ObtenXFolio(string Folio)
        {
            try
            {
                var query = await _repository.Obtener(z => z.Folio == Folio);
                return _mapper.Map<List<AcuseValidacionDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<AcuseValidacionDTO>();
            }
        }

        public async Task<AcuseValidacionDTO> CrearYObtener(AcuseValidacionDTO registro)
        {
            var respuesta = await _repository.Crear(_mapper.Map<AcuseValidacion>(registro));
            return _mapper.Map<AcuseValidacionDTO>(respuesta);
        }

        public async Task<RespuestaDTO> Editar(AcuseValidacionDTO registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenXId(registro.Id);
                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Acuse no existe";
                    return respuesta;
                }
                objetoEncontrado.IdFactura = registro.IdFactura;
                objetoEncontrado.IdUsuario = registro.IdUsuario;
                objetoEncontrado.Folio = registro.Folio;
                objetoEncontrado.Estatus = registro.Estatus;
                objetoEncontrado.Fecha = registro.Fecha;
                var modelo = _mapper.Map<AcuseValidacion>(objetoEncontrado);
                respuesta.Estatus = await _repository.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Acuse editado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal al editar Acuse";
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
                respuesta.Descripcion = "Acuse no existe";
                return respuesta;
            }
            var modelo = _mapper.Map<AcuseValidacion>(objetoEncontrado);
            respuesta.Estatus = await _repository.Eliminar(modelo);

            if (!respuesta.Estatus)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se pudo eliminars";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Acuse eliminado";
            return respuesta;
        }
    }
}
