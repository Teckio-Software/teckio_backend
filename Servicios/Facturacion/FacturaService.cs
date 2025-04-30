using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class FacturaService<T> : IFacturaService<T> where T : DbContext
    {
        private readonly IGenericRepository<Factura, T> _repository;
        private readonly IMapper _mapper;
        public FacturaService(
            IGenericRepository<Factura, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<bool> Crear(FacturaDTO parametro)
        {
            throw new NotImplementedException();
        }

        public async Task<FacturaDTO> CrearYObtener(FacturaDTO parametro)
        {
            parametro.EstatusEnviadoCentroCostos = false;
            var objetoCreado = await _repository.Crear(_mapper.Map<Factura>(parametro));
            if (objetoCreado.Id == 0)
                return new FacturaDTO();
            return _mapper.Map<FacturaDTO>(objetoCreado);
        }

        public Task<bool> Editar(FacturaDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EsEnviado(int IdFactura)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaDTO>> ObtenSoloValidas()
        {
            throw new NotImplementedException();
        }

        public async Task<List<FacturaDTO>> ObtenTodos()
        {
            var lista = await _repository.ObtenerTodos();
            return _mapper.Map<List<FacturaDTO>>(lista);
        }

        public Task<List<FacturaDTO>> ObtenTodosT()
        {
            throw new NotImplementedException();
        }

        public Task<FacturaDTO> ObtenXId(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<FacturaDTO> ObtenXIdT(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaDTO>> ObtenXRfcEmisor(string rfcEmisor)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaDTO>> ObtenXRfcReceptor(string rfcReceptor)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaDTO>> ObtenXUuid(string uuid)
        {
            throw new NotImplementedException();
        }
    }
}
