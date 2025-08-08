using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class OrdenVentaService<T> : IOrdenVentaService<T> where T : DbContext
    {
        private readonly IGenericRepository<OrdenVentum, T> _repository;
        private readonly IMapper _mapper;
        public OrdenVentaService(
            IGenericRepository<OrdenVentum, T> repository,
            IMapper mapper
            ) { 
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<RespuestaDTO> Crear(OrdenVentaDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                if(string.IsNullOrEmpty(modelo.Elaboro) || modelo.IdCliente < 0)
                {
                    return new RespuestaDTO
                    {
                        Estatus = false,
                        Descripcion = "No hay un cliente asociado"
                    };
                }
                modelo.FechaRegistro = DateTime.Now;
                modelo.Estatus = 0;
                modelo.ImporteTotal = 0;
                modelo.Subtotal = 0;
                modelo.TotalSaldado = 0;
                modelo.Descuento = 0;
                modelo.EstatusSaldado = 0;
                var objetoCreado = await _repository.Crear(_mapper.Map<OrdenVentum>(modelo));
                if (objetoCreado.Id > 0)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "Orden de compra creada";
                    return respuesta;
                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la creación de la orden de venta";
                return respuesta;
            }
        }

        public async Task<OrdenVentaDTO> CrearYObtener(OrdenVentaDTO modelo)
        {
            var objeto = _mapper.Map<OrdenVentum>(modelo);
            var respuesta = await _repository.Crear(objeto);
            return _mapper.Map<OrdenVentaDTO>(respuesta);
        }

        public async Task<RespuestaDTO> Editar(OrdenVentaDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _repository.Obtener(z => z.Id == modelo.Id);
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La orden de venta no existe";
                    return respuesta;
                }
                if (objetoEncontrado.Estatus != 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La orden de venta no puede estar facturada o cancelada";
                    return respuesta;
                }
                objetoEncontrado.Autorizo = modelo.Autorizo;
                objetoEncontrado.IdCliente = modelo.IdCliente;
                objetoEncontrado.Estatus = modelo.Estatus;
                objetoEncontrado.EstatusSaldado = modelo.EstatusSaldado;
                objetoEncontrado.ImporteTotal = modelo.ImporteTotal;
                objetoEncontrado.Subtotal = modelo.Subtotal;
                objetoEncontrado.EstatusSaldado = modelo.EstatusSaldado;
                objetoEncontrado.TotalSaldado = modelo.TotalSaldado;
                objetoEncontrado.Observaciones = modelo.Observaciones;
                objetoEncontrado.Descuento = modelo.Descuento;
                respuesta.Estatus = await _repository.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Descripcion = "Orden de venta editada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de la orden de venta";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(OrdenVentaDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _repository.Obtener(o => o.Id == modelo.Id);
                if (modelo.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontro la orden de venta";
                    return respuesta;
                }
                respuesta.Estatus = await _repository.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "Ocurrio un error al intentar eliminar la orden de venta";
                    return respuesta;
                }
                respuesta.Descripcion = "Orden de venta eliminada exitosamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Ocurrio un error al intentar eliminar la orden de venta";
                return respuesta;
            }
        }

        public async Task<List<OrdenVentaDTO>> ObtenerTodos()
        {
            var lista = await _repository.ObtenerTodos();
            return _mapper.Map<List<OrdenVentaDTO>>(lista);
        }
    }
}
