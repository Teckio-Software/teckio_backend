using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;





namespace SistemaERP.API.Controllers.Alumno44
{
    [Route("api/reportesdestajo/45")]
    [ApiController]
    public class ReporteDestajoAlumno45Controller : ControllerBase
    {
        private readonly ReporteDestajoProceso<Alumno38Context> _proceso;
        private readonly EstimacionesProceso<Alumno38Context> _estimacionesProceso;

        public ReporteDestajoAlumno45Controller(
            ReporteDestajoProceso<Alumno38Context> proceso,
            EstimacionesProceso<Alumno38Context> estimacionesProceso
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
