using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class CotizacionService<T> : ICotizacionService<T> where T : DbContext
    {
        private readonly IGenericRepository<Cotizacion, T> _Repositorio;
        //private readonly PROCOMIContext _Context;
        private readonly IMapper _Mapper;

        public CotizacionService(
            IGenericRepository<Cotizacion, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }
        public async Task<RespuestaDTO> Editar(CotizacionDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == parametro.Id);
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La cotización no existe";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusCotizacion != 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se pueden editar cotización capturadas";
                    return respuesta;
                }
                objetoEncontrado.IdContratista = parametro.IdContratista;
                objetoEncontrado.Observaciones = parametro.Observaciones;
                //objetoEncontrado.PersonaAutorizo = parametro.PersonaAutorizo;
                //objetoEncontrado.PersonaCompra = parametro.PersonaCompra;
                //objetoEncontrado.ImporteSinIva = parametro.ImporteSinIva;
                //objetoEncontrado.MontoDescuento = parametro.MontoDescuento;
                //objetoEncontrado.ImporteConIva = parametro.ImporteConIva;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Cotización editada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de la cotización";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> ActualizarEstatusAutorizar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenXId(Id);
                if (objetoEncontrado == new CotizacionDTO() || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La cotización no existe";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusCotizacion != 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se puede autorizar si esta capturado";
                    return respuesta;
                }
                objetoEncontrado.EstatusCotizacion = 2;
                var modelo = _Mapper.Map<Cotizacion>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo autorizar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Cotización autorizada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la autorización de la cotización";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> ActualizarEstatusCancelar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenXId(Id);
                if (objetoEncontrado == new CotizacionDTO() || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La cotización no existe";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusCotizacion == 3 || objetoEncontrado.EstatusInsumosComprados == 3)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Hay ordenes de compra relacionadas a esta cotización";
                    return respuesta;
                }
                objetoEncontrado.EstatusCotizacion = 4;
                var modelo = _Mapper.Map<Cotizacion>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo autorizar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Cotización autorizada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la autorización de la cotización";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> ActualizarEstatusRemoverAutorizar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenXId(Id);
                if (objetoEncontrado == new CotizacionDTO() || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La cotización no existe";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusCotizacion == 3 || objetoEncontrado.EstatusInsumosComprados == 3)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Hay ordenes de compra relacionadas a esta cotización";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusCotizacion != 2)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se puede remover la autorización de las requisiciones con estatus de autorizada";
                    return respuesta;
                }
                var modelo = _Mapper.Map<Cotizacion>(objetoEncontrado);
                objetoEncontrado.EstatusCotizacion = 1;
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó remover la autorización";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Autorización de la cotización removida";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la revocación de la autorización de la cotización";
                return respuesta;
            }
        }

        public async Task<List<CotizacionDTO>> ObtenXIdProyecto(int IdProyecto)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto);
                return _Mapper.Map<List<CotizacionDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<CotizacionDTO>();
            }
        }
        public async Task<List<CotizacionDTO>> ObtenXIdRequision(int IdRequisicion)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdRequisicion == IdRequisicion);
                return _Mapper.Map<List<CotizacionDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<CotizacionDTO>();
            }
        }

        public async Task<CotizacionDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<CotizacionDTO>(query);
            }
            catch (Exception ex)
            {
                return new CotizacionDTO();
            }
        }
        /// <summary>
        /// Checar
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<RespuestaDTO> ActualizarEstatusComprar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenXId(Id);
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La cotización no existe";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusInsumosComprados == 3)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La cotización ya ha sido comprada";
                    return respuesta;
                }
                objetoEncontrado.EstatusCotizacion = 3;
                var modelo = _Mapper.Map<Cotizacion>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó comprar la cotización";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Cotización comprada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la compra de la cotización";
                return respuesta;
            }
        }

        public async Task<CotizacionDTO> CrearYObtener(CotizacionDTO parametro)
        {
            try
            {
                parametro.FechaRegistro = DateTime.Now;
                var creacion = await _Repositorio.Crear(_Mapper.Map<Cotizacion>(parametro));

                if (creacion.Id == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _Mapper.Map<CotizacionDTO>(creacion);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                return new CotizacionDTO();
            }
        }

        public Task<RespuestaDTO> Crear(CotizacionCreacionDTO modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CotizacionDTO>> ObtenXIdContratista(int IdContratista)
        {
            var cotizacion =  await _Repositorio.ObtenerTodos(z => z.IdContratista == IdContratista);
            return _Mapper.Map<List<CotizacionDTO>>(cotizacion);
        }

        public async Task<RespuestaDTO> ActualizarEstatusCotizacion(int Id, int estatus)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == Id);
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La cotizacion no existe";
                    return respuesta;
                }
                objetoEncontrado.EstatusCotizacion = estatus;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó actualizar el estatus de la cotizacion";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "cotizacion actualizada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en el cambio de estatus de la cotizacion";
                return respuesta;
            }
        }
    }
}
