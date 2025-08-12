using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos.Facturaion;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class InsumoXProductoYServicioService<T> : IInsumoXProductoYServicioService<T> where T : DbContext
    {
        private readonly IGenericRepository<InsumoxProductoYservicio, T> _repository;

        private readonly IMapper _mapper;

        public InsumoXProductoYServicioService(IGenericRepository<InsumoxProductoYservicio, T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(InsumoXProductoYServicioDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = _mapper.Map<InsumoxProductoYservicio>(parametro);
                var resultado = await _repository.Crear(objeto);
                if (resultado.Id > 0)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "Insumo Por Producto y servicio creado exitosamente";
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al intentar crear el insumo X Producto y servicio";
                    respuesta.Estatus = false;
                }
                return respuesta;
            }
            catch
            {
                respuesta.Descripcion = "Ocurrio un error al intentar crear el insumo X Producto y servicio";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Editar(InsumoXProductoYServicioDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(i => i.Id == parametro.Id);
                if(objeto.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontro el insumo por producto y servicio";
                    return respuesta;
                }
                objeto.IdProductoYservicio = parametro.IdProductoYservicio;
                objeto.IdInsumo = parametro.IdInsumo;
                objeto.Cantidad = parametro.Cantidad;
                respuesta.Estatus = await _repository.Editar(objeto);
                if (respuesta.Estatus)
                {
                    respuesta.Descripcion = "Insumo por producto y servicio editado exitosamente";
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al intentar editar el insumo X Producto y servicio";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Descripcion = "Ocurrio un error al intentar editar el insumo X Producto y servicio";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(InsumoXProductoYServicioDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(i => i.Id == parametro.Id);
                if (objeto.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontro el insumo por producto y servicio";
                    return respuesta;
                }
                respuesta.Estatus = await _repository.Eliminar(objeto);
                if (respuesta.Estatus)
                {
                    respuesta.Descripcion = "Insumo por producto y servicio eliminado exitosamente";
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al intentar eliminar el insumo X Producto y servicio";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Descripcion = "Ocurrio un error al intentar eliminar el insumo X Producto y servicio";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<List<InsumoXProductoYServicioDTO>> ObtenerPorIdPrdYSer(int id)
        {
            try
            {
                var lista = await _repository.ObtenerTodos(i => i.IdProductoYservicio == id);
                if (lista.Count > 0)
                {
                    return _mapper.Map<List<InsumoXProductoYServicioDTO>>(lista);
                }
                else
                {
                    return new List<InsumoXProductoYServicioDTO>();
                }
            }
            catch
            {
                return new List<InsumoXProductoYServicioDTO>();
            }
        }
    }
}
