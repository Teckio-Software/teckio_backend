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

        public Task<bool> Crear(FacturaXOrdenVentaDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(FacturaXOrdenVentaDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaXOrdenVentaDTO>> ObtenerXIdOrdenVenta(int IdOrdenVenta)
        {
            throw new NotImplementedException();
        }
    }
}
