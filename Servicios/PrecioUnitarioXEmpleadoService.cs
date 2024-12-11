using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class PrecioUnitarioXEmpleadoService<T> : IPrecioUnitarioXEmpleadoService<T> where T : DbContext
    {
        IGenericRepository<PrecioUnitarioXEmpleado, T> _Repository;
        IMapper _Mapper;
        public PrecioUnitarioXEmpleadoService(
            IGenericRepository<PrecioUnitarioXEmpleado, T> Repository,
            IMapper Mapper
            ) { 
            _Repository = Repository;
            _Mapper = Mapper;
        }

        public async Task<RespuestaDTO> Crear(PrecioUnitarioXEmpleadoDTO objeto)
        {
            var respuesta = new RespuestaDTO();
            var resultado = await _Repository.Crear(_Mapper.Map<PrecioUnitarioXEmpleado>(objeto));
            if (resultado.Id <= 0)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salio mal al registrar";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se guardo correctamente";
            return respuesta;
        }

        public async Task<RespuestaDTO> CrearMultiple(List<PrecioUnitarioXEmpleadoDTO> lista)
        {
            var respuesta = new RespuestaDTO();
            var multiple = await _Repository.CrearMultiple(_Mapper.Map<List<PrecioUnitarioXEmpleado>>(lista));
            if (!multiple) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salio mal al registrar";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se guardo correctamente";
            return respuesta;
        }

        public async Task<List<PrecioUnitarioXEmpleadoDTO>> ObtenerXIdEmpleado(int IdEmpleado)
        {
            var lista = await _Repository.ObtenerTodos(z => z.IdEmpleado == IdEmpleado);
            return _Mapper.Map<List<PrecioUnitarioXEmpleadoDTO>>(lista);
        }

        public async Task<List<PrecioUnitarioXEmpleadoDTO>> ObtenerXIdEmpleadoYIdProyceto(int IdEmpleado, int IdProyceto)
        {
            var lista = await _Repository.ObtenerTodos(z => z.IdEmpleado == IdEmpleado && z.IdProyceto == IdProyceto);
            return _Mapper.Map<List<PrecioUnitarioXEmpleadoDTO>>(lista);
        }
    }
}
