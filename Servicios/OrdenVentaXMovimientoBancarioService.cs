using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos.Contabilidad;
using ERP_TECKIO.Modelos.Facturaion;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class OrdenVentaXMovimientoBancarioService<T> : IOrdenVentaXMovimientoBancarioService<T> where T : DbContext
    {
        private readonly IGenericRepository<OrdenVentaXMovimientoBancario, T> _genericRepository;
        private readonly IMapper _mapper;

        public OrdenVentaXMovimientoBancarioService(
            IGenericRepository<OrdenVentaXMovimientoBancario, T> genericRepository,
            IMapper mapper
            )
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public Task<bool> Crear(OrdenVentaXMovimientoBancarioDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(OrdenVentaXMovimientoBancarioDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrdenVentaXMovimientoBancarioDTO>> ObtenXIdMovimientoBancario(int IdMovimientoBancario)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrdenVentaXMovimientoBancarioDTO>> ObtenXIdOrdenVenta(int IdOrdenVenta)
        {
            throw new NotImplementedException();
        }
    }
}
