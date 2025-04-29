using AutoMapper;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion 
{
    public class FacturaComplementoPagoService<TContext> : IFacturaComplementoPagoService<TContext> where TContext : DbContext
    {
        private readonly IGenericRepository<FacturaComplementoPago, TContext> _Repositorio;
        private readonly IMapper _Mapper;
        public FacturaComplementoPagoService(
            IGenericRepository<FacturaComplementoPago, TContext> Repositorio
            , IMapper Mapper
            )
        {
            _Mapper = Mapper;
            _Repositorio = Repositorio;
        }
        public async Task<bool> Crear(FacturaComplementoPagoDTO parametro)
        {
            var objetoCreado = await _Repositorio.Crear(_Mapper.Map<FacturaComplementoPago>(parametro));
            if (objetoCreado.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<FacturaComplementoPagoDTO> CrearYObtener(FacturaComplementoPagoDTO parametro)
        {
            var objetoCreado = await _Repositorio.Crear(_Mapper.Map<FacturaComplementoPago>(parametro));
            if (objetoCreado.Id == 0)
                return new FacturaComplementoPagoDTO();
            return _Mapper.Map<FacturaComplementoPagoDTO>(objetoCreado);
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

        public async Task<List<FacturaComplementoPagoDTO>> ObtenTodos()
        {
            var lista = await _Repositorio.ObtenerTodos();
            return _Mapper.Map<List<FacturaComplementoPagoDTO>>(lista);
        }

        public async Task<FacturaComplementoPagoDTO> ObtenXId(int Id)
        {
            var lista = await _Repositorio.Obtener(z => z.Id == Id);
            return _Mapper.Map<FacturaComplementoPagoDTO>(lista);
        }

        public async Task<List<FacturaComplementoPagoDTO>> ObtenXIdFactura(int IdFactura)
        {
            var lista = await _Repositorio.ObtenerTodos(z => z.IdFactura == IdFactura);
            return _Mapper.Map<List<FacturaComplementoPagoDTO>>(lista);
        }

        public async Task<List<FacturaComplementoPagoDTO>> ObtenXUuidFactura(string Uuid)
        {
            var lista = await _Repositorio.ObtenerTodos(z => z.Uuid == Uuid);
            return _Mapper.Map<List<FacturaComplementoPagoDTO>>(lista);
        }
    }
}
