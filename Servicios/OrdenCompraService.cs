using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class OrdenCompraService<T> : IOrdenCompraService<T> where T : DbContext
    {
        private readonly IGenericRepository<OrdenCompra, T> _Repositorio;
        //private readonly PROCOMIContext _Context;
        private readonly IMapper _Mapper;

        public OrdenCompraService(
            IGenericRepository<OrdenCompra, T> repositorio
            //, PROCOMIContext context
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            //_Context = context;
            _Mapper = mapper;
        }
        public async Task<List<OrdenCompraDTO>> ObtenXIdProyecto(int IdProyecto)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto);
                return _Mapper.Map<List<OrdenCompraDTO>>(query);
            }
            catch
            {
                return new List<OrdenCompraDTO>();
            }
        }

        public async Task<OrdenCompraDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<OrdenCompraDTO>(query);
            }
            catch
            {
                return new OrdenCompraDTO();
            }
        }

        public async Task<List<OrdenCompraDTO>> ObtenXIdCotizacion(int IdCotizacion)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdCotizacion == IdCotizacion);
                return _Mapper.Map<List<OrdenCompraDTO>>(query);
            }
            catch
            {
                return new List<OrdenCompraDTO>();
            }
        }

        public async Task<List<OrdenCompraDTO>> ObtenXIdRequisicion(int IdRequisicion)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdRequisicion == IdRequisicion);
                return _Mapper.Map<List<OrdenCompraDTO>>(query);
            }
            catch
            {
                return new List<OrdenCompraDTO>();
            }
        }

        //public async Task<RespuestaDTO> Crear(OrdenCompraCreacionDTO modelo)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        if (string.IsNullOrEmpty(modelo.NoOrdenCompra) || modelo.IdProyecto <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No hay una cotización asociada";
        //            return respuesta;
        //        }
        //        modelo.Estatus = 1;
        //        modelo.FechaRegistro = DateTime.Now;
        //        var objetoCreado = await _Repositorio.Crear(_Mapper.Map<OrdenCompra>(modelo));
        //        if (objetoCreado.Id <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No se pudó crear";
        //            return respuesta;
        //        }
        //        respuesta.Estatus = true;
        //        respuesta.Descripcion = "Orden de compra creada";
        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.Estatus = false;
        //        respuesta.Descripcion = "Algo salió mal en la creación de la orden de compra";
        //        return respuesta;
        //    }
        //}

        //public async Task<OrdenCompraDTO> CrearYObtener(OrdenCompraCreacionDTO modelo)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        var ordenesCompra = await _Repositorio.ObtenerTodos(z => z.IdCotizacion == modelo.IdCotizacion);
        //        if (string.IsNullOrEmpty(modelo.NoOrdenCompra) || modelo.IdProyecto <= 0)
        //        {
        //            return new OrdenCompraDTO();
        //        }
        //        modelo.Estatus = 1;
        //        modelo.FechaRegistro = DateTime.Now;
        //        var objetoCreado = await _Repositorio.Crear(_Mapper.Map<OrdenCompra>(modelo));
        //        if (objetoCreado.Id <= 0)
        //        {
        //            throw new TaskCanceledException("No se pudo crear");
        //        }
        //        return _Mapper.Map<OrdenCompraDTO>(objetoCreado);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new OrdenCompraDTO();
        //    }
        //}

        public async Task<RespuestaDTO> Editar(OrdenCompraDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == parametro.Id);
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La orden de compra no existe";
                    return respuesta;
                }
                if (objetoEncontrado.Estatus != 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La orden de compra no puede estar facturada o cancelada";
                    return respuesta;
                }
                //objetoEncontrado.Elaboro = parametro.Elaboro;
                objetoEncontrado.Chofer = parametro.Chofer;
                objetoEncontrado.Observaciones = parametro.Observaciones;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Orden de compra editada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de la orden de compra";
                return respuesta;
            }
        }
        /// <summary>
        /// Ponemos un estado de cancelación
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<RespuestaDTO> ActualizarEstatusCancelar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenXId(Id);
                if (objetoEncontrado == new OrdenCompraDTO() || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La orden de compra no existe";
                    return respuesta;
                }
                //Orden compra facturada
                if (objetoEncontrado.Estatus == 2)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Hay una o más facturas asociadas a esta compra";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusAlmacen > 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Hay una o más entradas de almacén asociadas a la orden de compra";
                    return respuesta;
                }
                objetoEncontrado.Estatus = 3;
                var modelo = _Mapper.Map<OrdenCompra>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó cancelar la orden de compra";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Orden de compra cancelada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la cancelación de la orden de compra";
                return respuesta;
            }
        }



        public async Task<RespuestaDTO> ActualizarEstatusFacturado(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenXId(Id);
                if (objetoEncontrado == new OrdenCompraDTO() || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La orden de compra no existe";
                    return respuesta;
                }
                //Orden compra facturada
                if (objetoEncontrado.Estatus == 3)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "La orden de compra esta cancelada";
                    return respuesta;
                }
                objetoEncontrado.Estatus = 2;
                var modelo = _Mapper.Map<OrdenCompra>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó actualizar el estatus de facturación de la orden de compra";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Orden de compra actualizada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en el cambio de estatus facturación de la orden de compra";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> ActualizarEstatusEntradaAlmacen(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenXId(Id);
                if (objetoEncontrado == new OrdenCompraDTO() || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La orden de compra no existe";
                    return respuesta;
                }
                //Orden compra facturada
                if (objetoEncontrado.Estatus == 3)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "La orden de compra esta cancelada";
                    return respuesta;
                }
                objetoEncontrado.EstatusAlmacen = 1;
                var modelo = _Mapper.Map<OrdenCompra>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó actualizar el estatus de almacén de la orden de compra";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Orden de compra actualizada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en el cambio de estatus a almacén de la orden de compra";
                return respuesta;
            }
        }

        public Task<RespuestaDTO> Crear(OrdenCompraCreacionDTO modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<OrdenCompraDTO> CrearYObtener(OrdenCompraDTO parametro) 
        {
            try
            {
                var creacion = await _Repositorio.Crear(_Mapper.Map<OrdenCompra>(parametro));

                if (creacion.Id == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _Mapper.Map<OrdenCompraDTO>(creacion);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                return new OrdenCompraDTO();
            }
        }

        public Task<OrdenCompraDTO> CrearYObtener(OrdenCompraCreacionDTO modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrdenCompraDTO>> ObtenXIdContratista(int IdContratista)
        {
            var query = await _Repositorio.ObtenerTodos(z => z.IdContratista == IdContratista);
            return _Mapper.Map<List<OrdenCompraDTO>>(query);
        }

        public async Task<RespuestaDTO> ActualizaOrdenCompraSurtidos(int Id, int estatus)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(z=> z.Id == Id);
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La orden de compra no existe";
                    return respuesta;
                }
                objetoEncontrado.EstatusInsumosSurtidos = estatus;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó actualizar el estatus de la orden de compra";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Orden de compra actualizada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en el cambio de estatus de la orden de compra";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Pagar(OrdenCompraDTO modelo)
        {
            var respuesta = new RespuestaDTO();
            var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == modelo.Id);
            if (objetoEncontrado.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "La orden de compra no existe";
                return respuesta;
            }
            objetoEncontrado.EstatusSaldado = modelo.EstatusSaldado;
            objetoEncontrado.TotalSaldado = modelo.TotalSaldado;
            respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
            if (!respuesta.Estatus)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se pudo saldar";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Orden de compra editada";
            return respuesta;
        }
    }
}
