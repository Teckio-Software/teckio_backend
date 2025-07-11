using Microsoft.AspNetCore.Mvc;
using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERP_TECKIO.Controllers
{


    [Route("api/codigoagrupador/1")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CodigoAgrupadorIyATolucaController : ControllerBase
    {
        private readonly ICodigoAgrupadorService<IyATolucaContext> _CodigoAgrupadorService;

        public CodigoAgrupadorIyATolucaController(
            ICodigoAgrupadorService<IyATolucaContext> codigoAgrupadorService)
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
