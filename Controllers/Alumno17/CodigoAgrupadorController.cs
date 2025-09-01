using Microsoft.AspNetCore.Mvc;
using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERP_TECKIO.Controllers
{


    [Route("api/CodigoAgrupador/17")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CodigoAgrupadorAlumno17Controller : ControllerBase
    {
        private readonly ICodigoAgrupadorService<Alumno17Context> _CodigoAgrupadorService;

        public CodigoAgrupadorAlumno17Controller(
            ICodigoAgrupadorService<Alumno17Context> CodigoAgrupadorService)
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
