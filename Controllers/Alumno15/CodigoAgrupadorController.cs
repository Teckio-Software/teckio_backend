using Microsoft.AspNetCore.Mvc;
using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERP_TECKIO.Controllers
{


    [Route("api/CodigoAgrupador/15")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CodigoAgrupadorAlumno15Controller : ControllerBase
    {
        private readonly ICodigoAgrupadorService<Alumno15Context> _CodigoAgrupadorService;

        public CodigoAgrupadorAlumno15Controller(
            ICodigoAgrupadorService<Alumno15Context> CodigoAgrupadorService)
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
