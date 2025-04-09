using AutoMapper;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class FacturaDetalleService<T> : IFacturaDetalleService<T> where T : DbContext
    {
        private readonly IGenericRepository<FacturaDetalle, T> _repository;
        private readonly IMapper _mapper;
        public FacturaDetalleService(
            IGenericRepository<FacturaDetalle, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<bool> Crear(FacturaDetalleDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<FacturaDetalleDTO> CrearYObtener(FacturaDetalleDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(FacturaDetalleDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaDetalleDTO>> ObtenTodos()
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaDetalleDTO>> ObtenXCantidad(decimal Cantidad)
        {
            throw new NotImplementedException();
        }

        public Task<FacturaDetalleDTO> ObtenXId(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaDetalleDTO>> ObtenXIdFactura(int IdFactura)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaDetalleDTO>> ObtenXImporte(decimal Importe)
        {
            throw new NotImplementedException();
        }

        public Task<List<FacturaDetalleDTO>> ObtenXUnidadSat(string UnidadSat)
        {
            throw new NotImplementedException();
        }
    }
}
