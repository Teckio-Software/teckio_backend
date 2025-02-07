using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers.Alumno21
{
    [Route("api/fsr/21")]
    [ApiController]
    public class FactorSalarioRealController : ControllerBase
    {
        private readonly FactorSalarioRealProceso<Alumno21Context> _FactorSalarioRealProceso;
        
        public FactorSalarioRealController(
            FactorSalarioRealProceso<Alumno21Context> factorSalarioRealProceso)
        {
            _FactorSalarioRealProceso = factorSalarioRealProceso;
        }

        [HttpGet("obtenerFSR/{IdProyecto:int}")]
        public async Task<ActionResult<FactorSalarioRealDTO>> ObtenerFSR(int IdProyecto)
        {
            return await _FactorSalarioRealProceso.ObtenerFSR(IdProyecto);
        }

        [HttpGet("obtenerFSRdetalles/{IdFSR:int}")]
        public async Task<ActionResult<List<FactorSalarioRealDetalleDTO>>> ObtenerDetallesFSR(int IdFSR)
        {
            return await _FactorSalarioRealProceso.ObtenerDetalles(IdFSR);
        }

        [HttpGet("obtenerFSI/{IdProyecto:int}")]
        public async Task<ActionResult<FactorSalarioIntegradoDTO>> ObtenerFSI(int IdProyecto)
        {
            return await _FactorSalarioRealProceso.ObtenerFSI(IdProyecto);
        }

        [HttpGet("obtenerdiasnolaborables/{IdFSI:int}")]
        public async Task<ActionResult<List<DiasConsideradosDTO>>> ObtenerDiasNoLaborables(int IdFSI)
        {
            return await _FactorSalarioRealProceso.ObtenerDiasNoLaborables(IdFSI);
        }

        [HttpGet("obtenerdiaspagados/{IdFSI:int}")]
        public async Task<ActionResult<List<DiasConsideradosDTO>>> ObtenerDiasPagados(int IdFSI)
        {
            return await _FactorSalarioRealProceso.ObtenerDiasPagados(IdFSI);
        }

        [HttpPost("creardetalleFSR")]
        public async Task CrearDetalleFSR(FactorSalarioRealDetalleDTO nuevoDetalle)
        {
            await _FactorSalarioRealProceso.CrearDetalleFSR(nuevoDetalle);
        }

        [HttpPost("editardetalleFSR")]
        public async Task EditarDetalleFSR(FactorSalarioRealDetalleDTO detalleEditado)
        {
            await _FactorSalarioRealProceso.EditarDetalleFSR(detalleEditado);
        }

        [HttpPost("creardiasFSI")]
        public async Task CrearDiasFSI(DiasConsideradosDTO nuevosDias)
        {
            await _FactorSalarioRealProceso.AgregarDiasFSI(nuevosDias);
        }

        [HttpPost("editardiasFSI")]
        public async Task EditarDiasFSI(DiasConsideradosDTO diaEditado)
        {
            await _FactorSalarioRealProceso.EditarDiasFSI(diaEditado);
        }
    }
}
