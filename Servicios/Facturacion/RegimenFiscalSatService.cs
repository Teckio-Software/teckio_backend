using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos.Facturacion;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class RegimenFiscalSatService<T> : IRegimenFiscalSatService<T> where T : DbContext
    {
        private readonly IGenericRepository<RegimenFiscalSat, T> _repository;
        private readonly IMapper _mapper;
        public RegimenFiscalSatService(
            IGenericRepository<RegimenFiscalSat, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Crear(RegimenFiscalSatDTO parametro)
        {
            var crear = await _repository.Crear(_mapper.Map<RegimenFiscalSat>(parametro));
            if (crear.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<List<RegimenFiscalSatDTO>> ObtenerTodos()
        {
            var lista = await _repository.ObtenerTodos();
            return _mapper.Map<List<RegimenFiscalSatDTO>>(lista);
        }

        public async Task<RegimenFiscalSatDTO> ObtenerXClave(string clave)
        {
            var objeto = await _repository.Obtener(z => z.Clave == clave);
            if (objeto.Id <= 0)
            {
                return new RegimenFiscalSatDTO();
            }
            return _mapper.Map<RegimenFiscalSatDTO>(objeto);
        }

        public async Task<RegimenFiscalSatDTO> ObtenerXId(int Id)
        {
            var objeto = await _repository.Obtener(z => z.Id == Id);
            if (objeto.Id <= 0)
            {
                return new RegimenFiscalSatDTO();
            }
            return _mapper.Map<RegimenFiscalSatDTO>(objeto);
        }
    }
}
