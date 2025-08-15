using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class ProductoXEntradaProduccionAlmacenService<T> : IProductoXEntradaProduccionAlmacenService<T> where T : DbContext
    {
        private readonly IGenericRepository<ProductosXentradaProduccionAlmacen, T> _repository;
        private readonly IMapper _mapper;
        public ProductoXEntradaProduccionAlmacenService(
            IGenericRepository<ProductosXentradaProduccionAlmacen, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(ProductosXEntradaProduccionAlmacenDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = _mapper.Map<ProductosXentradaProduccionAlmacen>(modelo);
                var resultado = await _repository.Crear(objeto);
                if (resultado.Id > 0)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "Detalle de entrada creado exitosamente";
                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrio un error al intentar crear el detalle de entrada";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar crear el detalle de entrada";
                return respuesta;
            }
        }

        public async Task<ProductosXEntradaProduccionAlmacenDTO> CrearYObtener(ProductosXEntradaProduccionAlmacenDTO modelo)
        {
            try
            {
                var objeto = _mapper.Map<ProductosXentradaProduccionAlmacen>(modelo);
                var resultado = await _repository.Crear(objeto);
                if (resultado.Id > 0)
                {
                    return _mapper.Map<ProductosXEntradaProduccionAlmacenDTO>(objeto);
                }
                else
                {
                    return new ProductosXEntradaProduccionAlmacenDTO();
                }
            }
            catch
            {
                return new ProductosXEntradaProduccionAlmacenDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(ProductosXEntradaProduccionAlmacenDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(p => p.Id == modelo.Id);
                if (objeto.Id <= 0)
                {
                    respuesta.Descripcion = "No se encontro el detalle de entrada";
                    respuesta.Estatus = false;
                    return respuesta;
                }
                objeto.IdEntradaProduccionAlmacen = modelo.IdEntradaProduccionAlmacen;
                objeto.IdProductoYservicio = modelo.IdProductoYservicio;
                objeto.Cantidad = modelo.Cantidad;
                objeto.TipoOrigen = modelo.TipoOrigen;
                var resultado = await _repository.Editar(objeto);
                if (resultado)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "Detalle de entrada editado exitosamente";
                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrio un error al intentar editar el detalle de entrada";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar editar el detalle de entrada";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(ProductosXEntradaProduccionAlmacenDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(p => p.Id == modelo.Id);
                if (objeto.Id <= 0)
                {
                    respuesta.Descripcion = "No se encontro el detalle de entrada";
                    respuesta.Estatus = false;
                    return respuesta;
                }
                var resultado = await _repository.Eliminar(objeto);
                if (resultado)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "Detalle de entrada eliminado exitosamente";
                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrio un error al intentar eliminar el detalle de entrada";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar eliminar el detalle de entrada";
                return respuesta;
            }
        }
    }
}
