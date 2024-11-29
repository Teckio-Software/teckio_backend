using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using SistemaERP.DTO.Presupuesto;

namespace SistemaERP.API.Controllers.Alumno44
{
    [Route("api/reportesdestajo/44")]
    [ApiController]
    public class ReporteDestajoAlumno44Controller : ControllerBase
    {
        private readonly ReporteDestajoProceso<Alumno44Context> _proceso;
        private readonly EstimacionesProceso<Alumno44Context> _estimacionesProceso;

        public ReporteDestajoAlumno44Controller(
            ReporteDestajoProceso<Alumno44Context> proceso,
            EstimacionesProceso<Alumno44Context> estimacionesProceso
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
