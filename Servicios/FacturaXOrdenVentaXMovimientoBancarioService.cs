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

        public async Task<bool> Crear(FacturaXOrdenVentaXMovimientoBancarioDTO modelo)
        {
            var respuesta = await _genericRepository.Crear(_mapper.Map<FacturaXOrdenVentaXMovimientoBancario>(modelo));
            if (respuesta.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Editar(FacturaXOrdenVentaXMovimientoBancarioDTO modelo)
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

        public async Task<List<FacturaXOrdenVentaXMovimientoBancarioDTO>> ObtenXIdFacturaXOrdenVenta(int IdFacturaXOrdenVenta)
        {
            var lista = await _genericRepository.ObtenerTodos(z => z.IdFacturaXOrdenVenta == IdFacturaXOrdenVenta);
            return _mapper.Map<List<FacturaXOrdenVentaXMovimientoBancarioDTO>>(lista);
        }

        public async Task<List<FacturaXOrdenVentaXMovimientoBancarioDTO>> ObtenXIdMovimientoBancario(int IdMovimientoBancario)
        {
            var lista = await _genericRepository.ObtenerTodos(z => z.IdMovimientoBancario == IdMovimientoBancario);
            return _mapper.Map<List<FacturaXOrdenVentaXMovimientoBancarioDTO>>(lista);
        }
    }
}
