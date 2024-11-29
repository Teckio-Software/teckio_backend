using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    /// <summary>
    /// Controlador para las Salidas de almacén
    /// </summary>
    [Route("api/almacenSalida/1")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionSalidaAlmacen-Empresa1")]
    public class AlmacenSalidaAlumno01Controller : ControllerBase
    {
        private readonly IAlmacenSalidaService<Alumno01Context> _AlmacenSalidaServicio;
        private readonly IInsumoXAlmacenSalidaService<Alumno01Context> _AlmacenSalidaDetalleServicio;
        private readonly AlmacenSalidaProceso<Alumno01Context> _Proceso;
        /// <summary>
        /// Se usa para mostrar errores en consola
        /// </summary>
        private readonly ILogger<AlmacenSalidaAlumno01Controller> Logger;
        //private readonly UserManager<IdentityUser> zvUserManager;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly Alumno01Context Context;
        /// <summary>
        /// Constructor del controlador de Almacenes
        /// </summary>
        /// <param name="logger">Para mostrar errores en consola</param>
        /// <param name="context">Para mandar inofrmación de los registros</param>
        public AlmacenSalidaAlumno01Controller(
            //UserManager<IdentityUser> zUserManager,
            ILogger<AlmacenSalidaAlumno01Controller> logger,
            Alumno01Context context
            , IAlmacenSalidaService<Alumno01Context> AlmacenSalidaServicio
            , IInsumoXAlmacenSalidaService<Alumno01Context> AlmacenSalidaDetalleServicio
            , AlmacenSalidaProceso<Alumno01Context> Proceso
            )
        {
            //zvUserManager = zUserManager;
            Logger = logger;
            Context = context;
            _AlmacenSalidaServicio = AlmacenSalidaServicio;
            _AlmacenSalidaDetalleServicio = AlmacenSalidaDetalleServicio;
            _Proceso = Proceso;
        }

        /// <summary>
        /// Endpoint que llama al método para mostrar los registros de la tabla de almacenes
        /// que recibe como parametro los datos de paginación.
        /// </summary>
        /// <param name="paginacionDTO">Parametros de paginación</param>
        /// <param name="IdAlmacenEntrada">Id de la entrada a almacén</param>
        /// <returns>Lista con los registros de la tabla de almacenes sin paginar</returns>
        [HttpGet("todos")]
        public async Task<ActionResult<List<AlmacenSalidaDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = await _AlmacenSalidaServicio.ObtenTodos();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable.AsQueryable());
            var lista = queryable.AsQueryable().OrderByDescending(x => x.FechaRegistro).Paginar(paginacionDTO).ToList();
            if (lista.Count <= 0)
            {
                return NoContent();
            }
            return lista;
        }

        /// <summary>
        /// Endpoint que obtiene todos los registros de la tabla de almacenes sin paginar
        /// </summary>
        /// <returns>todos los registros de la tabla sin paginar</returns>
        [HttpGet("sinpaginar")]
        public async Task<ActionResult<List<AlmacenSalidaDTO>>> Get()
        {
            var queryable = await _AlmacenSalidaServicio.ObtenTodos();
            var lista = queryable.OrderByDescending(x => x.FechaRegistro).ToList();
            if (lista.Count <= 0)
            {
                return new List<AlmacenSalidaDTO>();
            }
            return lista;
        }

        [HttpPost("CrearAlmacenSalida")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<RespuestaDTO> CrearAlmacenSalida([FromBody] AlmacenSalidaCreacionDTO CreacionDTO)
        {
            var authen = HttpContext.User;
            return await _Proceso.CrearAlmacenSalida(CreacionDTO, authen.Claims.ToList());
        }

        [HttpPost("EditarAlmacenSalida")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<RespuestaDTO> EditarAlmacenSalida([FromBody] AlmacenSalidaDTO objeto)
        {
            return await _AlmacenSalidaServicio.Editar(objeto);
        }

        [HttpGet("ObtenXIdProyecto/{idProyecto}")]
        public async Task<ActionResult<List<AlmacenSalidaDTO>>> ObtenXIdProyecto(int idProyecto)
        {
            var lista = await _Proceso.ObtenXIdProyecto(idProyecto);
            return lista;
        }

        [HttpGet("ObtenXIdProyectoSalidasConPrestamos/{idProyecto}")]
        public async Task<ActionResult<List<AlmacenSalidaDTO>>> ObtenXIdProyectoSalidasConPrestamos(int idProyecto)
        {
            var lista = await _Proceso.ObtenXIdProyectoSalidasConPrestamos(idProyecto);
            return lista;
        }

        [HttpGet("obtenerInsumosDisponibles/{idAlmacen:int}")]
        public async Task<ActionResult<List<InsumosExistenciaDTO>>> obtenerInsumosDisponibles(int IdAlmacen)
        {
            var lista = await _Proceso.obtenerInsumosDisponibles(IdAlmacen);
            return lista;
        }            

        /// <summary>
        /// Endpoint que llama al método que permite obtener un registro a partir del Id de este
        /// </summary>
        /// <param name="Id">Id del registro a obtener</param>
        /// <returns>lista con el registro del Id dado</returns>
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<AlmacenSalidaDTO>> GetXId(int Id)
        {
            var lista = await _AlmacenSalidaServicio.ObtenXId(Id);
            return lista;
        }

        /// <summary>
        /// Endpoint para crear un nuevo almacen el cual recibe los parametros necesarios
        /// para la creación del nuevo registro
        /// </summary>
        /// <param name="ParametroDTO">Todos los campos de la tabla excepto el Id</param>
        /// <returns>NoContent</returns>
        //[HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearSalidaAlmacen-Empresa1")]
        //public async Task<ActionResult> Post([FromBody] AlmacenSalidaCreacionDTO ParametroDTO)
        //{
        //    if (ParametroDTO.IdAlmacen <= 0 || ParametroDTO.IdProyecto <= 0 || ParametroDTO.ListaAlmacenSalidaInsumoCreacion.Count <= 0)
        //    {
        //        return NoContent();
        //    }
        //    var resultado1 = await _AlmacenSalidaServicio.CrearYObtener(ParametroDTO);
        //    if (resultado1.Id > 0)
        //    {
        //        foreach (var Insumo in ParametroDTO.ListaAlmacenSalidaInsumoCreacion)
        //        {
        //            var resultado2 = await _AlmacenSalidaDetalleServicio.CrearYObtener(Insumo);
        //        }
        //    }
        //    return NoContent();
        //}

        /// <summary>
        /// Éndpoint para editar un registro el cual recibe como parametros almacenDTO
        /// el cual contiene todos los campos de la tabla y hace una edición a partir de 
        /// el Id del registro.
        /// </summary>
        /// <param name="Edita">Todos los campos de la tabla de almacen</param>
        /// <returns>NoContent</returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarSalidaAlmacen-Empresa1")]
        public async Task<ActionResult> Put([FromBody] AlmacenSalidaDTO Edita)
        {
            await _AlmacenSalidaServicio.Editar(Edita);
            return NoContent();
        }

        [HttpPut("autorizar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarSalidaAlmacen-Empresa1")]
        public async Task<ActionResult> PutAutorizar([FromBody] AlmacenSalidaDTO ParametroDTO)
        {
            var authen = HttpContext.User;
            ParametroDTO.Autorizo = authen.Claims.FirstOrDefault()!.Value;
            var lista = await _AlmacenSalidaServicio.Autorizar(ParametroDTO.Id);
            return NoContent();
        }

        [HttpPut("cancelar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarSalidaAlmacen-Empresa1")]
        public async Task<ActionResult> PutCancelar([FromBody] AlmacenSalidaDTO ParametroDTO)
        {
            var authen = HttpContext.User;
            var lista = await _AlmacenSalidaServicio.Cancelar(ParametroDTO.Id);

            return NoContent();
        }
    }
}
