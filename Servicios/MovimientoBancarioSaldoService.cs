using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class MovimientoBancarioSaldoService<T> : IMovimientoBancarioSaldoService<T> where T : DbContext
    {
        private readonly IGenericRepository<MovimientoBancarioSaldo, T> _repository;

        private readonly IMapper _mapper;
        public MovimientoBancarioSaldoService(
            IGenericRepository<MovimientoBancarioSaldo, T> repository,
            IMapper mapper
            ) {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> ActualizarSaldoXIdCuentaBancariaYAnioYMes(MovimientoBancarioSaldoDTO modelo)
        {
            var objetoencontrado = await _repository.Obtener(z => z.IdCuentaBancaria == modelo.IdCuentaBancaria && z.Anio == modelo.Anio && z.Mes == modelo.Mes);
            if (objetoencontrado.Id <= 0)
            {
                return false;
            }
            objetoencontrado.MontoFinal = modelo.MontoFinal;
            var resultado = await _repository.Editar(objetoencontrado);
            return resultado;
        }

        public async Task<bool> Crear(MovimientoBancarioSaldoDTO modelo)    
        {
            var objeto = await _repository.Crear(_mapper.Map<MovimientoBancarioSaldo>(modelo));
            if (objeto.Id <= 0) { 
                return false;
            }
            return true;
        }

        public async Task<List<MovimientoBancarioSaldoDTO>> ObtenerXIdCuentaBancaria(int IdCuentaBancaria)
        {
            var objeto = await _repository.ObtenerTodos(z => z.IdCuentaBancaria == IdCuentaBancaria);
            return _mapper.Map<List<MovimientoBancarioSaldoDTO>>(objeto);
        }

        public async Task<MovimientoBancarioSaldoDTO> ObtenerXIdCuentaBancariaYAnioYMes(MovimientoBancarioSaldoDTO modelo)
        {
            var objeto = await _repository.Obtener(z => z.Anio == modelo.Anio && z.Mes == modelo.Mes && z.IdCuentaBancaria == modelo.IdCuentaBancaria);
            return _mapper.Map<MovimientoBancarioSaldoDTO>(objeto);
        }
    }
}
