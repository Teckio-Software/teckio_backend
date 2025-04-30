using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos.Facturacion;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class UnidadService<T> : IUnidadService<T> where T : DbContext
    {
        private readonly IGenericRepository<Unidad, T> _repository;
        private readonly IMapper _mapper;
        public UnidadService(
            IGenericRepository<Unidad, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Crear(UnidadDTO parametro)
        {
            var crear = await _repository.Crear(_mapper.Map<Unidad>(parametro));
            if (crear.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<UnidadDTO> ObtenerXId(int Id)
        {
            var objeto = await _repository.Obtener(z => z.Id == Id);
            if (objeto.Id <= 0)
            {
                return new UnidadDTO();
            }
            return _mapper.Map<UnidadDTO>(objeto);
        }
    }
}
