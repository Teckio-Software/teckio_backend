using Microsoft.AspNetCore.Mvc;
using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;
using ERP_TECKIO.DTO;


namespace ERP_TECKIO.Controllers.Alumno24
{
    [Route("api/fsr/24")]
    [ApiController]
    public class FactorSalarioRealController : ControllerBase
    {
        private readonly FactorSalarioRealProceso<Alumno24Context> _FactorSalarioRealProceso;
        
        public FactorSalarioRealController(
            FactorSalarioRealProceso<Alumno24Context> factorSalarioRealProceso)
        {
            _FactorSalarioRealProceso = factorSalarioRealProceso;
        }

        [HttpPost("crearFsrDetalleXInsumo")]
        public async Task<ActionResult<RespuestaDTO>> CrearFsrDetalleXInsumo(FsrxinsummoMdOdetalleDTO objeto) { 
            return await _FactorSalarioRealProceso.CrearFsrDetalle(objeto);
        }

        [HttpPost("crearFsiDetalleXInsumo")]
        public async Task<ActionResult<RespuestaDTO>> CrearFsiDetalleXInsumo(FsixinsummoMdOdetalleDTO objeto)
        {
            return await _FactorSalarioRealProceso.CrearFsiDetalle(objeto);
        }

        [HttpPut("editarFsrDetalleXInsumo")]
        public async Task<ActionResult<RespuestaDTO>> EditarFsrDetalleXInsumo(FsrxinsummoMdOdetalleDTO objeto)
        {
            return await _FactorSalarioRealProceso.EditarFsrDetalle(objeto);
        }

        [HttpPut("editarFsiDetalleXInsumo")]
        public async Task<ActionResult<RespuestaDTO>> EditarFsiDetalleXInsumo(FsixinsummoMdOdetalleDTO objeto)
        {
            return await _FactorSalarioRealProceso.EditarFsiDetalle(objeto);
        }

        [HttpDelete("eliminarFsrDetalleXInsumo/{IdFsrDetalle:int}")]
        public async Task<ActionResult<RespuestaDTO>> EliminarFsrDetalleXInsumo(int IdFsrDetalle)
        {
            return await _FactorSalarioRealProceso.EliminarFsrDetalle(IdFsrDetalle);
        }

        [HttpDelete("eliminarFsiDetalleXInsumo/{IdFsiDetalle:int}")]
        public async Task<ActionResult<RespuestaDTO>> EliminarFsiDetalleXInsumo(int IdFsiDetalle)
        {
            return await _FactorSalarioRealProceso.EliminarFsiDetalle(IdFsiDetalle);
        }

        [HttpGet("ObtenerFactorSalarioXInsumo/{IdInsumo:int}")]
        public async Task<ActionResult<ObjetoFactorSalarioXInsumoDTO>> ObtenerFactorSalarioXInsumo(int IdInsumo)
        {
            return await _FactorSalarioRealProceso.ObtenerFactorSalario(IdInsumo);
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

        [HttpDelete("eliminarDetalleFSI/{IdDetalleFSI:int}")]
        public async Task EliminarDiasFSI(int IdDetalleFSI)
        {
            await _FactorSalarioRealProceso.EliminarDiasFSI(IdDetalleFSI);
        }

        [HttpDelete("eliminarDetalleFSR/{IdDetalleFSR:int}")]
        public async Task EliminarDiasFSR(int IdDetalleFSR)
        {
            await _FactorSalarioRealProceso.EliminarDetalleFSR(IdDetalleFSR);
        }
    }
}
