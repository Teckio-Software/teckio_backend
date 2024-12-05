using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;





namespace ERP_TECKIO.API.Controllers.Alumno35
{
    [Route("api/reportesdestajo/35")]
    [ApiController]
    public class ReporteDestajoAlumno35Controller : ControllerBase
    {
        private readonly ReporteDestajoProceso<Alumno35Context> _proceso;
        private readonly EstimacionesProceso<Alumno35Context> _estimacionesProceso;

        public ReporteDestajoAlumno35Controller(
            ReporteDestajoProceso<Alumno35Context> proceso,
            EstimacionesProceso<Alumno35Context> estimacionesProceso
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
