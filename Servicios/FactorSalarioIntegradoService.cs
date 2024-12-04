using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class FactorSalarioIntegradoService<T> : IFactorSalarioIntegradoService<T> where T : DbContext
    {
        private readonly IGenericRepository<FactorSalarioIntegrado, T> _Repositorio;
        private readonly IMapper _Mapper;
        public FactorSalarioIntegradoService(
            IGenericRepository<FactorSalarioIntegrado, T> repositorio
            , IMapper mapper
            )
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<List<FactorSalarioIntegradoDTO>> ObtenerTodosXProyecto(int IdProyecto)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto);
                return _Mapper.Map<List<FactorSalarioIntegradoDTO>>(query);
            }
            catch
            {
                return new List<FactorSalarioIntegradoDTO>();
            }
        }

        public async Task<FactorSalarioIntegradoDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<FactorSalarioIntegradoDTO>(query);
            }
            catch
            {
                return new FactorSalarioIntegradoDTO();
            }
        }

        public async Task<FactorSalarioIntegradoDTO> CrearYObtener(FactorSalarioIntegradoDTO registro)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<FactorSalarioIntegrado>(registro));
                if (objetoCreado.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo crear");
                }
                return _Mapper.Map<FactorSalarioIntegradoDTO>(objetoCreado);
            }
            catch
            {
                return new FactorSalarioIntegradoDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(FactorSalarioIntegradoDTO registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<FactorSalarioIntegrado>(registro);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == registro.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El FSI no existe";
                    return respuesta;
                }
                objetoEncontrado.Fsi = registro.Fsi;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo editar";
                }
                respuesta.Descripcion = "FSI editado correctamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del FSI";
                return respuesta;
            }
        }

        public async Task Eliminar(int Id)
        {
            try
            {
                var FSR = await _Repositorio.Obtener(z => z.Id == Id);
                await _Repositorio.Eliminar(FSR);
                return;
            }
            catch
            {
                return;
            }
        }
    }
}
