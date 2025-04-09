using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Modelos.Facturacion;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class MonedaSatService<T> : IMonedaSatService<T> where T : DbContext
    {
        private readonly IGenericRepository<MonedaSat, T> _repository;
        private readonly IMapper _mapper;
        public MonedaSatService(
            IGenericRepository<MonedaSat, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Crear(MonedaSatDTO parametro)
        {
            var crear = await _repository.Crear(_mapper.Map<MonedaSat>(parametro));
            if (crear.Id <= 0) { 
                return false;
            }
            return true;
        }

        public async Task<MonedaSatDTO> ObtenerXClave(string codigo)
        {
            var objeto = await _repository.Obtener(z => z.Codigo == codigo);
            if (objeto.Id <= 0)
            {
                return new MonedaSatDTO();
            }
            return _mapper.Map<MonedaSatDTO>(objeto);
        }

        public async Task<MonedaSatDTO> ObtenerXId(int Id)
        {
            var objeto = await _repository.Obtener(z => z.Id == Id);
            if (objeto.Id <= 0)
            {
                return new MonedaSatDTO();
            }
            return _mapper.Map<MonedaSatDTO>(objeto);
        }
    }
}
