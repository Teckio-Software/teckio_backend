using Microsoft.AspNetCore.Mvc;
using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERP_TECKIO.Controllers
{


    [Route("api/codigoagrupador/14")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CodigoAgrupador14Controller : ControllerBase
    {
        private readonly ICodigoAgrupadorService<Alumno14Context> _CodigoAgrupadorService;

        public CodigoAgrupador14Controller(
            ICodigoAgrupadorService<Alumno14Context> codigoAgrupadorService)
        {
            _CodigoAgrupadorService = codigoAgrupadorService;
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<CodigoAgrupadorSatDTO>>> ObtenerCodigos()
        {
            return await _CodigoAgrupadorService.ObtenTodos();
        }

    }
}
