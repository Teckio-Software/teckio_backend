using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/indirectosXConcepto/4")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IndirectosXConceptoAlumno04Controller : ControllerBase
    {
        private readonly IIndirectosXConceptoService<Alumno04Context> _indirectosXConceptoService;
        private readonly IndirectosXConceptoProceso<Alumno04Context> _indirectosXConceptoProceso;
        public IndirectosXConceptoAlumno04Controller(
            IIndirectosXConceptoService<Alumno04Context> indirectosXConceptoService,
            IndirectosXConceptoProceso<Alumno04Context> indirectosXConceptoProceso
            ) { 
            _indirectosXConceptoService = indirectosXConceptoService;
            _indirectosXConceptoProceso = indirectosXConceptoProceso;
        }

        [HttpGet("ObtenerIndirectos/{IdConcepto:int}")]
        public async Task<ActionResult<List<IndirectosXConceptoDTO>>> ObtenerIndirectos(int IdConcepto)
        {
            return await _indirectosXConceptoProceso.ObtenerIndirectos(IdConcepto);
        }

        [HttpGet("CrearIndirectosPadre/{IdConcepto:int}")]
        public async Task<ActionResult<RespuestaDTO>> CrearIndirectosPadre(int IdConcepto)
        {
            return await _indirectosXConceptoProceso.CrearIndirectosPadre(IdConcepto);
        }

        [HttpPost("CrearIndirecto")]
        public async Task<ActionResult<RespuestaDTO>> CrearIndirecto(IndirectosXConceptoDTO indirecto)
        {
            return await _indirectosXConceptoProceso.CrearIndirecto(indirecto);

        }

        [HttpPut("EditarIndirecto")]
        public async Task<ActionResult<RespuestaDTO>> EditarIndirecto(IndirectosXConceptoDTO indirecto)
        {
            return await _indirectosXConceptoProceso.EditarIndirecto(indirecto);
        }

        [HttpDelete("EliminarIndirecto/{idIndirecto:int}")]
        public async Task<ActionResult<RespuestaDTO>> EliminarIndirecto(int idIndirecto)
        {
            return await _indirectosXConceptoProceso.EliminarIndirecto(idIndirecto);
        }
    }
}
