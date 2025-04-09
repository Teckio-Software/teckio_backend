using AutoMapper;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class FacturaService<T> : IFacturaService<T>  where T : DbContext
    {
        private readonly IGenericRepository<Factura, T> _repository;
        private readonly IMapper _mapper;
        public FacturaService(
            IGenericRepository<Factura, T> repository,
            IMapper mapper
            ) {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<bool> Crear(FacturaBaseDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<FacturaBaseDTO> CrearYObtener(FacturaBaseDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(FacturaBaseDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EsEnviado(int IdFactura)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaBaseDTO>> ObtenSoloValidas()
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaBaseDTO>> ObtenTodos()
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaBaseDTO>> ObtenTodosT()
        {
            throw new NotImplementedException();
        }

        public Task<FacturaBaseDTO> ObtenXId(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<FacturaBaseDTO> ObtenXIdT(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaBaseDTO>> ObtenXRfcEmisor(string rfcEmisor)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaBaseDTO>> ObtenXRfcReceptor(string rfcReceptor)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaBaseDTO>> ObtenXUuid(string uuid)
        {
            throw new NotImplementedException();
        }
    }
}
