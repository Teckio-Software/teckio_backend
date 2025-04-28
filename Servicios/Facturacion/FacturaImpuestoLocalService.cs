using AutoMapper;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class FacturaImpuestoLocalService<T> : IFacturaImpuestoLocalService<T> where T : DbContext
    {
        private readonly IGenericRepository<FacturaImpuestosLocal, T> _repository;
        private readonly IMapper _mapper;

        public FacturaImpuestoLocalService(
            IGenericRepository<FacturaImpuestosLocal, T> repository,
IMapper mapper
            ) {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Crear(FacturaImpuestosLocalesDTO parametro)
        {
            var objeto = await _repository.Crear(_mapper.Map<FacturaImpuestosLocal>(parametro));
            if (objeto.Id <= 0) {
                return false;
            }
            return true;
        }

        public Task<FacturaImpuestosLocalesDTO> CrearYObtener(FacturaImpuestosLocalesDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(FacturaImpuestosLocalesDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaImpuestosLocalesDTO>> ObtenTodos()
        {
            throw new NotImplementedException();
        }

        public Task<FacturaImpuestosLocalesDTO> ObtenXId(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaImpuestosLocalesDTO>> ObtenXIdCategoriaImpuesto(int IdCategoriaImpuesto)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaImpuestosLocalesDTO>> ObtenXIdClasificacionImpuesto(int IdClasificacion)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaImpuestosLocalesDTO>> ObtenXIdFactura(int IdFactura)
        {
            throw new NotImplementedException();
        }
    }
}
