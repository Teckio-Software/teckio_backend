using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class CompraDirectaService<T> : ICompraDirectaService<T> where T : DbContext
    {
        private readonly IGenericRepository<CompraDirecta, T> _Repositorio;
        private readonly IMapper _Mapper;

        public CompraDirectaService(
            IGenericRepository<CompraDirecta, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }
        public async Task<RespuestaDTO> Crear(CompraDirectaCreacionDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                if (string.IsNullOrEmpty(parametro.NoCompraDirecta) || parametro.IdRequisicion <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No hay una requsicion asociada";
                    return respuesta;
                }
                var cotizaciones = await _Repositorio.ObtenerTodos(z => z.IdRequisicion == parametro.IdRequisicion);
                if (string.IsNullOrEmpty(parametro.NoCompraDirecta) || parametro.IdRequisicion <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No hay una requisición asociada";
                    return respuesta;
                }
                parametro.FechaRegistro = DateTime.Now;
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<CompraDirecta>(parametro));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Compra directa creada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la creación de la compra directa";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Editar(CompraDirectaDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenXId(parametro.Id);


                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La compra no existe";
                    return respuesta;
                }
                if (objetoEncontrado.Estatus != 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se puede modificar compras con estatus de capturado";
                    return respuesta;
                }
                objetoEncontrado.IdContratista = parametro.IdContratista;
                objetoEncontrado.Elaboro = parametro.Elaboro;
                objetoEncontrado.Chofer = parametro.Chofer;
                objetoEncontrado.Observaciones = parametro.Observaciones;
                var modelo = _Mapper.Map<CompraDirecta>(parametro);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Compra editada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de la compra directa";
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
                    respuesta.Descripcion = "La compra no existe";
                    return respuesta;
                }
                //Orden compra facturada
                if (objetoEncontrado.Estatus == 2)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "La compra esta facturada";
                    return respuesta;
                }
                objetoEncontrado.Estatus = 3;
                var modelo = _Mapper.Map<CompraDirecta>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó cancelar la compra";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Compra actualizada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en el cancelación de la compra";
                return respuesta;
            }
        }

        public async Task<List<CompraDirectaDTO>> ObtenXIdProyecto(int IdProyecto)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto);
                return _Mapper.Map<List<CompraDirectaDTO>>(query);
            }
            catch
            {
                return new List<CompraDirectaDTO>();
            }
        }

        public async Task<CompraDirectaDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<CompraDirectaDTO>(query);
            }
            catch
            {
                return new CompraDirectaDTO();
            }
        }

        public async Task<List<CompraDirectaDTO>> ObtenXIdRequisicion(int IdRequisicion)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdRequisicion == IdRequisicion);
                return _Mapper.Map<List<CompraDirectaDTO>>(query);
            }
            catch
            {
                return new List<CompraDirectaDTO>();
            }
        }

        public async Task<CompraDirectaDTO> CrearYObtener(CompraDirectaCreacionDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                parametro.FechaRegistro = DateTime.Now;
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<CompraDirecta>(parametro));
                if (objetoCreado.Id <= 0)
                {
                    return new CompraDirectaDTO();
                }
                return _Mapper.Map<CompraDirectaDTO>(objetoCreado);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                return new CompraDirectaDTO();
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
                    respuesta.Descripcion = "La compra no existe";
                    return respuesta;
                }
                //Orden compra facturada
                if (objetoEncontrado.Estatus == 3)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "La compra esta cancelada";
                    return respuesta;
                }
                objetoEncontrado.Estatus = 2;
                var modelo = _Mapper.Map<CompraDirecta>(objetoEncontrado);
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
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La compra no existe";
                    return respuesta;
                }
                //Orden compra facturada
                if (objetoEncontrado.Estatus == 3)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "La compra esta cancelada";
                    return respuesta;
                }
                objetoEncontrado.EstatusAlmacen = 1;
                var modelo = _Mapper.Map<CompraDirecta>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó actualizar la compra";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Compra actualizada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en el cambio de estatus de almacén de la compra";
                return respuesta;
            }
        }
    }
}
