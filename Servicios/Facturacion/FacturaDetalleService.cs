using AutoMapper;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
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

        public async Task<bool> Crear(FacturaDetalleDTO parametro)
        {
            try
            {
                var objetoCreado = await _repository.Crear(_mapper.Map<FacturaDetalle>(parametro));
                return objetoCreado.Id > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<FacturaDetalleDTO> CrearYObtener(FacturaDetalleDTO parametro)
        {
            var objetoCreado = await _repository.Crear(_mapper.Map<FacturaDetalle>(parametro));
            if (objetoCreado.Id == 0)
                return new FacturaDetalleDTO();
            return _mapper.Map<FacturaDetalleDTO>(objetoCreado);
        }

        public async Task<bool> Editar(FacturaDetalleDTO parametro)
        {
            try
            {
                var objetoObtenido = await _repository.Obtener(f=>f.Id == parametro.Id);
                if (objetoObtenido.Id <= 0)
                {
                    return false;
                }
                objetoObtenido.Cantidad = parametro.Cantidad;
                objetoObtenido.PrecioUnitario = parametro.PrecioUnitario;
                objetoObtenido.Importe = parametro.Importe;
                objetoObtenido.Descuento = parametro.Descuento;
                objetoObtenido.IdProductoYservicio = parametro.IdProductoYservicio;
                objetoObtenido.UnidadSat = parametro.UnidadSat;
                var objetoEditado = await _repository.Editar(objetoObtenido);
                return objetoEditado;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Eliminar(int Id)
        {
            try
            {
                var objetoObtenido = await _repository.Obtener(f => f.Id == Id);
                if (objetoObtenido.Id <= 0)
                {
                    return false;
                }
                
                var objetoEditado = await _repository.Eliminar(objetoObtenido);
                return objetoEditado;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<FacturaDetalleDTO>> ObtenTodos()
        {
            try
            {
                var lista = await _repository.ObtenerTodos();
                if (lista.Count > 0)
                {
                    return _mapper.Map<List<FacturaDetalleDTO>>(lista);
                }
                else
                {
                    return new List<FacturaDetalleDTO>();
                }
            }
            catch
            {
                return new List<FacturaDetalleDTO>();
            }
        }

        public async Task<List<FacturaDetalleDTO>> ObtenXCantidad(decimal Cantidad)
        {
            try
            {
                var lista = await _repository.ObtenerTodos(f=>f.Cantidad == Cantidad);
                if (lista.Count > 0)
                {
                    return _mapper.Map<List<FacturaDetalleDTO>>(lista);
                }
                else
                {
                    return new List<FacturaDetalleDTO>();
                }
            }
            catch
            {
                return new List<FacturaDetalleDTO>();
            }
        }

        public async Task<FacturaDetalleDTO> ObtenXId(int Id)
        {
            try
            {
                var objeto = await _repository.Obtener(f => f.Id == Id);
                return _mapper.Map<FacturaDetalleDTO>(objeto);
            }
            catch
            {
                return new FacturaDetalleDTO();
            }
        }

        public async Task<List<FacturaDetalleDTO>> ObtenXIdFactura(int IdFactura)
        {
            var lista = await _repository.ObtenerTodos(z => z.IdFactura == IdFactura);
            return _mapper.Map<List<FacturaDetalleDTO>>(lista);
        }

        public async Task<List<FacturaDetalleDTO>> ObtenXImporte(decimal Importe)
        {
            try
            {
                var lista = await _repository.ObtenerTodos(f => f.Importe == Importe);
                if (lista.Count > 0)
                {
                    return _mapper.Map<List<FacturaDetalleDTO>>(lista);
                }
                else
                {
                    return new List<FacturaDetalleDTO>();
                }
            }
            catch
            {
                return new List<FacturaDetalleDTO>();
            }
        }

        public async Task<List<FacturaDetalleDTO>> ObtenXUnidadSat(string UnidadSat)
        {
            try
            {
                var lista = await _repository.ObtenerTodos(f => f.UnidadSat == UnidadSat);
                if (lista.Count > 0)
                {
                    return _mapper.Map<List<FacturaDetalleDTO>>(lista);
                }
                else
                {
                    return new List<FacturaDetalleDTO>();
                }
            }
            catch
            {
                return new List<FacturaDetalleDTO>();
            }
        }
    }
}
