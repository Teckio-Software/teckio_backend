using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;





namespace ERP_TECKIO.API.Controllers.Alumno29
{
    [Route("api/contratos/29")]
    [ApiController]
    public class ContratosAlumno29Controller : ControllerBase
    {
        private readonly ContratosProceso<Alumno29Context> _contratosProceso;
        public ContratosAlumno29Controller(
            ContratosProceso<Alumno29Context> contratosProceso
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
