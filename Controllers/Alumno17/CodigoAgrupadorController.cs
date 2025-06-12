using Microsoft.AspNetCore.Mvc;
using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERP_TECKIO.Controllers
{


    [Route("api/codigoagrupador/17")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CodigoAgrupador17Controller : ControllerBase
    {
        private readonly ICodigoAgrupadorService<Alumno17Context> _CodigoAgrupadorService;

        public CodigoAgrupador17Controller(
            ICodigoAgrupadorService<Alumno17Context> codigoAgrupadorService)
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
