using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class ProductoXEntradaProduccionAlmacenService<T> : IProductoXEntradaProduccionAlmacenService<T> where T : DbContext
    {
        private readonly IGenericRepository<ProductosXentradaProduccionAlmacen, T> _repository;
        private readonly IMapper _mapper;
        public ProductoXEntradaProduccionAlmacenService(
            IGenericRepository<ProductosXentradaProduccionAlmacen, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<RespuestaDTO> Crear(ProductosXEntradaProduccionAlmacenDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<ProductosXEntradaProduccionAlmacenDTO> CrearYObtener(ProductosXEntradaProduccionAlmacenDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDTO> Editar(ProductosXEntradaProduccionAlmacenDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDTO> Eliminar(ProductosXEntradaProduccionAlmacenDTO modelo)
        {
            throw new NotImplementedException();
        }
    }
}
