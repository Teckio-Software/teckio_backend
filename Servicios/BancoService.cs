using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class BancoService<T> : IBancoService<T> where T : DbContext
    {
        private readonly IGenericRepository<Banco, T> _Repositorio;

        private readonly IMapper _Mapper;
        public BancoService(IGenericRepository<Banco, T> _Repositorio, IMapper _Mapper) {
            this._Repositorio = _Repositorio;
            this._Mapper = _Mapper;
        }

        public async Task<bool> Crear(BancoDTO banco)
        {
            if (string.IsNullOrEmpty(banco.Clave) || banco.Clave.Length > 4
                || string.IsNullOrEmpty(banco.Nombre) || banco.Nombre.Length >100
                || string.IsNullOrEmpty(banco.RazonSocial) || banco.RazonSocial.Length > 300)
            {
                return false;
            }
            var objeto = await _Repositorio.Crear(_Mapper.Map<Banco>(banco));
            if(objeto.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<BancoDTO> CrearYObtener(BancoDTO banco)
        {
            if (string.IsNullOrEmpty(banco.Clave) || banco.Clave.Length > 4
                || string.IsNullOrEmpty(banco.Nombre) || banco.Nombre.Length > 100
                || string.IsNullOrEmpty(banco.RazonSocial) || banco.RazonSocial.Length > 300)
            {
                return new BancoDTO();
            }
            var objeto = await _Repositorio.Crear(_Mapper.Map<Banco>(banco));
            if (objeto.Id <= 0)
            {
                return new BancoDTO();
            }
            return _Mapper.Map<BancoDTO>(banco);
        }

        public async Task<bool> Editar(BancoDTO banco)
        {
            var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == banco.Id);
            if (objetoEncontrado.Id <= 0)
            {
                return false;
            }

            objetoEncontrado.Clave = banco.Clave;
            objetoEncontrado.Nombre = banco.Nombre;
            objetoEncontrado.RazonSocial = banco.RazonSocial;
            var respuesta = await _Repositorio.Editar(objetoEncontrado);
            return respuesta;
        }

        public async Task<bool> Eliminar(int id)
        {
            var objetoEncontrado = await _Repositorio.Obtener(u => id == u.Id);
            if(objetoEncontrado == null || objetoEncontrado.Id <= 0) {  return false; }
            var respuesta = await _Repositorio.Eliminar(objetoEncontrado);
            return respuesta;
        }

        public async Task<List<BancoDTO>> ObtenTodos()
        {
            var lista = await _Repositorio.ObtenerTodos();
            return _Mapper.Map<List<BancoDTO>>(lista);
        }

        public async Task<BancoDTO> ObtenXClave(string clave)
        {
            var lista = await _Repositorio.Obtener(u => u.Clave == clave);
            return _Mapper.Map<BancoDTO>(lista);
        }

        public async Task<BancoDTO> ObtenXId(int id)
        {
            var lista = await _Repositorio.Obtener(u => u.Id == id);
            return _Mapper.Map<BancoDTO>(lista);
        }
    }
}
