using Microsoft.AspNetCore.Mvc;
using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERP_TECKIO.Controllers
{


    [Route("api/CodigoAgrupador/21")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CodigoAgrupadorAlumno21Controller : ControllerBase
    {
        private readonly ICodigoAgrupadorService<Alumno21Context> _CodigoAgrupadorService;

        public CodigoAgrupadorAlumno21Controller(
            ICodigoAgrupadorService<Alumno21Context> CodigoAgrupadorService)
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
