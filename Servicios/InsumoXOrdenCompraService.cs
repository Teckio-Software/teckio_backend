using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class InsumoXOrdenCompraService<T> : IInsumoXOrdenCompraService<T> where T : DbContext
    {
        private readonly IGenericRepository<InsumoXOrdenCompra, T> _Repositorio;
        private readonly IInsumoService<T> _insumos;
        private readonly IOrdenCompraService<T> _ordencompra;
        //private readonly IOrdenCompraService _OrdenCompraService;
        //private readonly PROCOMIContext _Context;
        private readonly IMapper _Mapper;

        public InsumoXOrdenCompraService(
            IGenericRepository<InsumoXOrdenCompra, T> repositorio
            , IInsumoService<T> insumos
            , IOrdenCompraService<T> ordencompra
            //, IOrdenCompraService OrdenCompraService
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _insumos = insumos;
            _ordencompra = ordencompra;
            //_OrdenCompraService = OrdenCompraService;
            //_Context = context;
            _Mapper = mapper;
        }


        public async Task<List<InsumoXOrdenCompraDTO>> ObtenTodos()
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<InsumoXOrdenCompraDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<InsumoXOrdenCompraDTO>();
            }
        }
        /// <summary>
        /// Obtiene los insumos por el Id de la orden de compra
        /// </summary>
        /// <param name="IdOrdenCompra">Identificador único de la orden de compra</param>
        /// <returns>Lista de <see cref="InsumoXOrdenCompraDTO"/></returns>
        public async Task<List<InsumoXOrdenCompraDTO>> ObtenXIdOrdenCompra(int? IdOrdenCompra)
        {
            try
            {
                List<InsumoXOrdenCompraDTO> lista = new List<InsumoXOrdenCompraDTO> ();
                var query = await _Repositorio.ObtenerTodos(z => z.IdOrdenCompra == IdOrdenCompra);
                return _Mapper.Map<List<InsumoXOrdenCompraDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<InsumoXOrdenCompraDTO>();
            }
        }
        /// <summary>
        /// Para uso interno
        /// </summary>
        /// <param name="IdCotizacion">Identificador de la cotización</param>
        /// <returns></returns>
        public async Task<List<InsumoXOrdenCompraDTO>> ObtenInsumosOrdenCompraXIdInsumoCotizacion(int IdInsumoCotizacion)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdInsumoXcotizacion == IdInsumoCotizacion);
                return _Mapper.Map<List<InsumoXOrdenCompraDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<InsumoXOrdenCompraDTO>();
            }
        }

        public async Task<InsumoXOrdenCompraDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<InsumoXOrdenCompraDTO>(query);
            }
            catch (Exception ex)
            {
                return new InsumoXOrdenCompraDTO();
            }
        }

        public async Task<RespuestaDTO> ActualizarEstatusFacturado(int Id)
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
                //1 = capturada, 2 = facturada, 3 = cancelada
                objetoEncontrado.EstatusInsumoOrdenCompra = 2;
                var modelo = _Mapper.Map<InsumoXOrdenCompra>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo cancelar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Insumo de la orden de compra cancelada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la cancelación del insumo";
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
                //1 = capturada, 2 = facturada, 3 = cancelada
                objetoEncontrado.EstatusInsumoOrdenCompra = 3;
                var modelo = _Mapper.Map<InsumoXOrdenCompra>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo cancelar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Insumo de la orden de compra cancelada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la cancelación del insumo";
                return respuesta;
            }
        }

        public async Task<bool> CrearLista(List<InsumoXOrdenCompraDTO> parametro)
        {
            var objetosMapaeados = _Mapper.Map<List<InsumoXOrdenCompra>>(parametro);
            return await _Repositorio.CrearMultiple(objetosMapaeados);
        }

        public Task<RespuestaDTO> Crear(InsumoXOrdenCompraCreacionDTO modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<RespuestaDTO> ActualizarCantidadRecibida(int IdInsumo, int idOrdenCompra, decimal CantidadRecibida)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var objetoEncontrado = await _Repositorio.Obtener(z => z.IdOrdenCompra == idOrdenCompra && z.IdInsumo == IdInsumo);
            if (objetoEncontrado.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "El insumo no existe";
                return respuesta;
            }
            if(objetoEncontrado.CantidadRecibida == null)
            {
                objetoEncontrado.CantidadRecibida = 0;
            }
            var nuevaCantidadRecibida = objetoEncontrado.CantidadRecibida + CantidadRecibida;
            objetoEncontrado.CantidadRecibida = nuevaCantidadRecibida;
            if(nuevaCantidadRecibida > 0 && nuevaCantidadRecibida < objetoEncontrado.Cantidad)
            {
                objetoEncontrado.EstatusInsumoOrdenCompra = 2;
            }if (nuevaCantidadRecibida >= objetoEncontrado.Cantidad) {
                objetoEncontrado.EstatusInsumoOrdenCompra = 3;
            }
            respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
            if (!respuesta.Estatus)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se actualizó el insumo";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Insumo surtido";
            return respuesta;
        }

        public async Task<InsumoXOrdenCompraDTO> CrearYObtener(InsumoXOrdenCompraDTO modelo)
        {
            try
            {
                var creacion = await _Repositorio.Crear(_Mapper.Map<InsumoXOrdenCompra>(modelo));

                if (creacion.Id == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _Mapper.Map<InsumoXOrdenCompraDTO>(creacion);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                return new InsumoXOrdenCompraDTO();
            }
        }
    }
}

