using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    /// <summary>
    /// Controlador de las EspecialIdades que hereda <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/especialidad/10")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionEspecialidad-Empresa1")]
    public class EspecialidadDemoTeckioAL10Controller : ControllerBase
    {
        private readonly EspecialidadProceso<DemoTeckioAL10Context> _EspecialidadProceso;
        public EspecialidadDemoTeckioAL10Controller(
            EspecialidadProceso<DemoTeckioAL10Context> especialidadProceso)
        {
            _EspecialidadProceso = especialidadProceso;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearEspecialidad-Empresa1")]
        public async Task<ActionResult> Post([FromBody] EspecialidadDTO parametros)
        {
            await _EspecialidadProceso.Post(parametros);
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarEspecialidad-Empresa1")]
        public async Task<ActionResult> Put([FromBody] EspecialidadDTO parametros)
        {
            try
            {
                await _EspecialidadProceso.Put(parametros);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpDelete("{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarEspecialidad-Empresa1")]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                await _EspecialidadProceso.Delete(Id);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<EspecialidadDTO>>> Get()
        {
            return await _EspecialidadProceso.Get();
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<EspecialidadDTO>> GetXId(int Id)
        {
            return await _EspecialidadProceso.GetXId(Id);
        }
    }
}
