using AutoMapper;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios.Facturacion
{
    public class CategoriaImpuestoService<T> : ICategoriaImpuestoService<T> where T : DbContext
    {
        private readonly IGenericRepository<CategoriaImpuesto, T> _repository;
        private readonly IMapper _mapper;

        public CategoriaImpuestoService(
            IGenericRepository<CategoriaImpuesto, T> repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoriaImpuestoDTO>> ObtenerTodos()
        {
            try
            {
                var query = await _repository.ObtenerTodos();
                return _mapper.Map<List<CategoriaImpuestoDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<CategoriaImpuestoDTO>();
            }
        }

        public async Task<CategoriaImpuestoDTO> ObtenerXId(int Id)
        {
            try
            {
                var query = await _repository.Obtener(z => z.Id == Id);
                return _mapper.Map<CategoriaImpuestoDTO>(query);
            }
            catch (Exception ex)
            {
                return new CategoriaImpuestoDTO();
            }
        }

        public async Task<CategoriaImpuestoDTO> CrearYObtener(CategoriaImpuestoDTO registro)
        {
            var respuesta = await _repository.Crear(_mapper.Map<CategoriaImpuesto>(registro));
            return _mapper.Map<CategoriaImpuestoDTO>(respuesta);
        }

        public async Task<RespuestaDTO> Editar(CategoriaImpuestoDTO registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenerXId(registro.Id);
                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Categoria no existe";
                    return respuesta;
                }
                objetoEncontrado.Tipo = registro.Tipo;
                var modelo = _mapper.Map<CategoriaImpuesto>(objetoEncontrado);
                respuesta.Estatus = await _repository.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Categoria editado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal al editar Categoria";
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
                respuesta.Descripcion = "Categoria no existe";
                return respuesta;
            }
            var modelo = _mapper.Map<CategoriaImpuesto>(objetoEncontrado);
            respuesta.Estatus = await _repository.Eliminar(modelo);

            if (!respuesta.Estatus)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se pudo eliminars";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Categoria eliminado";
            return respuesta;
        }
    }
}
