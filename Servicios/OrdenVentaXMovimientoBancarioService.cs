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

        public async Task<bool> Crear(OrdenVentaXMovimientoBancarioDTO modelo)
        {
            var respuesta = await _genericRepository.Crear(_mapper.Map<OrdenVentaXMovimientoBancario>(modelo));
            if (respuesta.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Editar(OrdenVentaXMovimientoBancarioDTO modelo)
        {
            var objetoEncontrado = await _genericRepository.Obtener(z => z.Id == modelo.Id);
            if (objetoEncontrado.Id <= 0)
            {
                return false;
            }
            objetoEncontrado.TotalSaldado = modelo.TotalSaldado;
            var editar = await _genericRepository.Editar(objetoEncontrado);
            if (!editar)
            {
                return false;
            }
            return true;
        }

        public async Task<List<OrdenVentaXMovimientoBancarioDTO>> ObtenXIdMovimientoBancario(int IdMovimientoBancario)
        {
            var lista = await _genericRepository.ObtenerTodos(z => z.IdMovimientoBancario == IdMovimientoBancario);
            return _mapper.Map<List<OrdenVentaXMovimientoBancarioDTO>>(lista);
        }

        public async Task<List<OrdenVentaXMovimientoBancarioDTO>> ObtenXIdOrdenVenta(int IdOrdenVenta)
        {
            var lista = await _genericRepository.ObtenerTodos(z => z.IdOrdenVenta == IdOrdenVenta);
            return _mapper.Map<List<OrdenVentaXMovimientoBancarioDTO>>(lista);
        }
    }
}
