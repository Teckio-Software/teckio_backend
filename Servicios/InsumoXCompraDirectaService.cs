using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;

namespace ERP_TECKIO.Servicios
{
    public class InsumoXCompraDirectaService<T> : IInsumoXCompraDirectaService<T> where T: DbContext
    {
        private readonly IGenericRepository<InsumoXCompraDirecta, T> _Repositorio;
        //private readonly PROCOMIContext _Context;
        private readonly IMapper _Mapper;

        public InsumoXCompraDirectaService(
            IGenericRepository<InsumoXCompraDirecta, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            //_Context = context;
            _Mapper = mapper;
        }
        public async Task<RespuestaDTO> Crear(InsumoXCompraDirectaCreacionDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                if (parametro.IdCompraDirecta <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No hay una cotización asociada";
                    return respuesta;
                }
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<InsumoXCompraDirecta>(parametro));
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
                respuesta.Descripcion = "Algo salió mal en la creación del insumo de la compra directa";
                return respuesta;
            }
        }

        public async Task<InsumoXCompraDirectaDTO> CrearYObtener(InsumoXCompraDirectaCreacionDTO parametro)
        {
            try
            {
                if (parametro.IdCompraDirecta <= 0)
                {
                    return new InsumoXCompraDirectaDTO();
                }
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<InsumoXCompraDirecta>(parametro));
                if (objetoCreado.Id <= 0)
                {
                    return new InsumoXCompraDirectaDTO();
                }
                return _Mapper.Map<InsumoXCompraDirectaDTO>(objetoCreado);
            }
            catch (Exception ex)
            {
                return new InsumoXCompraDirectaDTO();
            }
        }
        public async Task<RespuestaDTO> Editar(InsumoXCompraDirectaDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<InsumoXCompraDirecta>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                if (objetoEncontrado.EstatusInsumoCompraDirecta != 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se puede editar insumos con estatus de capturado";
                    return respuesta;
                }
                decimal resultado = 0;
                decimal importeIva = 0;
                decimal importeIsr = 0;
                decimal importeIeps = 0;
                decimal importeIsan = 0;
                importeIva = modelo.ImporteSinIva * modelo.Iva / 100;
                importeIsr = modelo.ImporteSinIva * modelo.Isr / 100;
                importeIeps = modelo.ImporteSinIva * modelo.Ieps / 100;
                importeIsan = modelo.ImporteSinIva * modelo.Isan / 100;
                resultado = modelo.ImporteSinIva + importeIva + importeIsr + importeIeps + importeIsan;

                objetoEncontrado.Cantidad = modelo.Cantidad;
                objetoEncontrado.PrecioUnitario = modelo.PrecioUnitario;
                objetoEncontrado.ImporteSinIva = modelo.ImporteSinIva;
                objetoEncontrado.Iva = modelo.Iva;
                objetoEncontrado.Isr = modelo.Isr;
                objetoEncontrado.Ieps = modelo.Ieps;
                objetoEncontrado.Isan = modelo.Isan;
                objetoEncontrado.ImporteConIva = resultado;

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
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de la requisición";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> ActualizarEstatusFacturado(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenXId(Id);
                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                //1 = capturada, 2 = facturada, 3 = cancelada
                objetoEncontrado.EstatusInsumoCompraDirecta = 2;
                var modelo = _Mapper.Map<InsumoXCompraDirecta>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo cambiar el estatus a facturada";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Insumo de la orden de compra facturada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en el estatus de facturado del insumo";
                return respuesta;
            }
        }
        public async Task<RespuestaDTO> ActualizarEstatusCancelar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenXId(Id);
                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                //1 = capturada, 2 = facturada, 3 = cancelada
                objetoEncontrado.EstatusInsumoCompraDirecta = 3;
                var modelo = _Mapper.Map<InsumoXCompraDirecta>(objetoEncontrado);
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
        public async Task<List<InsumoXCompraDirectaDTO>> ObtenTodos()
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<InsumoXCompraDirectaDTO>>(query);
            }
            catch
            {
                return new List<InsumoXCompraDirectaDTO>();
            }
        }

        public async Task<List<InsumoXCompraDirectaDTO>> ObtenXIdCompraDirecta(int IdCompraDirecta)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdCompraDirecta == IdCompraDirecta);
                return _Mapper.Map<List<InsumoXCompraDirectaDTO>>(query);
            }
            catch
            {
                return new List<InsumoXCompraDirectaDTO>();
            }
        }

        public async Task<InsumoXCompraDirectaDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<InsumoXCompraDirectaDTO>(query);
            }
            catch
            {
                return new InsumoXCompraDirectaDTO();
            }
        }

    }
}
