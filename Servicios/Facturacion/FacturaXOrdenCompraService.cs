using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos.Facturaion;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class FacturaXOrdenCompraService<T> : IFacturaXOrdenCompraService<T> where T : DbContext
    {
        private readonly IGenericRepository<FacturaXOrdenCompra, T> _repository;
        private readonly IMapper _mapper;
        public FacturaXOrdenCompraService(
            IGenericRepository<FacturaXOrdenCompra, T> repository,
            IMapper mapper
            ) { 
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Crear(FacturaXOrdenCompraDTO parametro)
        {
            var objeto = await _repository.Crear(_mapper.Map<FacturaXOrdenCompra>(parametro));
            if (objeto.Id <= 0) {
                return false;
            }
            return true;
        }

        public async Task<List<FacturaXOrdenCompraDTO>> ObtenerXIdOrdenCompra(int IdOrdenCompra)
        {
            var lista = await _repository.ObtenerTodos(z => z.IdOrdenCompra == IdOrdenCompra);
            return _mapper.Map<List<FacturaXOrdenCompraDTO>>(lista);
        }
    }
}
