using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class FamiliaInsumoService<T> : IFamiliaInsumoService<T> where T : DbContext
    {
        private readonly IGenericRepository<FamiliaInsumo, T> _Repositorio;
        private readonly IMapper _Mapper;

        public FamiliaInsumoService(
            IGenericRepository<FamiliaInsumo, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(FamiliaInsumoCreacionDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<FamiliaInsumo>(modelo));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Familia de insumo creada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la creación de la familia de insumo";
                return respuesta;
            }
        }

        public async Task<FamiliaInsumoDTO> CrearYObtener(FamiliaInsumoCreacionDTO modelo)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<FamiliaInsumo>(modelo));
                if (objetoCreado.Id == 0)
                    return new FamiliaInsumoDTO();
                return _Mapper.Map<FamiliaInsumoDTO>(objetoCreado);
            }
            catch
            {
                return new FamiliaInsumoDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(FamiliaInsumoDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<FamiliaInsumo>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La familia de insumo no existe";
                    return respuesta;
                }
                objetoEncontrado.Descripcion = modelo.Descripcion;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Familia de insumo editada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de la familia de insumo";
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
                    respuesta.Descripcion = "La familia de insumo no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Familia de insumo eliminada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación del tipo de insumo";
                return respuesta;
            }
        }

        public async Task<List<FamiliaInsumoDTO>> Lista()
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<FamiliaInsumoDTO>>(lista);
            }
            catch
            {
                return new List<FamiliaInsumoDTO>();
            }
        }

        public async Task<FamiliaInsumoDTO> ObtenXId(int Id)
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos(z => z.Id == Id);
                return _Mapper.Map<FamiliaInsumoDTO>(lista);
            }
            catch
            {
                return new FamiliaInsumoDTO();
            }
        }
    }
}

