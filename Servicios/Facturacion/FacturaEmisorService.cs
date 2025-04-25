using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class FacturaEmisorService<TContext> : IFacturaEmisorService<TContext> where TContext : DbContext
    {
        private readonly IGenericRepository<FacturaEmisor, TContext> _Repositorio;
        public readonly IMapper _Mapper;
        public FacturaEmisorService(IGenericRepository<FacturaEmisor, TContext> repositorio, IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<List<FacturaEmisorDTO>> ObtenTodos()
        {
            var lista = await _Repositorio.ObtenerTodos();
            return _Mapper.Map<List<FacturaEmisorDTO>>(lista);
        }

        public async Task<FacturaEmisorDTO> ObtenXIdFactura(int IdFactura)
        {
            var lista = await _Repositorio.Obtener(z => z.IdFactura == IdFactura);
            return _Mapper.Map<FacturaEmisorDTO>(lista);
        }

        public async Task<List<FacturaEmisorDTO>> ObtenXRfc(string Rfc)
        {
            var lista = await _Repositorio.ObtenerTodos(z => z.Rfc == Rfc);
            return _Mapper.Map<List<FacturaEmisorDTO>>(lista);
        }

        public async Task<FacturaEmisorDTO> ObtenXId(int Id)
        {
            var lista = await _Repositorio.Obtener(z => z.Id == Id);
            return _Mapper.Map<FacturaEmisorDTO>(lista);
        }

        public async Task<bool> Crear(FacturaEmisorDTO parametro)
        {
            var objetoCreado = await _Repositorio.Crear(_Mapper.Map<FacturaEmisor>(parametro));
            if (objetoCreado.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<FacturaEmisorDTO> CrearYObtener(FacturaEmisorDTO parametro)
        {
            var objetoCreado = await _Repositorio.Crear(_Mapper.Map<FacturaEmisor>(parametro));
            if (objetoCreado.Id == 0)
                return new FacturaEmisorDTO();
            return _Mapper.Map<FacturaEmisorDTO>(objetoCreado);
        }

        public async Task<bool> Editar(FacturaEmisorDTO parametro)
        {
            var modelo = _Mapper.Map<CategoriaImpuesto>(parametro);
            var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
            if (objetoEncontrado == null)
            {
                return false;
            }
            var respuesta = await _Repositorio.Editar(objetoEncontrado);

            return respuesta;
        }

        public async Task<bool> Eliminar(int Id)
        {
            var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == Id);
            if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
            {
                return false;
            }
            var respuesta = await _Repositorio.Eliminar(objetoEncontrado);
            return respuesta;
        }
    }
}
