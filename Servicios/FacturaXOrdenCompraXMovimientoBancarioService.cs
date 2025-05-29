using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Modelos.Contabilidad;
using ERP_TECKIO.Modelos.Facturaion;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class FacturaXOrdenCompraXMovimientoBancarioService<T> : IFacturaXOrdenCompraXMovimientoBancarioService<T> where T : DbContext
    {
        private readonly IGenericRepository<FacturaXOrdenCompraXMovimientoBancario, T> _Repositorio;
        private readonly IMapper _Mapper;
        public FacturaXOrdenCompraXMovimientoBancarioService(
            IGenericRepository<FacturaXOrdenCompraXMovimientoBancario, T> Repositorio,
            IMapper Mapper)
        {
            _Repositorio = Repositorio;
            _Mapper = Mapper;
        }

        public async Task<bool> Crear(FacturaXOrdenCompraXMovimientoBancarioDTO modelo)
        {
            var respuesta = await _Repositorio.Crear(_Mapper.Map<FacturaXOrdenCompraXMovimientoBancario>(modelo));
            if (respuesta.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Editar(FacturaXOrdenCompraXMovimientoBancarioDTO modelo)
        {
            var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == modelo.Id);
            if (objetoEncontrado.Id <= 0)
            {
                return false;
            }
            objetoEncontrado.TotalSaldado = modelo.TotalSaldado;
            var editar = await _Repositorio.Editar(objetoEncontrado);
            if (!editar)
            {
                return false;
            }
            return true;
        }

        public async Task<List<FacturaXOrdenCompraXMovimientoBancarioDTO>> ObtenXIdFacturaXOrdenCompra(int IdFacturaXOrdenCompra)
        {
            var lista = await _Repositorio.ObtenerTodos(z => z.IdFacturaXOrdenCompra == IdFacturaXOrdenCompra);
            return _Mapper.Map<List<FacturaXOrdenCompraXMovimientoBancarioDTO>>(lista);
        }

        public async Task<List<FacturaXOrdenCompraXMovimientoBancarioDTO>> ObtenXIdMovimientoBancario(int IdMovimientoBancario)
        {
            var lista = await _Repositorio.ObtenerTodos(z => z.IdMovimientoBancario == IdMovimientoBancario);
            return _Mapper.Map<List<FacturaXOrdenCompraXMovimientoBancarioDTO>>(lista);
        }
    }
}
