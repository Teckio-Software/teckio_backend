using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos.Facturacion;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class UnidadSatService<T> : IUnidadSatService<T> where T : DbContext
    {
        private readonly IGenericRepository<UnidadSat, T> _repository;
        private readonly IMapper _mapper;
        public UnidadSatService(
            IGenericRepository<UnidadSat, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Crear(UnidadSatDTO parametro)
        {
            var crear = await _repository.Crear(_mapper.Map<UnidadSat>(parametro));
            if (crear.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<UnidadSatDTO> ObtenerXClave(string clave)
        {
            var objeto = await _repository.Obtener(z => z.Clave == clave);
            if (objeto.Id <= 0)
            {
                return new UnidadSatDTO();
            }
            return _mapper.Map<UnidadSatDTO>(objeto);
        }

        public async Task<UnidadSatDTO> ObtenerXId(int Id)
        {
            var objeto = await _repository.Obtener(z => z.Id == Id);
            if (objeto.Id <= 0)
            {
                return new UnidadSatDTO();
            }
            return _mapper.Map<UnidadSatDTO>(objeto);
        }
    }
}
