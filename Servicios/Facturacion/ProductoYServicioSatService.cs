using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos.Facturacion;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class ProductoYServicioSatService<T> : IProductoYServicioSatService<T> where T : DbContext
    {
        private readonly IGenericRepository<ProductoYservicioSat, T> _repository;
        private readonly IMapper _mapper;
        public ProductoYServicioSatService(
            IGenericRepository<ProductoYservicioSat, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Crear(ProductoYServicioSatDTO parametro)
        {
            var crear = await _repository.Crear(_mapper.Map<ProductoYservicioSat>(parametro));
            if (crear.Id <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task<ProductoYServicioSatDTO> ObtenerXClave(string clave)
        {
            var objeto = await _repository.Obtener(z => z.Clave == clave);
            if (objeto.Id <= 0)
            {
                return new ProductoYServicioSatDTO();
            }
            return _mapper.Map<ProductoYServicioSatDTO>(objeto);
        }

        public async Task<ProductoYServicioSatDTO> ObtenerXId(int Id)
        {
            var objeto = await _repository.Obtener(z => z.Id == Id);
            if (objeto.Id <= 0)
            {
                return new ProductoYServicioSatDTO();
            }
            return _mapper.Map<ProductoYServicioSatDTO>(objeto);
        }
    }
}
