using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos.Contabilidad;
using ERP_TECKIO.Modelos.Facturaion;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class FacturaXOrdenVentaXMovimientoBancarioService<T> : IFacturaXOrdenVentaXMovimientoBancarioService<T> where T : DbContext
    {
        private readonly IGenericRepository<FacturaXOrdenVentaXMovimientoBancario, T> _genericRepository;
        private readonly IMapper _mapper;

        public FacturaXOrdenVentaXMovimientoBancarioService(
            IGenericRepository<FacturaXOrdenVentaXMovimientoBancario, T> genericRepository,
            IMapper mapper
            )
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public Task<bool> Crear(FacturaXOrdenVentaXMovimientoBancarioDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(FacturaXOrdenVentaXMovimientoBancarioDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaXOrdenVentaXMovimientoBancarioDTO>> ObtenXIdFacturaXOrdenVenta(int IdFacturaXOrdenVenta)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaXOrdenVentaXMovimientoBancarioDTO>> ObtenXIdMovimientoBancario(int IdMovimientoBancario)
        {
            throw new NotImplementedException();
        }
    }
}
