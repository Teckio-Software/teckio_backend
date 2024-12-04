using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class CuentaBancariaClienteService<T> : ICuentaBancariaClienteService<T> where T : DbContext
    {
        private readonly IGenericRepository<CuentaBancariaCliente, T> _Repositorio;
        private readonly IMapper _Mapper;

        public CuentaBancariaClienteService(IGenericRepository<CuentaBancariaCliente, T> _Repositorio, IMapper _Mapper)
        {
            this._Repositorio = _Repositorio;
            this._Mapper = _Mapper;
        }
        public async Task<bool> Crear(CuentaBancariaClienteDTO modelo)
        {
            if (string.IsNullOrEmpty(modelo.NumeroCuenta) || modelo.NumeroCuenta.Length > 20
                || string.IsNullOrEmpty(modelo.Clabe) || modelo.Clabe.Length > 18)
            {
                return false;
            }
            var objeto = await _Repositorio.Crear(_Mapper.Map<CuentaBancariaCliente>(modelo));
            if (objeto.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public Task<CuentaBancariaClienteDTO> CrearYObtener(CuentaBancariaClienteDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(CuentaBancariaClienteDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CuentaBancariaClienteDTO>> ObtenTodos()
        {
            throw new NotImplementedException();
        }

        public Task<CuentaBancariaClienteDTO> ObtenXId(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CuentaBancariaClienteDTO>> ObtenXIdCliente(int IdCliente)
        {
            var lista = await _Repositorio.ObtenerTodos(u => u.IdCliente == IdCliente);
            return _Mapper.Map<List<CuentaBancariaClienteDTO>>(lista);
        }
    }
}
