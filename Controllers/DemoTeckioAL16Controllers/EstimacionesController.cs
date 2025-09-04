using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/estimaciones/16")]
    [ApiController]
    public class EstimacionesDemoTeckioAL16Controller : ControllerBase
    {
        private readonly EstimacionesProceso<DemoTeckioAL16Context> _estimacionesProceso;
        public EstimacionesDemoTeckioAL16Controller(
            EstimacionesProceso<DemoTeckioAL16Context> estimacionesProceso
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

        [HttpGet("todasEstimacionesReportes/{IdPeriodo:int}")]
        public async Task<ActionResult<List<EstimacionesDTO>>> ObtenerEstimacionesReporte(int IdPeriodo)
        {
            return await _estimacionesProceso.ObtenerEstimacionesXIdPeriodoXReporte(IdPeriodo);
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

        [HttpGet("ObtenerPeriodosXEstimacion/{IdEstimacion:int}")]
        public async Task<ActionResult<List<PeriodosXEstimacionDTO>>> ObtenerPeriodosXEstimacion(int IdEstimacion)
        {
            return await _estimacionesProceso.ObtenerPeridosXEstimacion(IdEstimacion);
        }

        [HttpGet("ObtenerGeneradoresXEstimacion/{IdEstimacion:int}")]
        public async Task<ActionResult<List<GeneradoresXEstimacionDTO>>> ObtenerGeneradoresXEstimacion(int IdEstimacion)
        {
            return await _estimacionesProceso.ObtenerGeneradoresXEstimacion(IdEstimacion);
        }

        [HttpPost("CrearGeneradorXEstimacion")]
        public async Task<ActionResult<List<GeneradoresXEstimacionDTO>>> CrearGeneradorXEstimacion(GeneradoresXEstimacionDTO generador)
        {
            return await _estimacionesProceso.CrearGeneradoresXEstimacion(generador);
        }

        [HttpDelete("EliminarGeneradorXEstimacion/{IdGenerador:int}")]
        public async Task<ActionResult<List<GeneradoresXEstimacionDTO>>> EliminarGeneradorXEstimacion(int IdGenerador)
        {
            return await _estimacionesProceso.EliminarGeneradoresXEstimacion(IdGenerador);
        }

    }
}
