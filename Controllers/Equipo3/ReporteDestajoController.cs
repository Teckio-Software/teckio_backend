using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using SistemaERP.DTO.Presupuesto;

namespace SistemaERP.API.Controllers.Alumno41
{
    [Route("api/reportesdestajo/41")]
    [ApiController]
    public class ReporteDestajoAlumno41Controller : ControllerBase
    {
        private readonly ReporteDestajoProceso<Alumno41Context> _proceso;
        private readonly EstimacionesProceso<Alumno41Context> _estimacionesProceso;

        public ReporteDestajoAlumno41Controller(
            ReporteDestajoProceso<Alumno41Context> proceso,
            EstimacionesProceso<Alumno41Context> estimacionesProceso
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
