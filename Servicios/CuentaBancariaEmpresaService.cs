using AutoMapper;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class CuentaBancariaEmpresaService<T> : ICuentaBancariaEmpresaService<T> where T : DbContext
    {
        private readonly IGenericRepository<CuentaBancariaEmpresa, T> _Repositorio;
        private readonly IMapper _Mapper;
        public CuentaBancariaEmpresaService(IGenericRepository<CuentaBancariaEmpresa, T> _Repositorio, IMapper _Mapper)
        {
            this._Repositorio = _Repositorio;
            this._Mapper = _Mapper;
        }

        public async Task<bool> Crear(CuentaBancariaEmpresasDTO modelo)
        {
            if (string.IsNullOrEmpty(modelo.NumeroCuenta) || modelo.NumeroCuenta.Length > 20
             || string.IsNullOrEmpty(modelo.Clabe) || modelo.Clabe.Length > 18)
            {
                return false;
            }
            var objeto = await _Repositorio.Crear(_Mapper.Map<CuentaBancariaEmpresa>(modelo));
            if (objeto.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public Task<CuentaBancariaEmpresasDTO> CrearYObtener(CuentaBancariaEmpresasDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(CuentaBancariaEmpresasDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CuentaBancariaEmpresasDTO>> ObtenTodos()
        {
            var lista = await _Repositorio.ObtenerTodos();
            return _Mapper.Map<List<CuentaBancariaEmpresasDTO>>(lista);
        }

        public Task<CuentaBancariaEmpresasDTO> ObtenXId(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
