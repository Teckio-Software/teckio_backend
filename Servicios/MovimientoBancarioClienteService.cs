using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class MovimientoBancarioClienteService<T> : IMovimientoBancarioClienteService<T> where T : DbContext
    {
        private readonly IGenericRepository<MovimientoBancarioCliente, T> _repository;

        private readonly IMapper _mapper;
        public MovimientoBancarioClienteService(
            IGenericRepository<MovimientoBancarioCliente, T> repository,
            IMapper mapper
            ) {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Crear(MBancarioBeneficiarioDTO modelo)
        {
            var objeto = await _repository.Crear(_mapper.Map<MovimientoBancarioCliente>(modelo));
            if (objeto.Id <= 0) { 
                return false;
            }
            return true;
        }

        public async Task<MBancarioBeneficiarioDTO> CrearYObtener(MBancarioBeneficiarioDTO modelo)
        {
            var objeto = await _repository.Crear(_mapper.Map<MovimientoBancarioCliente>(modelo));
            return _mapper.Map<MBancarioBeneficiarioDTO>(objeto);
        }

        public async Task<List<MBancarioBeneficiarioDTO>> ObtenerTodos()
        {
            var objetos = await _repository.ObtenerTodos();
            return _mapper.Map<List<MBancarioBeneficiarioDTO>>(objetos);
        }
    }
}
