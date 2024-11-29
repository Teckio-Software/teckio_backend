
using ERP_TECKIO.DTO;

using Microsoft.AspNetCore.Mvc;


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace ERP_TECKIO
{
    /// <summary>
    /// Controlador de los tipos de tipopoliza que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/tipopoliza/29")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionTipoPoliza-Empresa29")]
    public class TipoPolizaAlumno29Controller : ControllerBase
    {
        private readonly ITipoPolizaService<Alumno29Context> _Service;
        /// <summary>
        /// Se usa para mostrar errores en consola
        /// </summary>
        private readonly ILogger<TipoPolizaAlumno29Controller> Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly Alumno29Context Context;
        /// <summary>
        /// Constructor del controlador de Almacenes
        /// </summary>
        /// <param name="logger">Para mostrar errores en consola</param>
        /// <param name="context">Para mandar inofrmación de los registros</param>
        public TipoPolizaAlumno29Controller(
            ILogger<TipoPolizaAlumno29Controller> logger,
            Alumno29Context context
            , ITipoPolizaService<Alumno29Context> service)
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearTipoPoliza-Empresa29")]
        public async Task<ActionResult> Post([FromBody] TipoPolizaCreacionDTO creacionDTO)
        {
            try
            {
                var resultado = await _Service.Crear(creacionDTO);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno29Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarTipoPoliza-Empresa29")]
        public async Task<ActionResult> Put([FromBody] TipoPolizaDTO parametroDTO)
        {
            try
            {
                var resultado = await _Service.Editar(parametroDTO);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno29Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }
    }
}
