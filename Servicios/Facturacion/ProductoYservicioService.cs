using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Modelos.Facturacion;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class ProductoYservicio<T> : IProductoYservicioService<T> where T : DbContext
    {
        private readonly IGenericRepository<ProductoYservicio, T> _repository;
        private readonly IMapper _mapper;

        public ProductoYservicio(
            IGenericRepository<ProductoYservicio, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductoYservicioDTO>> ObtenerTodos()
        {
            try
            {
                var query = await _repository.ObtenerTodos();
                return _mapper.Map<List<ProductoYservicioDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<ProductoYservicioDTO>();
            }
        }

        public async Task<ProductoYservicioDTO> ObtenerXId(int Id)
        {
            try
            {
                var query = await _repository.Obtener(z => z.Id == Id);
                return _mapper.Map<ProductoYservicioDTO>(query);
            }
            catch (Exception ex)
            {
                return new ProductoYservicioDTO();
            }
        }

        public async Task<ProductoYservicioDTO> CrearYObtener(ProductoYservicioDTO registro)
        {
            var respuesta = await _repository.Crear(_mapper.Map<ProductoYservicio>(registro));
            return _mapper.Map<ProductoYservicioDTO>(respuesta);
        }

        public async Task<RespuestaDTO> Editar(ProductoYservicioDTO registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenerXId(registro.Id);
                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Prducto y servicio no existe";
                    return respuesta;
                }
                objetoEncontrado.Codigo = registro.Codigo;
                objetoEncontrado.Descripcion = registro.Descripcion;
                objetoEncontrado.IdUnidad = registro.IdUnidad;
                objetoEncontrado.IdProductoYservicioSat = registro.IdProductoYservicioSat;
                objetoEncontrado.IdUnidadSat = registro.IdUnidadSat;

                var modelo = _mapper.Map<ProductoYservicio>(objetoEncontrado);
                respuesta.Estatus = await _repository.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Producto y servicio editado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal al editar producto y servicio";
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
                respuesta.Descripcion = "Producto y servicio no existe";
                return respuesta;
            }
            var modelo = _mapper.Map<ProductoYservicio>(objetoEncontrado);
            respuesta.Estatus = await _repository.Eliminar(modelo);

            if (!respuesta.Estatus)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se pudo eliminar";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Producto y servicio eliminado";
            return respuesta;
        }
    }
}
