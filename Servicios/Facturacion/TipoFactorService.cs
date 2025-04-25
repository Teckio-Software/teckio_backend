using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class TipoFactorService<T> : ITipoFactorService<T> where T : DbContext
    {
        private readonly IGenericRepository<TipoFactor, T> _repository;
        private readonly IMapper _mapper;

        public TipoFactorService(
            IGenericRepository<TipoFactor, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TipoFactorDTO>> ObtenerTodos()
        {
            try
            {
                var query = await _repository.ObtenerTodos();
                return _mapper.Map<List<TipoFactorDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<TipoFactorDTO>();
            }
        }

        public async Task<TipoFactorDTO> ObtenerXId(int Id)
        {
            try
            {
                var query = await _repository.Obtener(z => z.Id == Id);
                return _mapper.Map<TipoFactorDTO>(query);
            }
            catch (Exception ex)
            {
                return new TipoFactorDTO();
            }
        }

        public async Task<TipoFactorDTO> CrearYObtener(TipoFactorDTO registro)
        {
            var respuesta = await _repository.Crear(_mapper.Map<TipoFactor>(registro));
            return _mapper.Map<TipoFactorDTO>(respuesta);
        }

        public async Task<RespuestaDTO> Editar(TipoFactorDTO registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenerXId(registro.Id);
                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                objetoEncontrado.Descripcion = registro.Descripcion;
                var modelo = _mapper.Map<TipoFactor>(objetoEncontrado);
                respuesta.Estatus = await _repository.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Factor editado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal al editar el factor";
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
                respuesta.Descripcion = "El almacén no existe";
                return respuesta;
            }
            var modelo = _mapper.Map<TipoFactor>(objetoEncontrado);
            respuesta.Estatus = await _repository.Eliminar(modelo);

            if (!respuesta.Estatus)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se pudo eliminars";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Tipo factor eliminado";
            return respuesta;
        }

        public async Task<TipoFactorDTO> ObtenerXDescripcion(string descripcion)
        {
            try
            {
                var query = await _repository.Obtener(z => z.Descripcion == descripcion);
                return _mapper.Map<TipoFactorDTO>(query);
            }
            catch (Exception ex)
            {
                return new TipoFactorDTO();
            }
        }
    }
}
