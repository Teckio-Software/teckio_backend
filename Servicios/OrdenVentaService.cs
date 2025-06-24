using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class OrdenVentaService<T> : IOrdenVentaService<T> where T : DbContext
    {
        private readonly IGenericRepository<OrdenVentum, T> _repository;
        private readonly IMapper _mapper;
        public OrdenVentaService(
            IGenericRepository<OrdenVentum, T> repository,
            IMapper mapper
            ) { 
            _repository = repository;
            _mapper = mapper;
        }
        public Task<RespuestaDTO> Crear(OrdenVentaDTO modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<OrdenVentaDTO> CrearYObtener(OrdenVentaDTO modelo)
        {
            var respuesta = await _repository.Crear(_mapper.Map<OrdenVentum>(modelo));
            return _mapper.Map<OrdenVentaDTO>(respuesta);
        }

        public Task<RespuestaDTO> Editar(OrdenVentaDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDTO> Eliminar(OrdenVentaDTO modelo)
        {
            throw new NotImplementedException();
        }
    }
}
