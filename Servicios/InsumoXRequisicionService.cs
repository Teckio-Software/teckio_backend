using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class InsumoXRequisicionService<T> : IInsumoXRequisicionService<T> where T : DbContext
    {
        private readonly IGenericRepository<InsumoXRequisicion, T> _Repositorio;
        //private readonly PROCOMIContext _Context;
        private readonly IMapper _Mapper;

        public InsumoXRequisicionService(
            IGenericRepository<InsumoXRequisicion, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            //_Context = context;
            _Mapper = mapper;
        }
        public async Task<RespuestaDTO> Crear(InsumoXRequisicionDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<InsumoXRequisicion>(parametro));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Insumo creado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la creación de la requisición";
                return respuesta;
            }
        }

        public async Task<InsumoXRequisicionDTO> CrearYObtener(InsumoXRequisicionCreacionDTO parametro)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<InsumoXRequisicion>(parametro));
                if (objetoCreado.Id <= 0)
                {
                    throw new TaskCanceledException("No se pudó crear");
                }
                return _Mapper.Map<InsumoXRequisicionDTO>(objetoCreado);
            }
            catch (Exception ex)
            {
                return new InsumoXRequisicionDTO();
            }
        }
        public async Task<List<InsumoXRequisicionDTO>> ObtenTodos()
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<InsumoXRequisicionDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<InsumoXRequisicionDTO>();
            }
        }
        public async Task<List<InsumoXRequisicionDTO>> ObtenXIdRequisicion(int IdRequisicion)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdRequisicion == IdRequisicion);
                return _Mapper.Map<List<InsumoXRequisicionDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<InsumoXRequisicionDTO>();
            }
        }

        public async Task<InsumoXRequisicionDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<InsumoXRequisicionDTO>(query);
            }
            catch (Exception ex)
            {
                return new InsumoXRequisicionDTO();
            }
        }
        public async Task<RespuestaDTO> ActualizarEstatusAutorizar(int Id)
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
                if (objetoEncontrado.EstatusInsumoRequisicion != 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se puede autorizar si esta capturado";
                    return respuesta;
                }
                //1 = capturada, 2 = autorizada, 3 = comprada, 4 = cancelada
                objetoEncontrado.EstatusInsumoRequisicion = 2;
                var modelo = _Mapper.Map<InsumoXRequisicion>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
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
            catch (Exception ex)
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
                var objetoEncontrado = await ObtenXId(Id);
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                //Si hay una compra ya realizada te rebota
                if (objetoEncontrado.EstatusInsumoComprado >= 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Hay compras asociadas al insumo";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusInsumoRequisicion != 2)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se puede remover la autorización con estatus de autorizada";
                    return respuesta;
                }
                objetoEncontrado.EstatusInsumoRequisicion = 1;
                var modelo = _Mapper.Map<InsumoXRequisicion>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó remover la autorización";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Autorización del insumo removida";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la revocación de la autorización";
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
                if (objetoEncontrado.EstatusInsumoComprado >= 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Hay compras asociadas al insumo";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusInsumoRequisicion == 3)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Hay cotizaciones asociadas al insumo";
                    return respuesta;
                }
                //1 = capturada, 2 = autorizada, 3 = comprada, 4 = cancelada
                objetoEncontrado.EstatusInsumoRequisicion = 4;
                var modelo = _Mapper.Map<InsumoXRequisicion>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo cancelar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Insumo de la requisición cancelada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la cancelación del insumo";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Editar(InsumoXRequisicionDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == parametro.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusInsumoRequisicion != 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se puede editar insumos con estatus de capturado";
                    return respuesta;
                }
                objetoEncontrado.Denominacion = parametro.Denominacion;
                objetoEncontrado.PersonaIniciales = parametro.PersonaIniciales;
                objetoEncontrado.Cantidad = parametro.Cantidad;
                objetoEncontrado.Observaciones = parametro.Observaciones;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Insumo editado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del insumo de la requisición";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> ActualizarEstatusComprar(int Id)
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
                if (objetoEncontrado.EstatusInsumoRequisicion != 2)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se pueden comprar si estan autorizados";
                    return respuesta;
                }
                objetoEncontrado.EstatusInsumoComprado = objetoEncontrado.CantidadComprada < objetoEncontrado.Cantidad ? 1 :
                    objetoEncontrado.CantidadComprada == objetoEncontrado.Cantidad ? 2 :
                    objetoEncontrado.CantidadComprada > objetoEncontrado.Cantidad ? 3 : 0;
                var modelo = _Mapper.Map<InsumoXRequisicion>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Estatus del insumo actualizado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la actualización del insumo";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> ActualizarEstatusEntradaAlmacen(int Id)
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
                if (objetoEncontrado.EstatusInsumoRequisicion == 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo pueden entrar insumos comprados";
                    return respuesta;
                }
                objetoEncontrado.EstatusInsumoSurtido = objetoEncontrado.CantidadEnAlmacen < objetoEncontrado.CantidadComprada ? 1 :
                    objetoEncontrado.CantidadEnAlmacen == objetoEncontrado.CantidadComprada ? 2 :
                    objetoEncontrado.CantidadEnAlmacen > objetoEncontrado.CantidadComprada ? 3 : 0;
                var modelo = _Mapper.Map<InsumoXRequisicion>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Estatus del insumo actualizado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la actualización del insumo";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> ActualizarEstatusCotizar(int Id)
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
                if (objetoEncontrado.EstatusInsumoRequisicion != 2)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se pueden cotizar si estan autorizados";
                    return respuesta;
                }
                objetoEncontrado.EstatusInsumoComprado = 3;
                var modelo = _Mapper.Map<InsumoXRequisicion>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Estatus del insumo actualizado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la actualización del insumo";
                return respuesta;
            }
        }
        //Checar
        public async Task<RespuestaDTO> EditarCantidadComprada(int Id, decimal Cantidad)
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
                if (objetoEncontrado.EstatusInsumoRequisicion != 3)
                {
                    if (objetoEncontrado.EstatusInsumoRequisicion != 2)
                    {
                        respuesta.Estatus = false;
                        respuesta.Descripcion = "Solo se pueden comprar si estan autorizados";
                        return respuesta;
                    }
                }
                objetoEncontrado.CantidadComprada = Cantidad;
                var modelo = _Mapper.Map<InsumoXRequisicion>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Estatus del insumo actualizado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la actualización del insumo";
                return respuesta;
            }
        }
        //Checar
        public async Task<RespuestaDTO> EditarCantidadSurtida(int Id, decimal Cantidad)
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
                if (objetoEncontrado.EstatusInsumoComprado <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se pueden surtir si estan comprados";
                    return respuesta;
                }
                objetoEncontrado.CantidadEnAlmacen = Cantidad;
                var modelo = _Mapper.Map<InsumoXRequisicion>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Estatus del insumo actualizado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la actualización del insumo";
                return respuesta;
            }
        }
        /// <summary>
        /// Para el campo EstatusInsumosComprados
        /// </summary>
        /// <param name="IdRequisicion"></param>
        /// <returns>1 = compra parcial, 2 = compra total, 3 = compra parcial con excedentes, 4 = compra total con excedentes</returns>
        public async Task<int> CompruebaEstatusRequisicionInsumosComprados(int IdRequisicion)
        {
            var insumoXRequisicionDTO = await ObtenXIdRequisicion(IdRequisicion);
            if (insumoXRequisicionDTO.Count <= 0)
                return -1;

            int compraParcial = 0, compraTotal = 0, compraParcialExcedente = 0, compraTotalExcedente = 0;
            for (int i = 0; i < insumoXRequisicionDTO.Count; i++)
            {
                if (insumoXRequisicionDTO[i].Cantidad == insumoXRequisicionDTO[i].CantidadComprada)
                {
                    compraTotal++;
                }
                if (insumoXRequisicionDTO[i].Cantidad < insumoXRequisicionDTO[i].CantidadComprada)
                {
                    compraTotalExcedente++;
                    compraParcialExcedente++;
                }
                if (insumoXRequisicionDTO[i].Cantidad > insumoXRequisicionDTO[i].CantidadComprada)
                {
                    compraParcial++;
                }
            }
            if (insumoXRequisicionDTO.Count == compraParcial)
            {
                return 1;
            }
            if (insumoXRequisicionDTO.Count == compraTotal)
            {
                return 2;
            }
            if (insumoXRequisicionDTO.Count == compraTotal + compraTotalExcedente)
            {
                return 4;
            }
            if (insumoXRequisicionDTO.Count == compraParcial + compraParcialExcedente)
            {
                return 3;
            }

            return 0;
        }
        /// <summary>
        /// Para el campo EstatusInsumosComprados
        /// Si es excedente es porque compro y llego material demás
        /// </summary>
        /// <param name="IdRequisicion"></param>
        /// <returns>1 = surtimiento parcial, 2 = surtimiento total, 3 = surtimiento parcial con excedentes, 4 = surtimiento total con excedentes</returns>
        public async Task<int> CompruebaEstatusRequisicionInsumosSurtidos(int IdRequisicion)
        {
            var insumoXRequisicionDTO = await ObtenXIdRequisicion(IdRequisicion);
            if (insumoXRequisicionDTO.Count <= 0)
                return -1;

            int parcial = 0, total = 0, parcialExcedente = 0, totalExcedente = 0;
            for (int i = 0; i < insumoXRequisicionDTO.Count; i++)
            {
                if (insumoXRequisicionDTO[i].CantidadComprada == insumoXRequisicionDTO[i].CantidadEnAlmacen)
                {
                    total++;
                }
                if (insumoXRequisicionDTO[i].CantidadComprada < insumoXRequisicionDTO[i].CantidadEnAlmacen)
                {
                    totalExcedente++;
                    parcialExcedente++;
                }
                if (insumoXRequisicionDTO[i].CantidadComprada > insumoXRequisicionDTO[i].CantidadEnAlmacen)
                {
                    parcial++;
                }
            }
            if (insumoXRequisicionDTO.Count == parcial)
            {
                return 1;
            }
            if (insumoXRequisicionDTO.Count == total)
            {
                return 2;
            }
            if (insumoXRequisicionDTO.Count == total + totalExcedente)
            {
                return 4;
            }
            if (insumoXRequisicionDTO.Count == parcial + parcialExcedente)
            {
                return 3;
            }

            return 0;
        }

        public async Task<bool> CrearLista(List<InsumoXRequisicionDTO> parametro)
        {
            var objetosMapaeados = _Mapper.Map<List<InsumoXRequisicion>>(parametro);
            return await _Repositorio.CrearMultiple(objetosMapaeados);
        }

        public async Task<RespuestaDTO> ActualizarEstatusInsumosSurtidos(int Id, int estatus)
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
                objetoEncontrado.EstatusInsumoSurtido = estatus;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Estatus del insumo actualizado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la actualización del insumo";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> ActualizarEstatusInsumosComprados(int Id, int estatus)
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
                objetoEncontrado.EstatusInsumoComprado = estatus;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Estatus del insumo actualizado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la actualización del insumo";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(int idInsumoRequisicion)
        {
            var respuesta = new RespuestaDTO();
            respuesta.Estatus = false;
            var objetoEncomprado = await _Repositorio.Obtener(z => z.Id == idInsumoRequisicion);
            if (objetoEncomprado.Id <= 0) {
                respuesta.Descripcion = "insumo no encontrado";
                return respuesta;
            }
            if (objetoEncomprado.EstatusInsumoRequisicion != 1) {
                respuesta.Descripcion = "No puedes eliminar este insumo";
                return respuesta;
            }
            var eliminarInsumo = await _Repositorio.Eliminar(objetoEncomprado);
            if (!eliminarInsumo) {
                respuesta.Descripcion = "No se eliminó el insumo";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Insumo eliminado";
            return respuesta;
        }
    }
}
