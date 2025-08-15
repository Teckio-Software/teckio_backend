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
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == parametro.id);
                if (objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                objetoEncontrado.Unidad = parametro.Unidad;
                objetoEncontrado.Codigo = parametro.Codigo;
                objetoEncontrado.Descripcion = parametro.Descripcion;
                objetoEncontrado.IdTipoInsumo = parametro.idTipoInsumo;
                objetoEncontrado.IdFamiliaInsumo = parametro.idFamiliaInsumo;
                objetoEncontrado.CostoUnitario = parametro.CostoUnitario;
                objetoEncontrado.CostoBase = parametro.CostoBase;
                objetoEncontrado.EsFsrGlobal = parametro.EsFsrGlobal;
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
                    respuesta.Estatus = false;
                    return respuesta;
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

        public async Task<List<InsumoDTO>> ObtenerTodos()
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos();
                if (lista.Count > 0)
                {
                    return _Mapper.Map<List<InsumoDTO>>(lista);
                }
                else
                {
                    return new List<InsumoDTO>();
                }
            }
            catch
            {
                return new List<InsumoDTO>();

            }
        }

        public async Task<bool> AutorizarMultiple(List<InsumoParaExplosionDTO> registros)
        {
            var insumos = new List<Insumo>();
            foreach (var insumo in registros) {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == insumo.id);
                objetoEncontrado.EsAutorizado = true;
                insumos.Add(objetoEncontrado);
            }
            return await _Repositorio.EditarMultiple(insumos);
        }

        public async Task<bool> EditarMultiple(List<InsumoDTO> registros)
        {
            var insumos = new List<Insumo>();
            foreach (var insumo in registros)
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == insumo.id);
                objetoEncontrado.EsAutorizado = true;
                insumos.Add(objetoEncontrado);
            }
            return await _Repositorio.EditarMultiple(insumos);
        }

        public async Task<bool> AutorizarMultipleXPU(List<PrecioUnitarioDetalleDTO> registros)
        {
            var insumos = new List<Insumo>();
            foreach (var insumo in registros)
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == insumo.IdInsumo);
                objetoEncontrado.EsAutorizado = true;
                insumos.Add(objetoEncontrado);
            }
            return await _Repositorio.EditarMultiple(insumos);
        }
    }
}
