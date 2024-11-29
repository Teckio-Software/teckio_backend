


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using ERP_TECKIO;




using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace SistemaERP.API.Alumno41Controllers.Procomi
{
    /// <summary>
    /// Controlador de las EspecialIdades que hereda <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/especialidad/41")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionEspecialidad-Empresa35")]
    public class EspecialidadAlumno41Controller : ControllerBase
    {
        private readonly EspecialidadProceso<Alumno41Context> _EspecialidadProceso;
        public EspecialidadAlumno41Controller(
            EspecialidadProceso<Alumno41Context> especialidadProceso)
        {
            _EspecialidadProceso = especialidadProceso;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearEspecialidad-Empresa35")]
        public async Task<ActionResult> Post([FromBody] EspecialidadDTO parametros)
        {
            await _EspecialidadProceso.Post(parametros);
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarEspecialidad-Empresa35")]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarEspecialidad-Empresa35")]
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
