using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class PrecioUnitarioDetalleService<T> : IPrecioUnitarioDetalleService<T> where T : DbContext
    {
        private readonly IGenericRepository<PrecioUnitarioDetalle, T> _Repositorio;
        private readonly IMapper _Mapper;

        public PrecioUnitarioDetalleService(IGenericRepository<PrecioUnitarioDetalle, T> repositorio, IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<List<PrecioUnitarioDetalleDTO>> ObtenerTodosXIdPrecioUnitario(int IdPrecioUnitario)
        {
            var query = await _Repositorio.ObtenerTodos(z => z.IdPrecioUnitario == IdPrecioUnitario);
            return _Mapper.Map<List<PrecioUnitarioDetalleDTO>>(query);
        }

        public async Task<List<PrecioUnitarioDetalleDTO>> ObtenerTodos()
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<PrecioUnitarioDetalleDTO>>(query);
            }
            catch
            {
                return new List<PrecioUnitarioDetalleDTO>();
            }
        }

        public async Task<List<PrecioUnitarioDetalleDTO>> ObtenerTodosXIdPrecioUnitarioFiltrados(int IdPrecioUnitario, int IdPerteneciente)
        {
            var query = await _Repositorio.ObtenerTodos(z => z.IdPrecioUnitario == IdPrecioUnitario && z.IdPrecioUnitarioDetallePerteneciente == IdPerteneciente);
            return _Mapper.Map<List<PrecioUnitarioDetalleDTO>>(query);
        }

        public async Task<List<PrecioUnitarioDetalleDTO>> ObtenerTodosXIdPrecioUnitarioDetalle(PrecioUnitarioDetalleDTO PrecioUnitarioDetalle)
        {
            var RegistrosXPrecioUnitario = await ObtenerTodosXIdPrecioUnitario(PrecioUnitarioDetalle.IdPrecioUnitario);
            var RegistrosPertenecientes = RegistrosXPrecioUnitario.Where(z => z.IdPrecioUnitarioDetallePerteneciente == PrecioUnitarioDetalle.Id).ToList();
            if(RegistrosPertenecientes.Count <= 0)
            {
                return new List<PrecioUnitarioDetalleDTO>();
            }
            return RegistrosPertenecientes;
        }

        public async Task<PrecioUnitarioDetalleDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<PrecioUnitarioDetalleDTO>(query);
            }
            catch
            {
                return new PrecioUnitarioDetalleDTO();
            }
        }

        public async Task<PrecioUnitarioDetalleDTO> CrearYObtener(PrecioUnitarioDetalleDTO PrecioUnitarioDetalle)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<PrecioUnitarioDetalle>(PrecioUnitarioDetalle));
                if(objetoCreado.Id == 0)
                {
                    throw new TaskCanceledException("No se puedo crear");
                }
                return _Mapper.Map<PrecioUnitarioDetalleDTO>(objetoCreado);
            }
            catch
            {
                return new PrecioUnitarioDetalleDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(PrecioUnitarioDetalleDTO PrecioUnitarioDetalle)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<PrecioUnitarioDetalleDTO>(PrecioUnitarioDetalle);
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == PrecioUnitarioDetalle.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El detalle de precio unitario no existe";
                    return respuesta;
                }
                objetoEncontrado.IdPrecioUnitario = PrecioUnitarioDetalle.IdPrecioUnitario;
                objetoEncontrado.IdInsumo = PrecioUnitarioDetalle.IdInsumo;
                objetoEncontrado.EsCompuesto = PrecioUnitarioDetalle.EsCompuesto;
                objetoEncontrado.Cantidad = PrecioUnitarioDetalle.Cantidad;
                objetoEncontrado.CantidadExcedente = PrecioUnitarioDetalle.CantidadExcedente;
                objetoEncontrado.IdPrecioUnitarioDetallePerteneciente = PrecioUnitarioDetalle.IdPrecioUnitarioDetallePerteneciente;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo editar";
                }
                respuesta.Descripcion = "Detalle de precio unitario editado correctamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del detalle del precio Unitario";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El detalle de precio unitario no existe";
                    return respuesta;
                }
                bool eliminado = await _Repositorio.Eliminar(objetoEncontrado);
                if (!eliminado)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Detalle de precio unitario eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación del detalle de precio unitario";
                return respuesta;
            }
        }

        public async Task Recalcular(PrecioUnitarioDetalleDTO registro)
        {
            if(registro.IdPrecioUnitarioDetallePerteneciente > 0)
            {
                var registros = ObtenerTodosXIdPrecioUnitario(registro.IdPrecioUnitario).Result;
                var padre = registros.Where(z => z.Id == registro.IdPrecioUnitarioDetallePerteneciente).FirstOrDefault();
                var hijos = registros.Where(z => z.IdPrecioUnitarioDetallePerteneciente == padre.Id).ToList();
                decimal total = 0;
                for(int i = 0; i < hijos.Count; i++)
                {
                    total = total + hijos[i].CostoUnitario * hijos[i].Cantidad;
                }
                padre.CostoUnitario = total;
                await Editar(padre);
                await RecalcularPadres(padre, registros);
            }
        }

        private async Task RecalcularPadres(PrecioUnitarioDetalleDTO registro, List<PrecioUnitarioDetalleDTO> registros)
        {
            if(registro.IdPrecioUnitarioDetallePerteneciente > 0)
            {
                var padre = registros.Where(z => z.Id == registro.IdPrecioUnitarioDetallePerteneciente).FirstOrDefault();
                var hijos = registros.Where(z => z.IdPrecioUnitarioDetallePerteneciente == padre.Id).ToList();
                decimal total = 0;
                for(int i = 0; i < hijos.Count; i++)
                {
                    total = total + hijos[i].CostoUnitario * hijos[i].Cantidad;
                }
                padre.CostoUnitario = total;
                await Editar(padre);
                await RecalcularPadres(padre, registros);
            }
        }
    }
}
