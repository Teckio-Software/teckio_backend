using AutoMapper;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class FacturaDetalleImpuestoService<T> : IFacturaDetalleImpuestoService<T> where T : DbContext
    {
        private readonly IGenericRepository<FacturaDetalleImpuesto, T> _repository;
        private readonly IMapper _mapper;
        public FacturaDetalleImpuestoService(
            IGenericRepository<FacturaDetalleImpuesto, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<bool> Crear(FacturaDetalleImpuestoDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<FacturaDetalleImpuestoDTO> CrearYObtener(FacturaDetalleImpuestoDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(FacturaDetalleImpuestoDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaDetalleImpuestoDTO>> ObtenTodos()
        {
            throw new NotImplementedException();
        }

        public Task<FacturaDetalleImpuestoDTO> ObtenXId(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaDetalleImpuestoDTO>> ObtenXIdClasificacionImpuesto(int IdClasificacionImpuesto)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaDetalleImpuestoDTO>> ObtenXIdFacturaDetalle(int IdFacturaDetalle)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaDetalleImpuestoDTO>> ObtenXIdTipoFactor(int IdTipoFactor)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaDetalleImpuestoDTO>> ObtenXIdTipoImpuesto(int IdTipoImpuesto)
        {
            throw new NotImplementedException();
        }
    }
}
