using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;







namespace SistemaERP.API.Controllers.Alumno39
{
    [Route("api/indirectosXConcepto/39")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IndirectosXConceptoAlumno39Controller : ControllerBase
    {
        private readonly IIndirectosXConceptoService<Alumno39Context> _indirectosXConceptoService;
        private readonly IndirectosXConceptoProceso<Alumno39Context> _indirectosXConceptoProceso;
        public IndirectosXConceptoAlumno39Controller(
            IIndirectosXConceptoService<Alumno39Context> indirectosXConceptoService,
            IndirectosXConceptoProceso<Alumno39Context> indirectosXConceptoProceso
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
