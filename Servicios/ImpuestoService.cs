using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class ImpuestoService<T> : IImpuestoService<T> where T : DbContext
    {
        private readonly IGenericRepository<Impuesto, T> _Repository;
        private readonly IMapper _Mapper;

        public ImpuestoService(IGenericRepository<Impuesto, T> repository, IMapper Mapper)
        {
            _Repository = repository;
            _Mapper = Mapper;
        }

        public async Task<List<ImpuestoDTO>> ObtenerTodos()
        {
            var respuesta = await _Repository.ObtenerTodos();
            return _Mapper.Map<List<ImpuestoDTO>>(respuesta);
        }
    }
}
