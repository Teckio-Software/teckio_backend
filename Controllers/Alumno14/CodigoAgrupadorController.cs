using Microsoft.AspNetCore.Mvc;
using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERP_TECKIO.Controllers
{


    [Route("api/CodigoAgrupador/14")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CodigoAgrupadorAlumno14Controller : ControllerBase
    {
        private readonly ICodigoAgrupadorService<Alumno14Context> _CodigoAgrupadorService;

        public CodigoAgrupadorAlumno14Controller(
            ICodigoAgrupadorService<Alumno14Context> CodigoAgrupadorService)
        {
            _CodigoAgrupadorService = CodigoAgrupadorService;
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<CodigoAgrupadorSatDTO>>> ObtenerCodigos()
        {
            return await _CodigoAgrupadorService.ObtenTodos();
        }

    }
}
