using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class ProductosXSalidaAlmacenService<T> : IProductosXSalidaProduccionAlmacenService<T> where T : DbContext
    {
        private readonly IGenericRepository<ProductosXsalidaProduccionAlmacen, T> _repository;
        private readonly IMapper _mapper;

        public ProductosXSalidaAlmacenService(IGenericRepository<ProductosXsalidaProduccionAlmacen, T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(ProductosXsalidaProduccionAlmacenDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = _mapper.Map<ProductosXsalidaProduccionAlmacen>(parametro);
                var resultado = await _repository.Crear(objeto);
                if(resultado.Id > 0)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "Producto por salida creado correctamente";
                    return respuesta;
                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrio un error al crear el producto por salida";
                    return respuesta;
                }
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al crear el producto por salida";
                return respuesta;
            }
        }

        public async Task<ProductosXsalidaProduccionAlmacenDTO> CrearYObtener(ProductosXsalidaProduccionAlmacenDTO parametro)
        {
            try
            {
                var objeto = _mapper.Map<ProductosXsalidaProduccionAlmacen>(parametro);
                var resultado = await _repository.Crear(objeto);
                if (resultado.Id > 0)
                {
                    return _mapper.Map<ProductosXsalidaProduccionAlmacenDTO>(resultado);
                }
                else
                {
                    return new ProductosXsalidaProduccionAlmacenDTO();
                }
            }
            catch
            {
                return new ProductosXsalidaProduccionAlmacenDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(ProductosXsalidaProduccionAlmacenDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(p=>p.Id== parametro.Id);
                if(objeto == null || objeto.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontro el producto por salida";
                    return respuesta;
                }

                objeto.IdSalidaProduccionAlmacen = parametro.IdSalidaProduccionAlmacen;
                objeto.IdProductoYservicio = parametro.IdProductoYservicio;
                objeto.Cantidad = parametro.Cantidad;
                objeto.TipoOrigen = parametro.TipoOrigen;
                respuesta.Estatus = await _repository.Editar(objeto);
                if(respuesta.Estatus)
                {
                    respuesta.Descripcion = "Producto por salida editado correctamente";
                    return respuesta;
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al editar el producto por salida";
                    return respuesta;
                }
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al editar el producto por salida";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(int parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(p => p.Id == parametro);
                if (objeto == null || objeto.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontro el producto por salida";
                    return respuesta;
                }
                respuesta.Estatus = await _repository.Eliminar(objeto);
                if (respuesta.Estatus)
                {
                    respuesta.Descripcion = "Producto por salida eliminado correctamente";
                    return respuesta;
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al eliminar el producto por salida";
                    return respuesta;
                }
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al eliminar el producto por salida";
                return respuesta;
            }
        }

        public async Task<List<ProductosXsalidaProduccionAlmacenDTO>> ObtenerXSalida(int idSalida)
        {
            try
            {
                var lista = await _repository.ObtenerTodos(p => p.IdSalidaProduccionAlmacen == idSalida);
                if(lista.Count > 0)
                {
                    return _mapper.Map<List<ProductosXsalidaProduccionAlmacenDTO>>(lista);
                }
                else
                {
                    return new List<ProductosXsalidaProduccionAlmacenDTO>();
                }
            }
            catch
            {
                return new List<ProductosXsalidaProduccionAlmacenDTO>();
            }
        }
    }
}
