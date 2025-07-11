using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    /// <summary>
    /// Controlador de los tipos de tipopoliza que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/tipopoliza/1")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionTipoPoliza-Empresa1")]
    public class TipoPolizaIyATolucaController : ControllerBase
    {
        private readonly ITipoPolizaService<IyATolucaContext> _Service;
        /// <summary>
        /// Se usa para mostrar errores en consola
        /// </summary>
        private readonly ILogger<TipoPolizaIyATolucaController> Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly IyATolucaContext Context;
        /// <summary>
        /// Constructor del controlador de Almacenes
        /// </summary>
        /// <param name="logger">Para mostrar errores en consola</param>
        /// <param name="context">Para mandar inofrmación de los registros</param>
        public TipoPolizaIyATolucaController(
            ILogger<TipoPolizaIyATolucaController> logger,
            IyATolucaContext context
            , ITipoPolizaService<IyATolucaContext> service)
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearTipoPoliza-Empresa1")]
        public async Task<ActionResult> Post([FromBody] TipoPolizaCreacionDTO creacionDTO)
        {
            try
            {
                var resultado = await _Service.Crear(creacionDTO);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<IyATolucaContext>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarTipoPoliza-Empresa1")]
        public async Task<ActionResult> Put([FromBody] TipoPolizaDTO parametroDTO)
        {
            try
            {
                var resultado = await _Service.Editar(parametroDTO);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<IyATolucaContext>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }
    }
}
