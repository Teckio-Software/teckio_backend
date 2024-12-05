using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class InsumoXCotizacionService<T> : IInsumoXCotizacionService<T> where T : DbContext
    {
        private readonly IGenericRepository<InsumoXCotizacion, T> _Repositorio;
        //private readonly PROCOMIContext _Context;
        private readonly IMapper _Mapper;

        public InsumoXCotizacionService(
            IGenericRepository<InsumoXCotizacion, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            //_Context = context;
            _Mapper = mapper;
        }

        public async Task<List<InsumoXCotizacionDTO>> ObtenXIdCotizacion(int IdCotizacion)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdCotizacion == IdCotizacion);
                return _Mapper.Map<List<InsumoXCotizacionDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<InsumoXCotizacionDTO>();
            }
        }

        public async Task<InsumoXCotizacionDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<InsumoXCotizacionDTO>(query);
            }
            catch (Exception ex)
            {
                return new InsumoXCotizacionDTO();
            }
        }

        public async Task<RespuestaDTO> ActualizarEstatusAutorizar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == Id);
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusInsumoCotizacion != 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se puede autorizar si esta capturado";
                    return respuesta;
                }
                //1 = capturada, 2 = autorizada, 3 = comprada, 4 = cancelada
                objetoEncontrado.EstatusInsumoCotizacion = 2;
                //var modelo = _Mapper.Map<InsumoXCotizacion>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo autorizar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Insumo de la requisición autorizada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la autorización del insumo";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> ActualizarEstatusRemoverAutorizar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == Id);
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusInsumoCotizacion == 3)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ordenes de compra relacionados al insumo";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusInsumoCotizacion != 2)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se puede remover la autorización con estatus de autorizada";
                    return respuesta;
                }
                //1 = capturada, 2 = autorizada, 3 = comprada, 4 = cancelada
                objetoEncontrado.EstatusInsumoCotizacion = 1;
                var modelo = _Mapper.Map<InsumoXCotizacion>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó remover la autorización";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Autorización removida";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal al remover la autorización del insumo";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> ActualizarEstatusCancelar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenXId(Id);
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusInsumoCotizacion == 3)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Ordenes de compra relacionados al insumo";
                    return respuesta;
                }
                //1 = capturada, 2 = autorizada, 3 = comprada, 4 = cancelada
                objetoEncontrado.EstatusInsumoCotizacion = 4;
                var modelo = _Mapper.Map<InsumoXCotizacion>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó remover la autorización";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Insumo de la cotización cancelada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la autorización del insumo";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> ActualizaEstatusComprado(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var objetoEncontrado = await _Repositorio.Obtener(z=>z.Id == Id);
            if (objetoEncontrado.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El insumo no existe";
                return respuesta;
            }
            if (objetoEncontrado.EstatusInsumoCotizacion <= 1 || objetoEncontrado.EstatusInsumoCotizacion >= 3)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Solo se puede editar insumos con estatus de capturado";
                return respuesta;
            }
            //1 = capturada, 2 = autorizada, 3 = comprada, 4 = cancelada
            objetoEncontrado.EstatusInsumoCotizacion = 3;
             //modelo = _Mapper.Map<InsumoXCotizacion>(objetoEncontrado);
            respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
            if (!respuesta.Estatus)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se pudó comprar el insumo de la cotización";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Insumo comprado";
            return respuesta;
        }

        public async Task<RespuestaDTO> VerificaEstatusAutorizado(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            respuesta.Estatus = false;
            try
            {
                var objetoEncontrado = await ObtenXId(Id);
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusInsumoCotizacion <= 1 || objetoEncontrado.EstatusInsumoCotizacion >= 3)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se puede comprar insumos con estatus de autorizados";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Insumo autorizado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la compra del insumo en la cotización";
                return respuesta;
            }
        }

        

        public async Task<bool> CrearLista(List<InsumoXCotizacionDTO> parametro)
        {
            var objetosMapaeados = _Mapper.Map<List<InsumoXCotizacion>>(parametro);
            return await _Repositorio.CrearMultiple(objetosMapaeados);
        }

        public Task<RespuestaDTO> Crear(InsumoXCotizacionCreacionDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<InsumoXCotizacionDTO> CrearYObtener(InsumoXCotizacionCreacionDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<int> CompruebaEstatusCotizacionInsumosComprados(int IdCotizacion)
        {
            throw new NotImplementedException();
        }

        public async Task<RespuestaDTO> Editar(InsumoXCotizacionDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
            if (objetoEncontrado == null)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El insumo no existe";
                return respuesta;
            }
            objetoEncontrado.Cantidad = modelo.Cantidad;
            objetoEncontrado.PrecioUnitario = modelo.PrecioUnitario;
            objetoEncontrado.Descuento = modelo.Descuento;
            objetoEncontrado.ImporteSinIva = modelo.Cantidad * modelo.PrecioUnitario;
            objetoEncontrado.ImporteTotal = modelo.ImporteTotal;
            var actualizacion =  await _Repositorio.Editar(objetoEncontrado);
            if (!actualizacion) {
                respuesta.Estatus = false;
                return respuesta;
            }
            respuesta.Estatus = true;
            return respuesta;
        }

        public async Task<List<InsumoXCotizacionDTO>> ObtenTodos()
        {
            var lista =  await _Repositorio.ObtenerTodos();
            return _Mapper.Map<List<InsumoXCotizacionDTO>>(lista);
        }

        public async Task<InsumoXCotizacionDTO> CrearYObtener(InsumoXCotizacionDTO parametro)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<InsumoXCotizacion>(parametro));
                if (objetoCreado.Id == 0)
                    throw new TaskCanceledException("No se pudó crear");
                return _Mapper.Map<InsumoXCotizacionDTO>(objetoCreado);
            }
            catch
            {
                return new InsumoXCotizacionDTO();
            }
        }
    }
}
