using AutoMapper;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class ImpuestoInsumoOrdenCompraService<T> : IImpuestoInsumoOrdenCompraService<T> where T : DbContext
    {
        private readonly IGenericRepository<ImpuestoInsumoOrdenCompra, T> _Repositorio;
        private readonly IMapper _Mapper;

        public ImpuestoInsumoOrdenCompraService(
            IGenericRepository<ImpuestoInsumoOrdenCompra, T> Repositorio,
            IMapper Mapper
            ) {
            _Repositorio = Repositorio;
            _Mapper = Mapper;
        }
        public async Task<bool> CrearLista(List<ImpuestoInsumoOrdenCompraCreacionDTO> parametro)
        {
            var objetosMapaeados = _Mapper.Map<List<ImpuestoInsumoOrdenCompra>>(parametro);
            return await _Repositorio.CrearMultiple(objetosMapaeados);
        }

        public async Task<List<ImpuestoInsumoOrdenCompraDTO>> ObtenerXIdInsumoXOrdenCompra(int IdInsumoXOrdenCompra)
        {
            var respuesta = await _Repositorio.ObtenerTodos(z => z.IdInsumoOrdenCompra == IdInsumoXOrdenCompra);
            return _Mapper.Map<List<ImpuestoInsumoOrdenCompraDTO>>(respuesta);
        }
    }
}
