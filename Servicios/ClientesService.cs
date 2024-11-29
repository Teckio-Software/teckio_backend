using AutoMapper;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class ClientesService<T> : IClientesService<T> where T : DbContext
    {
        private readonly IGenericRepository<Clientes, T> _Repositorio;
        private readonly IMapper _Mapper;

        public ClientesService(
            IGenericRepository<Clientes, T> repositorio
            , IMapper mapper){
                _Repositorio = repositorio;
                _Mapper = mapper;
            }

        public async Task<bool> Crear(ClienteDTO modelo)
        {
            if (!await validacionoblogatorios(modelo)) 
            {
                return false;
            }
            modelo = _Mapper.Map<ClienteDTO>(modelo);
            var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Clientes>(modelo));
                if(objetoCreado.Id <= 0)
                {
                    return false;
                }
                return true;
        }

        public async Task<ClienteDTO> CrearYObtener(ClienteDTO modelo)
        {
            if (!await validacionoblogatorios(modelo))
            {
                return new ClienteDTO();
            }
            modelo = _Mapper.Map<ClienteDTO>(modelo);
            var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Clientes>(modelo));
                if (objetoCreado.Id == 0)
                {
                    return new ClienteDTO();
                }
                return _Mapper.Map<ClienteDTO>(objetoCreado);
        }

        public async Task<bool> Editar(ClienteDTO parametro)
        {
            if (!await validacionoblogatorios(parametro))
            {
                return false;
            }
            parametro = _Mapper.Map<ClienteDTO>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == parametro.Id);
                if (objetoEncontrado == null)
                {
                    return false;
                }
                objetoEncontrado.RazonSocial = parametro.RazonSocial;
                objetoEncontrado.Rfc = parametro.Rfc;
                objetoEncontrado.Email = parametro.Email;
                objetoEncontrado.Telefono = parametro.Telefono;
                objetoEncontrado.RepresentanteLegal = parametro.RepresentanteLegal;
                objetoEncontrado.NoExterior = parametro.NoExterior;
                objetoEncontrado.CodigoPostal = parametro.CodigoPostal;
                objetoEncontrado.IdCuentaContable = parametro.IdCuentaContable;
                objetoEncontrado.IdIvaTrasladado = parametro.IdIvaTrasladado;
                objetoEncontrado.IdIvaPorTrasladar = parametro.IdIvaPorTrasladar;
                objetoEncontrado.IdCuentaAnticipos = parametro.IdCuentaAnticipos;
                objetoEncontrado.IdIvaExento = parametro.IdIvaExento;
                objetoEncontrado.IdIvaGravable = parametro.IdIvaGravable;
                objetoEncontrado.IdRetensionIsr = parametro.IdRetensionIsr;
                objetoEncontrado.IdIeps = parametro.IdIeps;
                objetoEncontrado.IdIvaRetenido = parametro.IdIvaRetenido;

                return await _Repositorio.Editar(objetoEncontrado);
        }

        public async Task<List<ClienteDTO>> ObtenTodos()
        {
                var lista = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<ClienteDTO>>(lista);
        }

        public async Task<ClienteDTO> ObtenXId(int id)
        {
                var lista = await _Repositorio.ObtenerTodos(z => z.Id == id);
                return _Mapper.Map<ClienteDTO>(lista);
        }

        public async Task<bool> Eliminar(int Id)
        {
            var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == Id);
                if(objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                return false;
                }
            return await _Repositorio.Eliminar(objetoEncontrado);
        }

        private async Task<bool> validacionoblogatorios(ClienteDTO modelo)
        {
            if (string.IsNullOrEmpty(modelo.RazonSocial) || modelo.RazonSocial.Length > 150 ||
                string.IsNullOrEmpty(modelo.Rfc) || modelo.Rfc.Length > 15 ||
                string.IsNullOrEmpty(modelo.Domicilio) || modelo.Domicilio.Length > 100 ||
                string.IsNullOrEmpty(modelo.Colonia) || modelo.Colonia.Length > 100 ||
                string.IsNullOrEmpty(modelo.Municipio) || modelo.Municipio.Length > 100 ||
                string.IsNullOrEmpty(modelo.CodigoPostal) || modelo.CodigoPostal.Length > 6)
            {
                return false;
            }
            return true;
        }
    }
}
