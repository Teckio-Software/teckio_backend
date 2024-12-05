

using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;






using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ERP_TECKIO
{
    [Route("api/concepto/19")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionConcepto-Empresa19")]
    public class ConceptoAlumno19Controller : ControllerBase
    {
        private readonly ConceptoProceso<Alumno19Context> _ConceptoProceso;
        public ConceptoAlumno19Controller(
            ConceptoProceso<Alumno19Context> conceptoProceso)
        {
            _ConceptoProceso = conceptoProceso;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearConcepto-Empresa19")]
        public async Task<ActionResult> Post([FromBody] ConceptoDTO parametros)
        {
            await _ConceptoProceso.Post(parametros);
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarConcepto-Empresa19")]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarConcepto-Empresa19")]
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
