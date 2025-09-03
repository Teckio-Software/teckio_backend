using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.DemoTeckioAL12
{
    [Route("api/precioUnitarioXEmpleado/12")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class PrecioUnitarioXEmpleadoDemoTeckioAL12Controller : ControllerBase
    {
        private readonly IPrecioUnitarioXEmpleadoService<DemoTeckioAL12Context> _service;
        private readonly PrecioUnitarioXEmpleadoProceso<DemoTeckioAL12Context> _proceso;
        public PrecioUnitarioXEmpleadoDemoTeckioAL12Controller(
            IPrecioUnitarioXEmpleadoService<DemoTeckioAL12Context> service,
            PrecioUnitarioXEmpleadoProceso<DemoTeckioAL12Context> proceso
            ) { 
            _service = service;
            _proceso = proceso;
        }

        [HttpPost("Crear")]
        public async Task<ActionResult<RespuestaDTO>> Crear(PrecioUnitarioXEmpleadoDTO objeto) { 
            return await _service.Crear(objeto);
        }

        [HttpPost("CrearLista")]
        public async Task<ActionResult<RespuestaDTO>> CrearLista(List<PrecioUnitarioXEmpleadoDTO> objeto)
        {
            return await _service.CrearMultiple(objeto);
        }

        [HttpGet("ObtenerXIdEmpleado/{IdEmpleado:int}")]
        public async Task<ActionResult<List<PrecioUnitarioXEmpleadoDTO>>> ObtenerXIdEmpleado(int IdEmpleado)
        {
            return await _proceso.ObtenerXIdEmpleado(IdEmpleado);
        }

        [HttpGet("ObtenerParaAsignarPreciosUniatrios/{IdEmpleado:int}/{IdProyceto:int}")]
        public async Task<ActionResult<List<PrecioUnitarioXEmpleadoDTO>>> ObtenerParaAsignarPreciosUniatrios(int IdEmpleado, int IdProyceto)
        {
            return await _proceso.ObtenerParaAsignarPreciosUniatrios(IdEmpleado, IdProyceto);
        }

    }
}
