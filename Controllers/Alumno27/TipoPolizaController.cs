
using ERP_TECKIO.DTO;

using Microsoft.AspNetCore.Mvc;


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace ERP_TECKIO
{
    /// <summary>
    /// Controlador de los tipos de tipopoliza que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/tipopoliza/27")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionTipoPoliza-Empresa27")]
    public class TipoPolizaAlumno27Controller : ControllerBase
    {
        private readonly ITipoPolizaService<Alumno27Context> _Service;
        /// <summary>
        /// Se usa para mostrar errores en consola
        /// </summary>
        private readonly ILogger<TipoPolizaAlumno27Controller> Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly Alumno27Context Context;
        /// <summary>
        /// Constructor del controlador de Almacenes
        /// </summary>
        /// <param name="logger">Para mostrar errores en consola</param>
        /// <param name="context">Para mandar inofrmación de los registros</param>
        public TipoPolizaAlumno27Controller(
            ILogger<TipoPolizaAlumno27Controller> logger,
            Alumno27Context context
            , ITipoPolizaService<Alumno27Context> service)
        {
            _Service = service;
            Logger = logger;
            Context = context;
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<TipoPolizaDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var query = _Service.ObtenTodos().Result.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(query);
            var lista = query.OrderBy(x => x.Id).Paginar(paginacionDTO).ToList();
            if (lista.Count <= 0) { return new List<TipoPolizaDTO>(); }
            return lista;
        }

        [HttpGet("sinpaginar")]
        public async Task<ActionResult<List<TipoPolizaDTO>>> obtenerSinPaginar()
        {
            var query = _Service.ObtenTodos().Result.AsQueryable();
            var lista = query.OrderBy(x => x.Id).ToList();
            if (lista.Count <= 0) { return new List<TipoPolizaDTO>(); }
            return lista;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearTipoPoliza-Empresa27")]
        public async Task<ActionResult> Post([FromBody] TipoPolizaCreacionDTO creacionDTO)
        {
            try
            {
                var resultado = await _Service.Crear(creacionDTO);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno27Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarTipoPoliza-Empresa27")]
        public async Task<ActionResult> Put([FromBody] TipoPolizaDTO parametroDTO)
        {
            try
            {
                var resultado = await _Service.Editar(parametroDTO);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno27Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }
    }
}
