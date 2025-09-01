using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class SalidaProduccionAlmacenService<T> : ISalidaProduccionAlmacenService<T> where T : DbContext
    {

        private readonly IGenericRepository<SalidaProduccionAlmacen, T> _repository;
        private readonly IMapper _mapper;

        public SalidaProduccionAlmacenService(IGenericRepository<SalidaProduccionAlmacen, T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(SalidaProduccionAlmacenDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = _mapper.Map<SalidaProduccionAlmacen>(parametro);
                var resultado = await _repository.Crear(objeto);
                if(resultado.Id > 0)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "Salida de producción al almacén creada correctamente";
                    return respuesta;
                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrio un error al crear la salida de producción al almacén";
                    return respuesta;
                }
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al crear la salida de producción al almacén";
                return respuesta;
            }
        }

        public async Task<SalidaProduccionAlmacenDTO> CrearYObtener(SalidaProduccionAlmacenDTO parametro)
        {
            try
            {
                var objeto = _mapper.Map<SalidaProduccionAlmacen>(parametro);
                var resultado = await _repository.Crear(objeto);
                if(resultado.Id > 0)
                {
                    return _mapper.Map<SalidaProduccionAlmacenDTO>(resultado);
                }
                else
                {
                    return new SalidaProduccionAlmacenDTO();
                }
            }
            catch
            {
                return new SalidaProduccionAlmacenDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(SalidaProduccionAlmacenDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(s=>s.Id== parametro.Id);
                if(objeto==null||objeto.Id<=0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontro la salida de producción al almacén";
                    return respuesta;
                }
                objeto.IdAlmacen = parametro.IdAlmacen;
                objeto.FechaEntrada = objeto.FechaEntrada;
                objeto.Recibio = parametro.Recibio;
                objeto.Observaciones = parametro.Observaciones;

                respuesta.Estatus = await _repository.Editar(objeto);

                if(respuesta.Estatus)
                {
                    respuesta.Descripcion = "Salida de producción al almacén editada correctamente";
                    return respuesta;
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al editar la salida de producción al almacén";
                    return respuesta;
                }

            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al editar la salida de producción al almacén";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(int parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(s => s.Id == parametro);
                if (objeto == null || objeto.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontro la salida de producción al almacén";
                    return respuesta;
                }

                respuesta.Estatus = await _repository.Eliminar(objeto);

                if (respuesta.Estatus)
                {
                    respuesta.Descripcion = "Salida de producción al almacén eliminada correctamente";
                    return respuesta;
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al eliminar la salida de producción al almacén";
                    return respuesta;
                }

            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al eliminar la salida de producción al almacén";
                return respuesta;
            }
        }

        public async Task<SalidaProduccionAlmacenDTO> ObtenerXId(int parametro)
        {
            try
            {
                var objeto = await _repository.Obtener(s => s.Id == parametro);
                if(objeto.Id > 0)
                {
                    return _mapper.Map<SalidaProduccionAlmacenDTO>(objeto);
                }
                else
                {
                    return new SalidaProduccionAlmacenDTO();
                }
            }
            catch
            {
                return new SalidaProduccionAlmacenDTO();
            }
        }
    }
}
