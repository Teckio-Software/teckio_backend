using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Modelos.Contabilidad;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class OrdenCompraXMovimientoBancarioService<T> : IOrdenCompraXMovimientoBancarioService<T> where T : DbContext
    {
        private readonly IGenericRepository<OrdenCompraXMovimientoBancario, T> _Repositorio;
        private readonly IMapper _Mapper;
        public OrdenCompraXMovimientoBancarioService(
            IGenericRepository<OrdenCompraXMovimientoBancario, T> Repositorio, 
            IMapper Mapper)
            {
            _Repositorio = Repositorio;
            _Mapper = Mapper;
        }
        public async Task<bool> Crear(OrdenCompraXMovimientoBancarioDTO modelo)
        {
            var respuesta = await _Repositorio.Crear(_Mapper.Map<OrdenCompraXMovimientoBancario>(modelo));
            if (respuesta.Id <= 0) {
                return false;
            }
            return true;
        }

        public async Task<List<OrdenCompraXMovimientoBancarioDTO>> ObtenXIdMovimientoBancario(int IdMovimientoBancario)
        {
            var lista = await _Repositorio.ObtenerTodos(z => z.IdMovimientoBancario == IdMovimientoBancario);
            return _Mapper.Map<List<OrdenCompraXMovimientoBancarioDTO>>(lista);
        }

        public async Task<List<OrdenCompraXMovimientoBancarioDTO>> ObtenXIdOrdenCompra(int IdOrdenCompra)
        {
            var lista = await _Repositorio.ObtenerTodos(z => z.IdOrdenCompra == IdOrdenCompra);
            return _Mapper.Map<List<OrdenCompraXMovimientoBancarioDTO>>(lista);
        }
    }
}
