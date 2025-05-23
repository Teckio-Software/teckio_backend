using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Modelos.Facturaion;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class FacturaImpuestosService<T> : IFacturaImpuestosService<T> where T : DbContext
    {
        private readonly IGenericRepository<FacturaImpuesto, T> _repository;
        private readonly IMapper _mapper;

        public FacturaImpuestosService(
            IGenericRepository<FacturaImpuesto, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<FacturaImpuestosDTO>> ObtenerTodos()
        {
            try
            {
                var query = await _repository.ObtenerTodos();
                return _mapper.Map<List<FacturaImpuestosDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<FacturaImpuestosDTO>();
            }
        }

        public async Task<FacturaImpuestosDTO> ObtenerXId(int Id)
        {
            try
            {
                var query = await _repository.Obtener(z => z.Id == Id);
                return _mapper.Map<FacturaImpuestosDTO>(query);
            }
            catch (Exception ex)
            {
                return new FacturaImpuestosDTO();
            }
        }

        public async Task<FacturaImpuestosDTO> CrearYObtener(FacturaImpuestosDTO registro)
        {
            var respuesta = await _repository.Crear(_mapper.Map<FacturaImpuesto>(registro));
            return _mapper.Map<FacturaImpuestosDTO>(respuesta);
        }

        public async Task<RespuestaDTO> Editar(FacturaImpuestosDTO registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenerXId(registro.Id);
                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Factura impuesto no existe";
                    return respuesta;
                }
                objetoEncontrado.IdCategoriaImpuesto = registro.IdCategoriaImpuesto;
                objetoEncontrado.IdFactura = registro.IdFactura;
                objetoEncontrado.IdClasificacionImpuesto = registro.IdClasificacionImpuesto;
                objetoEncontrado.TotalImpuesto = registro.TotalImpuesto;
                var modelo = _mapper.Map<FacturaImpuesto>(objetoEncontrado);
                respuesta.Estatus = await _repository.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Factura impuesto editado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal al editar Factura impuesto ";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            var objetoEncontrado = await ObtenerXId(Id);

            if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Factura impuesto no existe";
                return respuesta;
            }
            var modelo = _mapper.Map< FacturaImpuesto> (objetoEncontrado);
            respuesta.Estatus = await _repository.Eliminar(modelo);

            if (!respuesta.Estatus)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se pudo eliminar";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Factura impuestos eliminado";
            return respuesta;
        }

        public async Task<List<FacturaImpuestosDTO>> ObtenerXIdFactura(int IdFactura)
        {
            var lista = await _repository.ObtenerTodos(z => z.IdFactura == IdFactura);
            return _mapper.Map<List<FacturaImpuestosDTO>>(lista);
        }
    }
}
