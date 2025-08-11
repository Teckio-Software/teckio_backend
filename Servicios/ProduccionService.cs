using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class ProduccionService<T> : IProduccionService<T> where T : DbContext
    {
        private readonly IGenericRepository<Produccion, T> _repository;
        private readonly IMapper _mapper;
        public ProduccionService(
            IGenericRepository<Produccion, T> repository,
            IMapper mapper
            ) {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<RespuestaDTO> Crear(ProduccionDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = _mapper.Map<Produccion>(modelo);
                var resultado = await _repository.Crear(objeto);
                if (resultado.Id > 0)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "Producción creada exitosamente";

                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ocurrio un error al intentar crear la producción";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Descripcion = "Ocurrio un error al intentar crear la producción";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<ProduccionDTO> CrearYObtener(ProduccionDTO modelo)
        {
            try
            {
                var objeto = _mapper.Map<Produccion>(modelo);
                var resultado = await _repository.Crear(objeto);
                if (resultado.Id > 0)
                {
                    return _mapper.Map<ProduccionDTO>(resultado);

                }
                else
                {
                    return new ProduccionDTO();
                }
            }
            catch
            {
                return new ProduccionDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(ProduccionDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(p => p.Id == modelo.Id);
                if (objeto.Id <= 0)
                {
                    respuesta.Descripcion = "No se encontro la producción";
                    respuesta.Estatus = false;
                    return respuesta;
                }
                objeto.IdProductoYservicio = modelo.IdProductoYservicio;
                //Probablemente no deberia ser editable pero provisionalmente la agregue.
                objeto.FechaProduccion = modelo.FechaProduccion;
                //Probablemente no deberia ser editable pero provisionalmente lo agregue.
                objeto.Produjo = modelo.Produjo;
                objeto.Cantidad = modelo.Cantidad;
                objeto.Observaciones = modelo.Observaciones;
                objeto.Estatus = modelo.Estatus;
                objeto.Autorizo = modelo.Autorizo;
                respuesta.Estatus = await _repository.Editar(objeto);
                if (respuesta.Estatus)
                {
                    respuesta.Descripcion = "Producción editada exitosamente";
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al intentar editar la producción";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Descripcion = "Ocurrio un error al intentar editar la producción";
                respuesta.Estatus = false;
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(ProduccionDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(p => p.Id == modelo.Id);
                if (objeto.Id <= 0)
                {
                    respuesta.Descripcion = "No se encontro la producción";
                    respuesta.Estatus = false;
                    return respuesta;
                }
                respuesta.Estatus = await _repository.Eliminar(objeto);
                if (respuesta.Estatus)
                {
                    respuesta.Descripcion = "Producción eliminada exitosamente";
                }
                else
                {
                    respuesta.Descripcion = "Ocurrio un error al intentar eliminar la producción";
                }
                return respuesta;
            }
            catch
            {
                respuesta.Descripcion = "Ocurrio un error al intentar eliminar la producción";
                respuesta.Estatus = false;
                return respuesta;
            }
        }
    }
}