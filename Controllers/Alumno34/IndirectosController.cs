using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;





namespace ERP_TECKIO.API.Controllers.Alumno34
{
    [Route("api/indirectos/34")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IndirectosAlumno34Controller : ControllerBase
    {
        private readonly IConjuntoIndirectosService<Alumno34Context> _conjuntoIndirectosService;
        private readonly IIndirectosService<Alumno34Context> _indirectosService;
        private readonly IndirectosProceso<Alumno34Context> _indirectosProceso;

        public IndirectosAlumno34Controller(
                        IConjuntoIndirectosService<Alumno34Context> conjuntoIndirectosService,
                        IIndirectosService<Alumno34Context> indirectosService,
                        IndirectosProceso<Alumno34Context> indirectosProceso
            )
        {
            _conjuntoIndirectosService = conjuntoIndirectosService;
            _indirectosService = indirectosService;
            _indirectosProceso = indirectosProceso;
        }

        [HttpGet("ObtenerConjuntoIndirecto/{IdProyecto:int}")]
        public async Task<ActionResult<ConjuntoIndirectosDTO>> ObtenerConjuntoIndirecto(int IdProyecto)
        {
            return await _indirectosProceso.ObtenerConjuntoIndirecto(IdProyecto);
        }

        [HttpGet("CrearConjuntoIndirecto/{IdProyecto:int}")]
        public async Task<ActionResult<RespuestaDTO>> CrearConjuntoIndirecto(int IdProyecto) { 
            return await _indirectosProceso.CrearConjuntoIndirecto(IdProyecto);
        }

        [HttpPut("EditarConjuntoIndirecto")]
        public async Task<ActionResult<RespuestaDTO>> EditarConjuntoIndirecto(ConjuntoIndirectosDTO conjunto)
        {
            return await _indirectosProceso.RecalcularConjunto(conjunto);
        }

        [HttpGet("ObtenerIndirectos/{IdProyecto:int}")]
        public async Task<ActionResult<List<IndirectosDTO>>> ObtenerIndirectos(int IdProyecto)
        {
            return await _indirectosProceso.ObtenerIndirectos(IdProyecto);
        }

        [HttpPost("CrearIndirecto")]
        public async Task<ActionResult<RespuestaDTO>> CrearIndirecto(IndirectosDTO indirecto) {
            return await _indirectosProceso.CrearIndirecto(indirecto);
        }

        [HttpPut("EditarIndirecto")]
        public async Task<ActionResult<RespuestaDTO>> EditarIndirecto(IndirectosDTO indirecto)
        {
            return await _indirectosProceso.EditarIndirecto(indirecto);
        }

        [HttpDelete("EliminarIndirecto/{idIndirecto:int}")]
        public async Task<ActionResult<RespuestaDTO>> EliminarIndirecto(int idIndirecto)
        {
            return await _indirectosProceso.EliminarIndirecto(idIndirecto);
        }
    }
}
