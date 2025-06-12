using Microsoft.AspNetCore.Mvc;
using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERP_TECKIO.Controllers
{


    [Route("api/codigoagrupador/24")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CodigoAgrupador24Controller : ControllerBase
    {
        private readonly ICodigoAgrupadorService<Alumno24Context> _CodigoAgrupadorService;

        public CodigoAgrupador24Controller(
            ICodigoAgrupadorService<Alumno24Context> codigoAgrupadorService)
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
