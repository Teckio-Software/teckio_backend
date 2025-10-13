using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos.Facturaion;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class FacturaXOrdenVentaService<T> : IFacturaXOrdenVentaService<T> where T : DbContext
    {
        private readonly IGenericRepository<FacturaXOrdenVenta, T> _genericRepository;
        private readonly IMapper _mapper;

        public FacturaXOrdenVentaService(
            IGenericRepository<FacturaXOrdenVenta, T> genericRepository,
            IMapper mapper
            ) { 
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<bool> Crear(FacturaXOrdenVentaDTO parametro)
        {
            var objeto = await _genericRepository.Crear(_mapper.Map<FacturaXOrdenVenta>(parametro));
            if (objeto.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Editar(FacturaXOrdenVentaDTO parametro)
        {
            var objetoEncontado = await _genericRepository.Obtener(z => z.Id == parametro.Id);
            if (objetoEncontado.Id <= 0)
            {
                return false;
            }
            objetoEncontado.Estatus = parametro.Estatus;
            objetoEncontado.TotalSaldado = parametro.TotalSaldado;
            var respuesta = await _genericRepository.Editar(objetoEncontado);
            if (!respuesta)
            {
                return false;
            }
            return true;
        }

        public async Task<List<FacturaXOrdenVentaDTO>> ObtenerXIdOrdenVenta(int IdOrdenVenta)
        {
            var lista = await _genericRepository.ObtenerTodos(z => z.IdOrdenVenta == IdOrdenVenta);
            return _mapper.Map<List<FacturaXOrdenVentaDTO>>(lista);
        }
    }
}
