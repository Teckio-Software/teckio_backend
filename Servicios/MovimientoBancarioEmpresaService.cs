using AutoMapper;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class MovimientoBancarioEmpresaService<T> : IMovimientoBancarioEmpresaService<T> where T : DbContext
    {
        private readonly IGenericRepository<MovimientoBancarioEmpresa, T> _repository;

        private readonly IMapper _mapper;
        public MovimientoBancarioEmpresaService(
            IGenericRepository<MovimientoBancarioEmpresa, T> repository,
            IMapper mapper
            ) {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Crear(MBancarioBeneficiarioDTO modelo)
        {
            var objeto = await _repository.Crear(_mapper.Map<MovimientoBancarioEmpresa>(modelo));
            if (objeto.Id <= 0) { 
                return false;
            }
            return true;
        }

        public async Task<MBancarioBeneficiarioDTO> CrearYObtener(MBancarioBeneficiarioDTO modelo)
        {
            var objeto = await _repository.Crear(_mapper.Map<MovimientoBancarioEmpresa>(modelo));
            return _mapper.Map<MBancarioBeneficiarioDTO>(objeto);
        }
    }
}
