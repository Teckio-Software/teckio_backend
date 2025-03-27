using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class GeneradoresService<T> : IGeneradoresService<T> where T : DbContext
    {
        private readonly IGenericRepository<Generadores, T> _Repositorio;
        private readonly IMapper _Mapper;

        public GeneradoresService(IGenericRepository<Generadores, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<List<GeneradoresDTO>> ObtenerTodosXIdPrecioUnitario(int IdPrecioUnitario)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdPrecioUnitario == IdPrecioUnitario);
                return _Mapper.Map<List<GeneradoresDTO>>(query);
            }
            catch
            {
                return new List<GeneradoresDTO>();
            }
        }

        public async Task<GeneradoresDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<GeneradoresDTO>(query);
            }
            catch
            {
                return new GeneradoresDTO();
            }
        }

        public async Task<RespuestaDTO> Crear(GeneradoresDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Generadores>(modelo));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Generador creado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la creación del generador";
                return respuesta;
            }
        }

        public async Task<GeneradoresDTO> CrearYObtener(GeneradoresDTO modelo)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Generadores>(modelo));
                if (objetoCreado.Id == 0)
                    throw new TaskCanceledException("No se pudó crear");
                return _Mapper.Map<GeneradoresDTO>(objetoCreado);
            }
            catch (Exception ex)
            {
                return new GeneradoresDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(GeneradoresDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<Generadores>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == parametro.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                objetoEncontrado.Codigo = parametro.Codigo;
                objetoEncontrado.EjeX = parametro.EjeX;
                objetoEncontrado.EjeY = parametro.EjeY;
                objetoEncontrado.EjeZ = parametro.EjeZ;
                objetoEncontrado.Cantidad = parametro.Cantidad;
                objetoEncontrado.X = parametro.X;
                objetoEncontrado.Y = parametro.Y;
                objetoEncontrado.Z = parametro.Z;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Generador editado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del generador";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == Id);

                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El generador no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Generador eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación del generador";
                return respuesta;
            }
        }
         public async Task<bool> EliminarTodos(int idPrecioU) {
            var objetos = await _Repositorio.ObtenerTodos(Z => Z.IdPrecioUnitario == idPrecioU);
            var respuesta = await _Repositorio.EliminarMultiple(objetos);
            return respuesta;
        }
    }
}
