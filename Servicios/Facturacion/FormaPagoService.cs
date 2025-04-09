using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos.Facturacion;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class FormaPagoService<T> : IFormaPagoSatService<T> where T : DbContext
    {
        private readonly IGenericRepository<FormaPagoSat, T> _repository;
        private readonly IMapper _mapper;
        public FormaPagoService(
            IGenericRepository<FormaPagoSat, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Crear(FormaPagoSatDTO parametro)
        {
            var crear = await _repository.Crear(_mapper.Map<FormaPagoSat>(parametro));
            if (crear.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<FormaPagoSatDTO> ObtenerXClave(string clave)
        {
            var objeto = await _repository.Obtener(z => z.Clave == clave);
            if (objeto.Id <= 0)
            {
                return new FormaPagoSatDTO();
            }
            return _mapper.Map<FormaPagoSatDTO>(objeto);
        }

        public async Task<FormaPagoSatDTO> ObtenerXId(int Id)
        {
            var objeto = await _repository.Obtener(z => z.Id == Id);
            if (objeto.Id <= 0)
            {
                return new FormaPagoSatDTO();
            }
            return _mapper.Map<FormaPagoSatDTO>(objeto);
        }
    }
}
