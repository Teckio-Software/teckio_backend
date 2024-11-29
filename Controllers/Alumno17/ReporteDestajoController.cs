using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;





namespace ERP_TECKIO.API.Controllers.Alumno17
{
    [Route("api/reportesdestajo/17")]
    [ApiController]
    public class ReporteDestajoAlumno17Controller : ControllerBase
    {
        private readonly ReporteDestajoProceso<Alumno17Context> _proceso;
        private readonly EstimacionesProceso<Alumno17Context> _estimacionesProceso;

        public ReporteDestajoAlumno17Controller(
            ReporteDestajoProceso<Alumno17Context> proceso,
            EstimacionesProceso<Alumno17Context> estimacionesProceso
            ) { 
            _proceso = proceso;
            _estimacionesProceso = estimacionesProceso;
        }

        [HttpPost("reporteDestajo")]
        public async Task<ActionResult<List<ReporteDestajoDTO>>> reporteDestajo(ReporteDestajoDTO parametrosBusqueda)
        {
            var reporte = await _proceso.reporteDestajo(parametrosBusqueda);
            return reporte;
        }

        [HttpPost("destajoAcumulado")]
        public async Task<ActionResult<ObjetoDestajoacumuladoDTO>> DestajoAcumulado(ParametrosParaBuscarContratos parametros) {
            var detajoAcumulado = await _proceso.DestajoAcumulado(parametros);
            return detajoAcumulado;
        }

        [HttpGet("ObtenerPeridosConImporteTotal/{IdProyecto:int}")]
        public async Task<ActionResult<List<PeriodosResumenDTO>>> ObtenerPeridosConImporteTotal(int IdProyecto)
        {
            return await _estimacionesProceso.ObtenerPeridosConImporteTotal(IdProyecto);
        }

        [HttpPost("DestajoTotal")]
        public async Task<ActionResult<ObjetoDestajoTotalDTO>> DestajoTotal(ParametrosParaBuscarContratos parametros)
        {
            var detajoAcumulado = await _proceso.DestajoTotal(parametros);
            return detajoAcumulado;
        }
    }
}
