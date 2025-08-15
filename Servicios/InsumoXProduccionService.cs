using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class InsumoXProduccionService<T> : IInsumoXProduccionService<T> where T : DbContext
    {
        private readonly IGenericRepository<InsumoXproduccion, T> _repository;
        private readonly IMapper _mapper;
        public InsumoXProduccionService(
            IGenericRepository<InsumoXproduccion, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<RespuestaDTO> Crear(InsumoXProduccionDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = _mapper.Map<InsumoXproduccion>(modelo);
                var resultado = await _repository.Crear(objeto);
                if (resultado.Id > 0)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "Insumo por producción creado exitosamente";
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al intentar crear el insumo por producción";
                    respuesta.Estatus = false;
                }
                return respuesta;
            }catch(Exception ex )
            {
                respuesta.Descripcion = "Ocurrio un error al intentar crear el insumo por producción";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<InsumoXProduccionDTO> CrearYObtener(InsumoXProduccionDTO modelo)
        {
            try
            {
                var objeto = _mapper.Map<InsumoXproduccion>(modelo);
                var resultado = await _repository.Crear(objeto);
                if (resultado.Id > 0)
                {
                    return _mapper.Map<InsumoXProduccionDTO>(resultado);
                }
                else
                {
                    return new InsumoXProduccionDTO();

                }
            }
            catch (Exception ex)
            {
                return new InsumoXProduccionDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(InsumoXProduccionDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(i=>i.Id==modelo.Id);
                if (objeto.Id <= 0)
                {
                    respuesta.Descripcion = "No se encontro el insumo por producto y servicio";
                    respuesta.Estatus = false;
                    return respuesta;
                }
                objeto.Cantidad = modelo.Cantidad;
                var resultado = await _repository.Editar(objeto);
                if (resultado)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "Insumo por producción editado exitosamente";
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al intentar editar el insumo por producción";
                    respuesta.Estatus = false;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Descripcion = "Ocurrio un error al intentar editar el insumo por producción";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(int id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(i => i.Id == id);
                if (objeto.Id <= 0)
                {
                    respuesta.Descripcion = "No se encontro el insumo por producto y servicio";
                    respuesta.Estatus = false;
                    return respuesta;
                }

                var resultado = await _repository.Eliminar(objeto);
                if (resultado)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "Insumo por producción eliminado exitosamente";
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al intentar eliminar el insumo por producción";
                    respuesta.Estatus = false;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Descripcion = "Ocurrio un error al intentar eliminar el insumo por producción";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<InsumoXProduccionDTO> ObtenerXId(int id)
        {
            try
            {
                var resultado = await _repository.Obtener(i => i.Id == id);
                if (resultado.Id > 0)
                {
                    return _mapper.Map<InsumoXProduccionDTO>(resultado);
                }
                else
                {
                    return new InsumoXProduccionDTO();
                }
            }
            catch
            {
                return new InsumoXProduccionDTO();
            }
        }

        public async Task<List<InsumoXProduccionDTO>> ObtenerXProduccion(int id)
        {
            try
            {
                var lista = await _repository.ObtenerTodos(i => i.IdProduccion == id);
                if (lista.Count > 0)
                {
                    return _mapper.Map<List<InsumoXProduccionDTO>>(lista);
                }
                else
                {
                    return new List<InsumoXProduccionDTO>();
                }
            }
            catch
            {
                return new List<InsumoXProduccionDTO>();
            }
        }
    }
}
