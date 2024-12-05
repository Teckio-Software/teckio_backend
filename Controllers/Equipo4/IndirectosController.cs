using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;





namespace SistemaERP.API.Controllers.Alumno42
{
    [Route("api/indirectos/42")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IndirectosAlumno42Controller : ControllerBase
    {
        private readonly IConjuntoIndirectosService<Alumno42Context> _conjuntoIndirectosService;
        private readonly IIndirectosService<Alumno42Context> _indirectosService;
        private readonly IndirectosProceso<Alumno42Context> _indirectosProceso;

        public IndirectosAlumno42Controller(
                        IConjuntoIndirectosService<Alumno42Context> conjuntoIndirectosService,
                        IIndirectosService<Alumno42Context> indirectosService,
                        IndirectosProceso<Alumno42Context> indirectosProceso
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
