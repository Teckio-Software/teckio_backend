using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/concepto/11")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionConcepto-Empresa1")]
    public class ConceptoDemoTeckioAL11Controller : ControllerBase
    {
        private readonly ConceptoProceso<DemoTeckioAL11Context> _ConceptoProceso;
        public ConceptoDemoTeckioAL11Controller(
            ConceptoProceso<DemoTeckioAL11Context> conceptoProceso)
        {
            _ConceptoProceso = conceptoProceso;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearConcepto-Empresa1")]
        public async Task<ActionResult> Post([FromBody] ConceptoDTO parametros)
        {
            await _ConceptoProceso.Post(parametros);
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarConcepto-Empresa1")]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarConcepto-Empresa1")]
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
