using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class ExistenciaProductoAlmacenService<T> : IExistenciaProductoAlmacenService<T> where T : DbContext
    {
        private readonly IGenericRepository<ExistenciaProductosAlmacen, T> _repository;
        private readonly IMapper _mapper;
        public ExistenciaProductoAlmacenService(
            IGenericRepository<ExistenciaProductosAlmacen, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(ExistenciaProductoAlmacenDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var existente = await _repository.Obtener(e => e.IdProductoYservicio == modelo.IdProductoYservicio && e.IdAlmacen == modelo.IdAlmacen);
                if (existente.Id > 0)
                {
                    existente.Cantidad = existente.Cantidad + modelo.Cantidad;
                    respuesta.Estatus = await _repository.Editar(existente);
                    if (respuesta.Estatus)
                    {
                        respuesta.Descripcion = "Existencia registrada exitosamente";
                    }
                    else
                    {
                        respuesta.Descripcion = "Ocurrio un error al registrar la existencia";
                    }
                    return respuesta;
                }
                var objeto = _mapper.Map<ExistenciaProductosAlmacen>(modelo);
                var resultado = await _repository.Crear(objeto);
                if (resultado.Id > 0)
                {
                    respuesta.Descripcion = "Existencia producto almacen creada exitosamente";
                    respuesta.Estatus = false;
                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrio un error al intentar crear la existencia producto almacen";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar crear la existencia producto almacen";
                return respuesta;
            }
        }

        public async Task<ExistenciaProductoAlmacenDTO> CrearYObtener(ExistenciaProductoAlmacenDTO modelo)
        {
            try
            {
                var existente = await _repository.Obtener(e => e.IdProductoYservicio == modelo.IdProductoYservicio && e.IdAlmacen == modelo.IdAlmacen);
                if (existente.Id > 0)
                {
                    existente.Cantidad = existente.Cantidad + modelo.Cantidad;
                    var resultEdit = await _repository.Editar(existente);
                    if (resultEdit)
                    {
                        return _mapper.Map<ExistenciaProductoAlmacenDTO>(existente);
                    }
                    else
                    {
                        return new ExistenciaProductoAlmacenDTO();
                    }
                }
                var objeto = _mapper.Map<ExistenciaProductosAlmacen>(modelo);
                var resultado = await _repository.Crear(objeto);
                if (resultado.Id > 0)
                {
                    return _mapper.Map<ExistenciaProductoAlmacenDTO>(resultado);
                }
                else
                {
                return new ExistenciaProductoAlmacenDTO();
                }
            }
            catch
            {
                return new ExistenciaProductoAlmacenDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(ExistenciaProductoAlmacenDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(e=>e.Id==modelo.Id);
                if (objeto.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontró la existencia producto almacen";
                    return respuesta;
                }
                objeto.IdProductoYservicio = modelo.IdProductoYservicio;
                objeto.Cantidad = modelo.Cantidad;
                objeto.IdAlmacen = modelo.IdAlmacen;
                var resultado = await _repository.Editar(objeto);
                if (resultado)
                {
                    respuesta.Descripcion = "Existencia producto almacen editada exitosamente";
                    respuesta.Estatus = false;
                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrio un error al intentar editar la existencia producto almacen";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar editar la existencia producto almacen";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(ExistenciaProductoAlmacenDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(e => e.Id == modelo.Id);
                if (objeto.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontró la existencia producto almacen";
                    return respuesta;
                }
                var resultado = await _repository.Eliminar(objeto);
                if (resultado)
                {
                    respuesta.Descripcion = "Existencia producto almacen eliminada exitosamente";
                    respuesta.Estatus = false;
                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrio un error al intentar eliminar la existencia producto almacen";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar eliminar la existencia producto almacen";
                return respuesta;
            }
        }

        public async Task<ExistenciaProductoAlmacenDTO> ObtenerExistencia(int idAlmacen, int idProdYSer)
        {
            try
            {
                var objeto = await _repository.Obtener(e => e.IdAlmacen == idAlmacen && e.IdProductoYservicio == idProdYSer);
                if (objeto.Id>0)
                {
                    return _mapper.Map<ExistenciaProductoAlmacenDTO>(objeto);
                }
                else
                {
                    return new ExistenciaProductoAlmacenDTO();
                }
            }
            catch
            {
                return new ExistenciaProductoAlmacenDTO();
            }
        }
    }
}
