using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class EntradaProduccionAlmacenService<T> : IEntradaProduccionAlmacenService<T> where T : DbContext
    {
        private readonly IGenericRepository<EntradaProduccionAlmacen, T> _repository;
        private readonly IMapper _mapper;
        public EntradaProduccionAlmacenService(
            IGenericRepository<EntradaProduccionAlmacen, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(EntradaProduccionAlmacenDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = _mapper.Map<EntradaProduccionAlmacen>(modelo);
                var resultado = await _repository.Crear(objeto);
                if (resultado.Id > 0)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "Entrada procuccion almacen creada exitosamente";
                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrio un problema al intentar crear la entrada producto almacen";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un problema al intentar crear la entrada producto almacen";
                return respuesta;
            }
        }

        public async Task<EntradaProduccionAlmacenDTO> CrearYObtener(EntradaProduccionAlmacenDTO modelo)
        {
            try
            {
                var objeto = _mapper.Map<EntradaProduccionAlmacen>(modelo);
                var resultado = await _repository.Crear(objeto);
                if (resultado.Id > 0)
                {
                    return _mapper.Map<EntradaProduccionAlmacenDTO>(resultado);
                }
                else
                {
                    return new EntradaProduccionAlmacenDTO();
                }
            }
            catch
            {
                return new EntradaProduccionAlmacenDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(EntradaProduccionAlmacenDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(e=>e.Id==modelo.Id);
                if (objeto.Id <= 0)
                {
                    respuesta.Descripcion = "No se encontro la entrada produccion almacen";
                    respuesta.Estatus = false;
                    return respuesta;
                }
                objeto.IdAlmacen = modelo.IdAlmacen;
                objeto.FechaEntrada = modelo.FechaEntrada;
                objeto.Recibio = modelo.Recibio;
                objeto.Observaciones = modelo.Observaciones;
                var resultado = await _repository.Editar(objeto);
                if (resultado)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "Entrada procuccion almacen editada exitosamente";
                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrio un problema al intentar editar la entrada producto almacen";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un problema al intentar editar la entrada producto almacen";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(EntradaProduccionAlmacenDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(e => e.Id == modelo.Id);
                if (objeto.Id <= 0)
                {
                    respuesta.Descripcion = "No se encontro la entrada produccion almacen";
                    respuesta.Estatus = false;
                    return respuesta;
                }
                var resultado = await _repository.Eliminar(objeto);
                if (resultado)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "Entrada procuccion almacen eliminada exitosamente";
                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrio un problema al intentar eliminar la entrada producto almacen";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un problema al intentar eliminar la entrada producto almacen";
                return respuesta;
            }
        }
    }
}
