

using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;






using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ERP_TECKIO
{
    [Route("api/concepto/37")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionConcepto-Empresa35")]
    public class ConceptoAlumno37Controller : ControllerBase
    {
        private readonly ConceptoProceso<Alumno37Context> _ConceptoProceso;
        public ConceptoAlumno37Controller(
            ConceptoProceso<Alumno37Context> conceptoProceso)
        {
            _ConceptoProceso = conceptoProceso;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearConcepto-Empresa35")]
        public async Task<ActionResult> Post([FromBody] ConceptoDTO parametros)
        {
            await _ConceptoProceso.Post(parametros);
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarConcepto-Empresa35")]
        public async Task<ActionResult> Put([FromBody] ConceptoDTO parametros)
        {
            try
            {
                await _ConceptoProceso.Put(parametros);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpDelete("{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarConcepto-Empresa35")]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                await _ConceptoProceso.Delete(Id);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpGet("todos/{IdProyecto:int}")]
        public async Task<ActionResult<List<ConceptoDTO>>> Get(int IdProyecto)
        {
            return await _ConceptoProceso.Get(IdProyecto);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ConceptoDTO>> GetXId(int Id)
        {
            return await _ConceptoProceso.GetXId(Id);
        }
    }
}
