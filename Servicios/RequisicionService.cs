using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class RequisicionService<T> : IRequisicionService<T> where T : DbContext
    {
        private readonly IGenericRepository<Requisicion, T> _Repositorio;
        //private readonly PROCOMIContext _Context;
        private readonly IMapper _Mapper;

        public RequisicionService(IGenericRepository<Requisicion, T> repositorio
            //, PROCOMIContext context
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            //_Context = context;
            _Mapper = mapper;
        }
        public async Task<RespuestaDTO> Crear(RequisicionDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                if (string.IsNullOrEmpty(parametro.NoRequisicion) || parametro.IdProyecto <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No hay un proyecto asociado";
                    return respuesta;
                }
                parametro.FechaRegistro = DateTime.Now;
                parametro.EstatusRequisicion = 1;
                parametro.EstatusInsumosComprados = 0;
                parametro.EstatusInsumosSurtIdos = 0;
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Requisicion>(parametro));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Requisición creada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la creación de la requisición";
                return respuesta;
            }
        }

        public async Task<RequisicionDTO> CrearYObtener(RequisicionDTO parametro)
        {
            try
            {
                parametro.FechaRegistro = DateTime.Now;
                parametro.EstatusRequisicion = 1;
                parametro.EstatusInsumosComprados = 0;
                parametro.EstatusInsumosSurtIdos = 0;
                var creacion = await _Repositorio.Crear(_Mapper.Map<Requisicion>(parametro));

                if (creacion.Id == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _Mapper.Map<RequisicionDTO>(creacion);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                return new RequisicionDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(RequisicionDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == parametro.Id);
                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La requisición no existe";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusRequisicion != 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se puede editar si esta capturado";
                    return respuesta;
                }
                objetoEncontrado.IdProyecto = parametro.IdProyecto;
                objetoEncontrado.PersonaSolicitante = parametro.PersonaSolicitante;
                objetoEncontrado.Observaciones = parametro.Observaciones;
                objetoEncontrado.Residente = parametro.Residente;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Requisición editada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de la requisición";
                return respuesta;
            }
        }
        public async Task<RespuestaDTO> ActualizarEstatusAutorizar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenXId(Id);
                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La requisición no existe";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusRequisicion != 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se puede autorizar si esta capturado";
                    return respuesta;
                }
                objetoEncontrado.EstatusRequisicion = 2;
                var modelo = _Mapper.Map<Requisicion>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo autorizar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Requisición autorizada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la autorización de la requisición";
                return respuesta;
            }
        }
        public async Task<RespuestaDTO> ActualizarEstatusRemoverAutorizar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenXId(Id);
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La requisición no existe";
                    return respuesta;
                }
                //Con el estatus nos damos cuenta si solo esta autorizada o si ya tiene cotizaciones asociadas
                if (objetoEncontrado.EstatusRequisicion != 2)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se puede remover la autorización si esta autorizado";
                    return respuesta;
                }
                objetoEncontrado.EstatusRequisicion = 1;
                var modelo = _Mapper.Map<Requisicion>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó remover la autorización";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Autorización de la requisición removIda";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la revocación de la autorización de la requisición";
                return respuesta;
            }
        }
        public async Task<RespuestaDTO> ActualizarEstatusCotizar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var requisicionDTO = await ObtenXId(Id);
                if (requisicionDTO.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La requisición no existe";
                    return respuesta;
                }
                //Con el estatus nos damos cuenta si solo esta autorizada o si ya tiene cotizaciones asociadas
                if (requisicionDTO.EstatusRequisicion != 2)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se puede cotizar las requisiciones autorizadas";
                    return respuesta;
                }
                requisicionDTO.EstatusRequisicion = 3;
                var modelo = _Mapper.Map<Requisicion>(requisicionDTO);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó cotizar la requisición";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Requisición cotizada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la cotización de la requisición";
                return respuesta;
            }
        }
        //Checar
        public async Task<RespuestaDTO> ActualizarEstatusComprar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var requisicionDTO = await ObtenXId(Id);
                if (requisicionDTO.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La requisición no existe";
                    return respuesta;
                }
                if (requisicionDTO.EstatusRequisicion == 4)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La requisición esta cancelada";
                    return respuesta;
                }
                if (requisicionDTO.EstatusRequisicion == 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La requisición debe estar autorizada";
                    return respuesta;
                }
                requisicionDTO.EstatusInsumosComprados = 1;
                var modelo = _Mapper.Map<Requisicion>(requisicionDTO);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó comprar la requisición";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Requisición comprada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la compra de la requisición";
                return respuesta;
            }
        }
        //Checar
        public async Task<RespuestaDTO> ActualizarEstatusEntradaAlmacen(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var requisicionDTO = await ObtenXId(Id);
                if (requisicionDTO.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La requisición no existe";
                    return respuesta;
                }
                if (requisicionDTO.EstatusRequisicion == 4)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La requisición esta cancelada";
                    return respuesta;
                }
                if (requisicionDTO.EstatusRequisicion == 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La requisición debe estar comprada";
                    return respuesta;
                }
                requisicionDTO.EstatusRequisicion = 3;
                var modelo = _Mapper.Map<Requisicion>(requisicionDTO);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó cancelar la requisición";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Requisición cancelada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la cancelación de la requisición";
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
                    respuesta.Descripcion = "La requisición no existe";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusRequisicion == 5)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Hay ordenes de compra asociadas";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusRequisicion == 3)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Hay cotizaciones asociadas";
                    return respuesta;
                }
                objetoEncontrado.EstatusRequisicion = 4;
                var modelo = _Mapper.Map<Requisicion>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó cancelar la requisición";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Requisición cancelada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la cancelación de la requisición";
                return respuesta;
            }
        }
        public async Task<List<RequisicionDTO>> ObtenTodos()
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<RequisicionDTO>>(query);
            }
            catch
            {
                return new List<RequisicionDTO>();
            }
        }
        /// <summary>
        /// Para las requisiciones por proyecto
        /// </summary>
        /// <param name="idProyecto">Identificador único del proyecto</param>
        /// <returns>Lista de requisiciones</returns>
        public async Task<List<RequisicionDTO>> ObtenXIdProyecto(int idProyecto)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdProyecto == idProyecto);
                return _Mapper.Map<List<RequisicionDTO>>(query);
            }
            catch
            {
                return new List<RequisicionDTO>();
            }
        }
        public async Task<RequisicionDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<RequisicionDTO>(query);
            }
            catch
            {
                return new RequisicionDTO();
            }
        }



        public async Task<int> CompruebaEstatusRequisicion(int Id)
        {
            //Debemos de comprobar si hay compras, cotizaciones y entradas de almacén en cada uno de los insumos
            var requisicionDTO = await ObtenXId(Id);
            if (requisicionDTO.Id <= 0)
                return -1;

            return requisicionDTO.EstatusRequisicion;
        }

        public async Task<int> CompruebaEstatusRequisicionInsumosComprados(int Id)
        {
            //Debemos de comprobar si hay compras, cotizaciones y entradas de almacén en cada uno de los insumos
            var requisicionDTO = await ObtenXId(Id);
            if (requisicionDTO.Id <= 0)
                return -1;

            return requisicionDTO.EstatusInsumosComprados;
        }

        public async Task<int> CompruebaEstatusRequisicionInsumosSurtidos(int Id)
        {
            //Debemos de comprobar si hay compras, cotizaciones y entradas de almacén en cada uno de los insumos
            var requisicionDTO = await ObtenXId(Id);
            if (requisicionDTO.Id <= 0)
                return -1;

            return requisicionDTO.EstatusInsumosSurtIdos;
        }

        public async Task<RespuestaDTO> ActualizarRequisicionInsumosSurtidos(int Id, int estatus)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == Id);
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La requisicion no existe";
                    return respuesta;
                }
                objetoEncontrado.EstatusInsumosSurtidos = estatus;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó actualizar el estatus insumos surtidos";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Requisicion actualizada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en el cambio de estatus de la requisicion";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> ActualizarRequisicionInsumosComprados(int Id, int estatus)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == Id);
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La requisicion no existe";
                    return respuesta;
                }
                objetoEncontrado.EstatusInsumosComprados = estatus;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó actualizar el estatus insumos comprados";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Requisicion actualizada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en el cambio de estatus de la requisicion";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(RequisicionDTO modelo)
        {
            var respuesta = new RespuestaDTO();
            var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == modelo.Id);
            if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "La requisición no existe";
                return respuesta;
            }
            if (objetoEncontrado.EstatusRequisicion != 1)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Solo se puede eliminar si esta capturado";
                return respuesta;
            }
            var elimianr = await _Repositorio.Eliminar(objetoEncontrado);
            if (!elimianr) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se pudo eliminar";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Requisición elimianda";
            return respuesta;
        }
    }
}
