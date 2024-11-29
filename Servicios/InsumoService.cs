using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class InsumoService<T> : IInsumoService<T> where T : DbContext
    {
        private readonly IGenericRepository<Insumo, T> _Repositorio;
        private readonly IMapper _Mapper;

        public InsumoService(IGenericRepository<Insumo, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(InsumoCreacionDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Insumo>(modelo));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Insumo creado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la creación del insumo";
                return respuesta;
            }
        }

        public async Task<InsumoDTO> CrearYObtener(InsumoCreacionDTO modelo)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Insumo>(modelo));
                if (objetoCreado.Id == 0)
                    throw new TaskCanceledException("No se pudó crear");
                return _Mapper.Map<InsumoDTO>(objetoCreado);
            }
            catch
            {
                return new InsumoDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(InsumoDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<Insumo>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                objetoEncontrado.Unidad = modelo.Unidad;
                objetoEncontrado.Codigo = modelo.Codigo;
                objetoEncontrado.Descripcion = modelo.Descripcion;
                objetoEncontrado.IdTipoInsumo = modelo.IdTipoInsumo;
                objetoEncontrado.IdFamiliaInsumo = modelo.IdFamiliaInsumo;
                objetoEncontrado.CostoUnitario = modelo.CostoUnitario;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Insumo editado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del insumo";
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
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Insumo eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación del insumo";
                return respuesta;
            }
        }

        public async Task<List<InsumoDTO>> ObtenXIdProyecto(int IdProyecto)
        {
                var query = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto);
                return _Mapper.Map<List<InsumoDTO>>(query);
        }

        public async Task<InsumoDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<InsumoDTO>(query);
            }
            catch
            {
                return new InsumoDTO();
            }
        }

        public async Task<List<InsumoDTO>> ObtenerInsumoXTipoYProyecto(int IdProyecto, int IdTipoInsumo)
        {
            var query = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto && z.IdTipoInsumo == IdTipoInsumo);
            return _Mapper.Map<List<InsumoDTO>>(query);
        }

        public async Task<List<InsumoDTO>> ObtenerInsumoXProyecto(int IdProyecto)
        {
            var query = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto);
            return _Mapper.Map<List<InsumoDTO>>(query);
        }
    }
}
