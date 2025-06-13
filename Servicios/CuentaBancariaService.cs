using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class CuentaBancariaService<T> : ICuentaBancariaService<T> where T : DbContext
    {
        private readonly IGenericRepository<CuentaBancaria, T> _Repositorio;
        private readonly IMapper _Mapper;
        public CuentaBancariaService(IGenericRepository<CuentaBancaria, T> _Repositorio, IMapper _Mapper) { 
            this._Repositorio = _Repositorio;
            this._Mapper = _Mapper; 
        }

        public async Task<bool> Crear(CuentaBancariaDTO modelo)
        {
            if (string.IsNullOrEmpty(modelo.NumeroCuenta) || modelo.NumeroCuenta.Length > 10
                || string.IsNullOrEmpty(modelo.Clabe) || modelo.Clabe.Length > 18)
            {
                return false;
            }
            var objeto = await _Repositorio.Crear(_Mapper.Map<CuentaBancaria>(modelo));
            if (objeto.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<CuentaBancariaDTO> CrearYObtener(CuentaBancariaDTO modelo)
        {
            if (string.IsNullOrEmpty(modelo.NumeroCuenta) || modelo.NumeroCuenta.Length > 20
                || string.IsNullOrEmpty(modelo.Clabe) || modelo.Clabe.Length > 18)
            {
                return new CuentaBancariaDTO();
            }
            var objeto = await _Repositorio.Crear(_Mapper.Map<CuentaBancaria>(modelo));
            if (objeto.Id <= 0)
            {
                return new CuentaBancariaDTO();
            }
            return _Mapper.Map<CuentaBancariaDTO>(objeto);
        }

        public async Task<bool> Editar(CuentaBancariaDTO modelo)
        {
            if (string.IsNullOrEmpty(modelo.NumeroCuenta) || modelo.NumeroCuenta.Length > 20
                || string.IsNullOrEmpty(modelo.Clabe) || modelo.Clabe.Length > 18)
            {
                return false;
            }
            var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
            if (objetoEncontrado.Id <= 0)
            {
                return false;
            }

            objetoEncontrado.NumeroCuenta = modelo.NumeroCuenta;
            objetoEncontrado.Clabe = modelo.Clabe;
            var respuesta = await _Repositorio.Editar(objetoEncontrado);
            return respuesta;
        }

        public async Task<bool> Eliminar(int Id)
        {
            var objetoEncontrado = await _Repositorio.Obtener(u => Id == u.Id);
            if (objetoEncontrado == null || objetoEncontrado.Id <= 0) { return false; }
            var respuesta = await _Repositorio.Eliminar(objetoEncontrado);
            return respuesta;
        }

        public async Task<List<CuentaBancariaDTO>> ObtenTodos()
        {
            var lista = await _Repositorio.ObtenerTodos();
            return _Mapper.Map<List<CuentaBancariaDTO>>(lista);
        }

        public async Task<CuentaBancariaDTO> ObtenXId(int Id)
        {
            var lista = await _Repositorio.Obtener(u => u.Id == Id);
            return _Mapper.Map<CuentaBancariaDTO>(lista);
        }

        public async Task<List<CuentaBancariaDTO>> ObtenXIdContratista(int IdContratista)
        {
            var lista = await _Repositorio.ObtenerTodos(u => u.IdContratista == IdContratista);
            return _Mapper.Map<List<CuentaBancariaDTO>>(lista);
        }
    }
}
