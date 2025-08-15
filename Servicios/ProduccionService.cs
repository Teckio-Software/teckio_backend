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
                //Vlidaciones del estatus
                switch (objeto.Estatus)
                {
                    case 0:
                        if (!(modelo.Estatus == 0 || modelo.Estatus == 1 || modelo.Estatus == 2))
                        {
                            respuesta.Descripcion = "La producción apenas esta capturada, solo puede autorizarse o cancelarse";
                            respuesta.Estatus = false;
                            return respuesta;
                        }
                        break;
                    case 1:
                        if (!(modelo.Estatus == 1 || modelo.Estatus == 2 || modelo.Estatus == 3))
                        {
                            respuesta.Descripcion = "La producción esta autorizada, solo puede pasar a proceso o cancelarse";
                            respuesta.Estatus = false;
                            return respuesta;
                        }
                        break;
                    case 2:
                        //Provisionalmente agregue un condicional pero en teoría no debería ser editable
                        if (modelo.Estatus != 2)
                        {
                            respuesta.Descripcion = "La producción esta cancelada, ya no puede editarse";
                            respuesta.Estatus = false;
                            return respuesta;
                        }
                        break;
                    case 3:
                        if (!(modelo.Estatus == 3 || modelo.Estatus == 4))
                        {
                            respuesta.Descripcion = "La producción esta en proceso, solo puede pasar a terminada";
                            respuesta.Estatus = false;
                            return respuesta;
                        }
                        break;
                    case 4:
                        if (!(modelo.Estatus == 4 || modelo.Estatus == 5 || modelo.Estatus == 6))
                        {
                            respuesta.Descripcion = "La producción esta terminada, solo se puede aprobar la calidad o rechazarse";
                            respuesta.Estatus = false;
                            return respuesta;
                        }
                        break;
                    case 5:
                        //Provisionalmente agregue un condicional pero en teoría no debería ser editable
                        if (modelo.Estatus != 5)
                        {
                            respuesta.Descripcion = "La producción esta en calidad aprobada, ya no puede editarse";
                            respuesta.Estatus = false;
                            return respuesta;
                        }
                        break;
                    case 6:
                        //Provisionalmente agregue un condicional pero en teoría no debería ser editable
                        if (modelo.Estatus != 6)
                        {
                            respuesta.Descripcion = "La producción esta rechzada, ya no puede editarse";
                            respuesta.Estatus = false;
                            return respuesta;
                        }
                        break;
                }
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

        public async Task<RespuestaDTO> Eliminar(int id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _repository.Obtener(p => p.Id == id);
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

        public async Task<ProduccionDTO> ObtenerXId(int id)
        {
            try
            {
                var resultado = await _repository.Obtener(p => p.Id == id);
                return _mapper.Map<ProduccionDTO>(resultado);
            }
            catch
            {
                return new ProduccionDTO();
            }
        }

        //public async Task<List<ProduccionDTO>> ObtenerTodos()
        //{
        //    try
        //    {
        //        var lista = await _repository.ObtenerTodos();
        //        if (lista.Count > 0)
        //        {

        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch
        //    {
        //        return new <List<ProduccionDTO>();
        //    }
        //}
    }
}