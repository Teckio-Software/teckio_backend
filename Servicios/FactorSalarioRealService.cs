using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class FactorSalarioRealService<T> : IFactorSalarioRealService<T> where T : DbContext
    {
        private readonly IGenericRepository<FactorSalarioReal, T> _Repositorio;
        private readonly IMapper _Mapper;
        public FactorSalarioRealService(
            IGenericRepository<FactorSalarioReal, T> repositorio
            , IMapper mapper
            )
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<List<FactorSalarioRealDTO>> ObtenerTodosXProyecto(int IdProyecto)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto);
                return _Mapper.Map<List<FactorSalarioRealDTO>>(query);
            }
            catch
            {
                return new List<FactorSalarioRealDTO>();
            }
        }

        public async Task<FactorSalarioRealDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<FactorSalarioRealDTO>(query);
            }
            catch
            {
                return new FactorSalarioRealDTO();
            }
        }

        public async Task<FactorSalarioRealDTO> CrearYObtener(FactorSalarioRealDTO registro)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<FactorSalarioReal>(registro));
                if(objetoCreado.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo crear");
                }
                return _Mapper.Map<FactorSalarioRealDTO>(objetoCreado);
            }
            catch
            {
                return new FactorSalarioRealDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(FactorSalarioRealDTO registro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<FactorSalarioReal>(registro);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == registro.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El FSR no existe";
                    return respuesta;
                }
                objetoEncontrado.PorcentajeFsr = registro.PorcentajeFsr;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo editar";
                }
                respuesta.Descripcion = "FSR editado correctamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del FSR";
                return respuesta;
            }
        }

        public async Task Eliminar(int Id)
        {
            try
            {
                var FSI = await _Repositorio.Obtener(z => z.Id == Id);
                await _Repositorio.Eliminar(FSI);
                return;
            }
            catch
            {
                return;
            }
        }
    }
}
