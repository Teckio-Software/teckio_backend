using AutoMapper;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class GeneradoresXEstimacionService<T> : IGeneradoresXEstimacionService<T> where T : DbContext
    {
        private readonly IGenericRepository<GeneradoresXEstimacion, T> _Repositorio;
        private readonly IMapper _Mapper;

        public GeneradoresXEstimacionService(IGenericRepository<GeneradoresXEstimacion, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(GeneradoresXEstimacionDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<GeneradoresXEstimacion>(modelo));
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

        public Task<GeneradoresDTO> CrearYObtener(GeneradoresXEstimacionDTO modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<RespuestaDTO> Editar(GeneradoresXEstimacionDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<GeneradoresXEstimacion>(parametro);
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
            var respuesta = new RespuestaDTO();
            var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == Id);
            if (objetoEncontrado.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se encontro el registro";
                return respuesta;
            }

            var eliminar = await _Repositorio.Eliminar(objetoEncontrado);
            if (!eliminar) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se elimino el registro";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se elimino el registro";
            return respuesta;
        }

        public async Task<GeneradoresXEstimacionDTO> ObtenXId(int Id)
        {
            var objeto = await _Repositorio.Obtener(z => z.Id == Id);
            return _Mapper.Map<GeneradoresXEstimacionDTO>(objeto);
        }

        public async Task<List<GeneradoresXEstimacionDTO>> ObtenXIdEstimacion(int Id)
        {
            var lista = await _Repositorio.ObtenerTodos(z => z.IdEstimacion == Id);
            return _Mapper.Map<List<GeneradoresXEstimacionDTO>>(lista);
        }
    }
}
