using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.GrupoTeckio
{
    [Route("api/precioUnitarioXEmpleado/2")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class PrecioUnitarioXEmpleadoGrupoTeckioController : ControllerBase
    {
        private readonly IPrecioUnitarioXEmpleadoService<GrupoTeckioContext> _service;
        private readonly PrecioUnitarioXEmpleadoProceso<GrupoTeckioContext> _proceso;
        public PrecioUnitarioXEmpleadoGrupoTeckioController(
            IPrecioUnitarioXEmpleadoService<GrupoTeckioContext> service,
            PrecioUnitarioXEmpleadoProceso<GrupoTeckioContext> proceso
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
