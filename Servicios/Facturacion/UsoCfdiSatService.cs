using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos.Facturacion;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class UsoCfdiSatService<T> : IUsoCfdiSatService<T> where T : DbContext
    {
        private readonly IGenericRepository<UsoCfdiSat, T> _repository;
        private readonly IMapper _mapper;
        public UsoCfdiSatService(
            IGenericRepository<UsoCfdiSat, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Crear(UsoCfdiSatDTO parametro)
        {
            var crear = await _repository.Crear(_mapper.Map<UsoCfdiSat>(parametro));
            if (crear.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<List<UsoCfdiSatDTO>> ObtenerTodos()
        {
            var lista = await _repository.ObtenerTodos();
            return _mapper.Map<List<UsoCfdiSatDTO>>(lista);
        }

        public async Task<UsoCfdiSatDTO> ObtenerXClave(string clave)
        {
            var objeto = await _repository.Obtener(z => z.Clave == clave);
            if (objeto.Id <= 0)
            {
                return new UsoCfdiSatDTO();
            }
            return _mapper.Map<UsoCfdiSatDTO>(objeto);
        }

        public async Task<UsoCfdiSatDTO> ObtenerXId(int Id)
        {
            var objeto = await _repository.Obtener(z => z.Id == Id);
            if (objeto.Id <= 0)
            {
                return new UsoCfdiSatDTO();
            }
            return _mapper.Map<UsoCfdiSatDTO>(objeto);
        }
    }
}
