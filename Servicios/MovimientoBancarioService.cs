using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class MovimientoBancarioService<T> : IMovimientoBancarioService<T> where T : DbContext
    {
        private readonly IGenericRepository<MovimientoBancario, T> _repository;
        private readonly IMapper _mapper;

        public MovimientoBancarioService(
            IGenericRepository<MovimientoBancario, T> repository,
            IMapper mapper
        ) {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Crear(MovimientoBancarioTeckioDTO modelo)
        {
            var objeto = await _repository.Crear(_mapper.Map<MovimientoBancario>(modelo));
            if (objeto.Id <= 0) {
                return false;
            }
            return true;
        }

        public async Task<MovimientoBancarioTeckioDTO> CrearYObtener(MovimientoBancarioTeckioDTO modelo)
        {
            var objeto = await _repository.Crear(_mapper.Map<MovimientoBancario>(modelo));
            return _mapper.Map<MovimientoBancarioTeckioDTO>(objeto);
        }

        public async Task<List<MovimientoBancarioTeckioDTO>> ObtenerXIdCuentaBancaria(int IdCuentaBancaria)
        {
            var objetos = await _repository.ObtenerTodos(z => z.IdCuentaBancariaEmpresa == IdCuentaBancaria);
            return _mapper.Map<List<MovimientoBancarioTeckioDTO>>(objetos);
        }

        public async Task<List<MovimientoBancarioTeckioDTO>> ObtenerTodos()
        {
            var objetos = await _repository.ObtenerTodos();
            return _mapper.Map<List<MovimientoBancarioTeckioDTO>>(objetos);
        }

        public async Task<bool> ActualizarEstatusXId(int IdMovimientoBancario, int estatus)
        {
            var objeto = await _repository.Obtener(z => z.Id == IdMovimientoBancario);
            objeto.Estatus = estatus;
            var resultado = await _repository.Editar(objeto);
            return resultado;
        }

        public async Task<MovimientoBancarioTeckioDTO> ObtenerXId(int Id)
        {
            var objeto = await _repository.Obtener(z => z.Id == Id);
            return _mapper.Map<MovimientoBancarioTeckioDTO>(objeto);
        }

        public async Task<decimal> ObtenerMontoXIdCuentaBancariaYAnioYMes(DateTime fecha, int IdCuentaBancaria)
        {
            var objetos = await _repository.ObtenerTodos(z => z.FechaAplicacion.Year == fecha.Year && z.FechaAplicacion.Month == 
            fecha.Month && z.Estatus == 2 && z.IdCuentaBancariaEmpresa == IdCuentaBancaria);
            var lista = _mapper.Map<List<MovimientoBancarioTeckioDTO>>(objetos);
            if (lista.Count() <= 0) {
                return 0;
            }
            var Deposito = lista.Where(z => z.TipoDeposito == 1).Sum(z => z.MontoTotal);
            var Retiro = lista.Where(z => z.TipoDeposito == 2).Sum(z => z.MontoTotal);

            return Deposito - Retiro;

        }
    }
}
