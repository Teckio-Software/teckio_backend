using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;





using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ERP_TECKIO
{
    /// <summary>
    /// Controlador de los Insumos que hereda <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/generadores/11")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPrecioUnitario-Empresa11")]
    public class GeneradoresAlumno11Controller : ControllerBase
    {
        private readonly GeneradoresProceso<Alumno11Context> _GeneradoresProceso;
        public GeneradoresAlumno11Controller(
            GeneradoresProceso<Alumno11Context> generadoresProceso)
        {
            _GeneradoresProceso = generadoresProceso;
        }

        [HttpPost("crear")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearPrecioUnitario-Empresa11")]
        public async Task<ActionResult<GeneradoresDTO>> Post([FromBody] GeneradoresDTO parametro)
        {
            return await _GeneradoresProceso.Post(parametro);
        }

        [HttpPut("editar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarPrecioUnitario-Empresa11")]
        public async Task<ActionResult<GeneradoresDTO>> Put([FromBody] GeneradoresDTO parametros)
        {
            return await _GeneradoresProceso.Put(parametros);
        }

        [HttpGet("todos/{IdPrecioUnitario:int}")]
        public async Task<ActionResult<List<GeneradoresDTO>>> Get(int IdPrecioUnitario)
        {
            return await _GeneradoresProceso.Get(IdPrecioUnitario);
        }

        [HttpDelete("{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EliminarPrecioUnitario-Empresa11")]
        public async Task Delete(int Id)
        {
            await _GeneradoresProceso.Delete(Id);
        }
    }
}
