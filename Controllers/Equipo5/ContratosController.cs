using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

using SistemaERP.DTO.Presupuesto;



namespace SistemaERP.API.Controllers.Alumno43
{
    [Route("api/contratos/43")]
    [ApiController]
    public class ContratosAlumno43Controller : ControllerBase
    {
        private readonly ContratosProceso<Alumno43Context> _contratosProceso;
        public ContratosAlumno43Controller(
            ContratosProceso<Alumno43Context> contratosProceso
            )
        {
            _contratosProceso = contratosProceso;
        }

        [HttpPost("obtenerContratosDestajos")]
        public async Task<ActionResult<List<ContratoDTO>>> ObtenerPeriodos([FromBody] ParametrosParaBuscarContratos registro)
        {
            return await _contratosProceso.ObtenerContratosDestajos(registro);
        }

        [HttpPost("crearContratoDestajo")]
        public async Task<ActionResult> CrearContratoDestajo([FromBody] ContratoDTO nuevoContrato)
        {
            await _contratosProceso.CrearContratoDestajo(nuevoContrato);
            return NoContent();
        }

        [HttpPost("editarContratoDestajo")]
        public async Task<ActionResult> EditarContratoDestajo([FromBody] ContratoDTO nuevoContrato)
        {
            await _contratosProceso.EditarContratoDestajo(nuevoContrato);
            return NoContent();
        }

        [HttpGet("ObtenerDetallesDestajos/{IdContrato:int}")]
        public async Task<ActionResult<List<DetalleXContratoParaTablaDTO>>> ObtenerDetallesDestajos(int idContrato)
        {
            return await _contratosProceso.ObtenerDetallesDestajos(idContrato);
        }

        [HttpPost("crearOEditarDetalle")]
        public async Task<ActionResult> CrearOEditarDetalle([FromBody] DetalleXContratoParaTablaDTO registro)
        {
            await _contratosProceso.CrearOEditarDetalle(registro);
            return NoContent();
        }

        [HttpPost("finiquitarXContrato")]
        public async Task<ActionResult<RespuestaDTO>> finiquitarXContrato([FromBody] ParametrosParaBuscarContratos parametros)
        {
            return await _contratosProceso.finiquitarXContrato(parametros);
        }

        [HttpPost("ObtenerDestajistasXConcepto")]
        public async Task<ActionResult<List<DestajistasXConceptoDTO>>> ObtenerDestajistasXConcepto([FromBody] DetalleXContratoParaTablaDTO parametros)
        {
            return await _contratosProceso.ObtenerDestajistasXConcepto(parametros);
        }
    }
}
