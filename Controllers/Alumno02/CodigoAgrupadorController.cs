using Microsoft.AspNetCore.Mvc;
using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERP_TECKIO.Controllers
{


    [Route("api/CodigoAgrupador/2")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CodigoAgrupadorAlumno02Controller : ControllerBase
    {
        private readonly ICodigoAgrupadorService<Alumno02Context> _CodigoAgrupadorService;

        public CodigoAgrupadorAlumno02Controller(
            ICodigoAgrupadorService<Alumno02Context> CodigoAgrupadorService)
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
