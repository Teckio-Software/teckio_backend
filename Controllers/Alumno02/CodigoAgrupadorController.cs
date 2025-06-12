using Microsoft.AspNetCore.Mvc;
using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERP_TECKIO.Controllers
{


    [Route("api/codigoagrupador/2")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CodigoAgrupador02Controller : ControllerBase
    {
        private readonly ICodigoAgrupadorService<Alumno02Context> _CodigoAgrupadorService;

        public CodigoAgrupador02Controller(
            ICodigoAgrupadorService<Alumno02Context> codigoAgrupadorService)
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
