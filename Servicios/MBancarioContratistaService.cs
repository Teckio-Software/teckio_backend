using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class MBancarioContratistaService<T> : IMBancarioContratistaService<T> where T : DbContext
    {
        private readonly IGenericRepository<MovimientoBancarioContratista, T> _repository;
        private readonly IMapper _mapper;

        public MBancarioContratistaService(
            IGenericRepository<MovimientoBancarioContratista, T> repository,
            IMapper mapper
            ) {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Crear(MBancarioBeneficiarioDTO modelo)
        {
            var objeto = await _repository.Crear(_mapper.Map<MovimientoBancarioContratista>(modelo));
            if (objeto.Id <= 0) { 
                return false;
            }
            return true;
        }

        public async Task<MBancarioBeneficiarioDTO> CrearYObtener(MBancarioBeneficiarioDTO modelo)
        {
            var objeto = await _repository.Crear(_mapper.Map<MovimientoBancarioContratista>(modelo));
            return _mapper.Map<MBancarioBeneficiarioDTO>(objeto);
        }

        public async Task<List<MBancarioBeneficiarioDTO>> ObtenerTodos()
        {
            var objetos = await _repository.ObtenerTodos();
            return _mapper.Map<List<MBancarioBeneficiarioDTO>>(objetos);
        }

        public async Task<MBancarioBeneficiarioDTO> ObtenerXIdMovimientoBancario(int IdMovimientoBancario)
        {
            var objeto = await _repository.Obtener(z => z.IdMovimientoBancario == IdMovimientoBancario);
            return _mapper.Map<MBancarioBeneficiarioDTO>(objeto);
        }
    }
}
