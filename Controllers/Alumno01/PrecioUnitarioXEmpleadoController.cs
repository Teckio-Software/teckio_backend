using ERP_TECKIO.DTO;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.Alumno01
{
    [Route("api/precioUnitarioXEmpleado/1")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class PrecioUnitarioXEmpleadoAlumno01Controller : ControllerBase
    {
        private readonly IPrecioUnitarioXEmpleadoService<Alumno01Context> _service;
        public PrecioUnitarioXEmpleadoAlumno01Controller(
            IPrecioUnitarioXEmpleadoService<Alumno01Context> service
            ) { 
            _service = service;
        }

        [HttpPost("Crear")]
        public async Task<ActionResult<RespuestaDTO>> Crear(PrecioUnitarioXEmpleadoDTO objeto) { 
            return await _service.Crear(objeto);
        }

        [HttpGet("ObtenerXIdEmpleado/{IdEmpleado:int}")]
        public async Task<ActionResult<List<PrecioUnitarioXEmpleadoDTO>>> ObtenerXIdEmpleado(int IdEmpleado)
        {
            return await _service.ObtenerXIdEmpleado(IdEmpleado);
        }

    }
}
