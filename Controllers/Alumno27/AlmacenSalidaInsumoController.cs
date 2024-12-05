

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using System.Data;



using System.Diagnostics;


namespace ERP_TECKIO
{
    [Route("api/almacenSalidainsumo/27")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionSalidaAlmacen-Empresa1")]
    public class AlmacenSalidaInsumoAlumno27Controller : ControllerBase
    {
        private readonly IInsumoXAlmacenSalidaService<Alumno27Context> _AlmacenSalidaDetalleServicio;
        private readonly AlmacenSalidaProceso<Alumno27Context> _Proceso;
        /// <summary>
        /// Se usa para mostrar errores en consola
        /// </summary>
        private readonly ILogger<AlmacenSalidaInsumoAlumno27Controller> Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly Alumno27Context Context;
        /// <summary>
        /// Constructor del controlador de Almacenes
        /// </summary>
        /// <param name="logger">Para mostrar errores en consola</param>
        /// <param name="context">Para mandar inofrmación de los registros</param>
        public AlmacenSalidaInsumoAlumno27Controller(
            ILogger<AlmacenSalidaInsumoAlumno27Controller> logger,
            Alumno27Context context
            , IInsumoXAlmacenSalidaService<Alumno27Context> AlmacenSalidaDetalleServicio
            , AlmacenSalidaProceso<Alumno27Context> Proceso
            )
        {
            Logger = logger;
            Context = context;
            _AlmacenSalidaDetalleServicio = AlmacenSalidaDetalleServicio;
            _Proceso = Proceso;
        }

        [HttpPost("CrearInsumoSalidaAlmacen")]
        public async Task<ActionResult<RespuestaDTO>> CrearInsumoSalidaAlmacen(AlmacenSalidaInsumoCreacionDTO parametro)
        {
            return await _Proceso.CrearInsumoSalidaAlmacen(parametro);
        }

        [HttpGet("ObtenXIdProyecto/{idProyecto}")]
        public async Task<ActionResult<List<AlmacenSalidaInsumoDTO>>> ObtenXIdProyecto(int idProyecto)
        {
            var lista = await _Proceso.InsumosAlmacenSalidaObtenXIdProyceto(idProyecto);
            return lista;
        }

        [HttpGet("ObtenXIdSalidaAlmacen/{idSalidaAlmacen}")]
        public async Task<ActionResult<List<AlmacenSalidaInsumoDTO>>> ObtenXIdSalidaAlmacen(int idSalidaAlmacen)
        {
            var lista = await _Proceso.IsumosAlmacenSalidaObtenXIdSalidaAlmacen(idSalidaAlmacen);
            return lista;
        }

        [HttpGet("ObtenXIdAlmacenYPrestamo/{idAlmacen}")]
        public async Task<ActionResult<List<AlmacenSalidaInsumoDTO>>> ObtenXIdAlmacenYPrestamo(int idAlmacen)
        {
            var lista = await _Proceso.ObtenXIdAlmacenYPrestamo(idAlmacen);
            return lista;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paginacionDTO"></param>
        /// <returns></returns>
        [HttpGet("todos/{IdAlmacenSalida:int}")]
        public async Task<ActionResult<List<AlmacenSalidaInsumoDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO, int IdAlmacenSalida)
        {
            var queryable = await _AlmacenSalidaDetalleServicio.ObtenXIdAlmacenSalida(IdAlmacenSalida);
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable.AsQueryable());
            var lista = queryable.AsQueryable().Paginar(paginacionDTO).ToList();
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
        [HttpGet("sinpaginar/{IdAlmacenSalida:int}")]
        public async Task<ActionResult<List<AlmacenSalidaInsumoDTO>>> Get(int IdAlmacenSalida)
        {
            var lista = await _AlmacenSalidaDetalleServicio.ObtenXIdAlmacenSalida(IdAlmacenSalida);
            return lista;
        }

        /// <summary>
        /// Endpoint que llama al método que permite obtener un registro a partir del Id de este
        /// </summary>
        /// <param name="Id">Id del registro a obtener</param>
        /// <returns>lista con el registro del Id dado</returns>
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<AlmacenSalidaInsumoDTO>> GetXId(int Id)
        {
            var lista = await _AlmacenSalidaDetalleServicio.ObtenXId(Id);
            return lista;

        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarSalidaAlmacen-Empresa1")]
        public async Task<ActionResult> Put([FromBody] AlmacenSalidaInsumoDTO Edita)
        {
            var resultado = await _AlmacenSalidaDetalleServicio.Editar(Edita);
            return NoContent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParametroDTO"></param>
        /// <returns></returns>
        [HttpPut("autorizar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarSalidaAlmacen-Empresa1")]
        public async Task<ActionResult> PutAutorizar([FromBody] AlmacenSalidaInsumoDTO ParametroDTO)
        {
            var authen = HttpContext.User;
            await _AlmacenSalidaDetalleServicio.Autorizar(ParametroDTO.Id);
            return NoContent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParametroDTO"></param>
        /// <returns></returns>
        [HttpPut("cancelar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarSalidaAlmacen-Empresa1")]
        public async Task<ActionResult> PutCancelar([FromBody] AlmacenSalidaInsumoDTO ParametroDTO)
        {
            var authen = HttpContext.User;
            await _AlmacenSalidaDetalleServicio.Cancelar(ParametroDTO.Id);
            return NoContent();
        }
    }
}
