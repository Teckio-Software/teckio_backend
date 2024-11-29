using AutoMapper;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class ProyectoService<T> : IProyectoService<T> where T : DbContext
    {
        private readonly IGenericRepository<Proyecto, T> _Repositorio;
        //private readonly PROCOMIContext _Context;
        private readonly IMapper _Mapper;

        public ProyectoService(IGenericRepository<Proyecto, T> repositorio, IMapper mapper)
        {
            _Repositorio = repositorio;
            //_Context = context;
            _Mapper = mapper;
        }

        public async Task<RespuestaDTO> Crear(ProyectoDTO modelo)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Proyecto>(modelo));
                if (objetoCreado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudó crear";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Proyecto creado correctamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la creación del proyecto";
                return respuesta;
            }
        }

        public async Task<ProyectoDTO> CrearYObtener(ProyectoDTO modelo)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Proyecto>(modelo));
                if (objetoCreado.Id == 0)
                    throw new TaskCanceledException("No se pudó crear");
                return _Mapper.Map<ProyectoDTO>(objetoCreado);
            }
            catch
            {
                return new ProyectoDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(ProyectoDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<Proyecto>(parametro);

                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);

                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El proyecto no existe";
                    return respuesta;
                }

                objetoEncontrado.Nombre = modelo.Nombre;
                objetoEncontrado.NoSerie = modelo.NoSerie;
                objetoEncontrado.Moneda = modelo.Moneda;
                objetoEncontrado.PresupuestoSinIva = modelo.PresupuestoSinIva;
                objetoEncontrado.TipoCambio = modelo.TipoCambio;
                objetoEncontrado.PresupuestoSinIvaMonedaNacional = modelo.PresupuestoSinIvaMonedaNacional;
                objetoEncontrado.PorcentajeIva = modelo.PorcentajeIva;
                objetoEncontrado.PresupuestoConIvaMonedaNacional = modelo.PresupuestoConIvaMonedaNacional;
                objetoEncontrado.Anticipo = modelo.Anticipo;
                objetoEncontrado.CodigoPostal = modelo.CodigoPostal;
                objetoEncontrado.Domicilio = modelo.Domicilio;
                objetoEncontrado.FechaInicio = modelo.FechaInicio;
                objetoEncontrado.FechaFinal = modelo.FechaFinal;
                objetoEncontrado.TipoProgramaActividad = modelo.TipoProgramaActividad;
                objetoEncontrado.InicioSemana = modelo.InicioSemana;
                objetoEncontrado.EsSabado = modelo.EsSabado;
                objetoEncontrado.EsDomingo = modelo.EsDomingo;
                objetoEncontrado.IdPadre = modelo.IdPadre;

                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);

                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo editar";
                }
                respuesta.Descripcion = "Proyecto editado correctamente";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del proyecto";
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
                    respuesta.Descripcion = "El proyecto no existe";
                    return respuesta;
                }
                //Aqui van las otras valIdaciones
                bool eliminado = await _Repositorio.Eliminar(objetoEncontrado);
                if (!eliminado)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Proyecto eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación del proyecto";
                return respuesta;
            }
        }

        public async Task<List<ProyectoDTO>> Lista()
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<ProyectoDTO>>(query);
            }
            catch
            {
                return new List<ProyectoDTO>();
            }
        }

        public async Task<ProyectoDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<ProyectoDTO>(query);
            }
            catch
            {
                return new ProyectoDTO();
            }
        }

        /// <summary>
        /// Metodo que estructura con estructura de arbol los registros
        /// a partir de su padre 
        /// </summary>
        /// <param name="registros"></param>
        /// <returns></returns>
        public async Task<List<ProyectoDTO>> Estructurar(List<ProyectoDTO> registros)
        {
            var padres = registros.Where(z => z.IdPadre == 0).ToList();
            for(int i = 0; i < padres.Count; i++)
            {
                padres[i].Hijos = await BuscaHijos(registros, padres[i]);
            }
            return padres;
        }

        private async Task<List<ProyectoDTO>> BuscaHijos(List<ProyectoDTO> registros, ProyectoDTO padre)
        {
            var padres = registros.Where(z => z.IdPadre == padre.Id).ToList();
            for(int i = 0; i < padres.Count; i++)
            {
                padres[i].Hijos = await BuscaHijos(registros, padres[i]);
            }
            return padres;
        }

        public async Task<ProyectoDTO> ObtenXNombreProyecto(string nombreProyecto)
        {
            var query = await _Repositorio.Obtener(z => z.Nombre == nombreProyecto);
            return _Mapper.Map<ProyectoDTO>(query);
        }
    }
}
