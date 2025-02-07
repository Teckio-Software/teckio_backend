using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/estimaciones/13")]
    [ApiController]
    public class EstimacionesAlumno13Controller : ControllerBase
    {
        private readonly EstimacionesProceso<Alumno13Context> _estimacionesProceso;
        public EstimacionesAlumno13Controller(
            EstimacionesProceso<Alumno13Context> estimacionesProceso
            )
        {
            _estimacionesProceso = estimacionesProceso;
        }

        [HttpGet("todosPeriodos/{IdProyecto:int}")]
        public async Task<ActionResult<List<PeriodoEstimacionesDTO>>> ObtenerPeriodos(int IdProyecto)
        {
            return await _estimacionesProceso.ObtenerPeriodosXIdProyecto(IdProyecto);
        }

        [HttpGet("todasEstimaciones/{IdPeriodo:int}")]
        public async Task<ActionResult<List<EstimacionesDTO>>> ObtenerEstimaciones(int IdPeriodo)
        {
            return await _estimacionesProceso.ObtenerEstimacionesXIdPeriodo(IdPeriodo);
        }

        [HttpPost("crearPeriodo")]
        public async Task<ActionResult<PeriodoEstimacionesDTO>> CrearPeriodo([FromBody] PeriodoEstimacionesDTO registro)
        {
            return await _estimacionesProceso.CrearPeriodo(registro);
        }

        [HttpPost("editarEstimacion")]
        public async Task<ActionResult<List<EstimacionesDTO>>> EditarEstimacion([FromBody] EstimacionesDTO registro)
        {
            return await _estimacionesProceso.EditarEstimacion(registro);
        }

        [HttpDelete("eliminarPeriodo/{IdPeriodo:int}")]
        public async Task<ActionResult> EliminarPeriodo(int IdPeriodo)
        {
            await _estimacionesProceso.EliminarPeriodo(IdPeriodo);
            return NoContent();
        }
    }
}
