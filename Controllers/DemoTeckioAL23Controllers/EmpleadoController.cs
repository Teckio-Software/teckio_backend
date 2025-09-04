using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using ERP_TECKIO.Servicios.Contratos;
using ERP_TECKIO.DTO.Usuario;

namespace ERP_TECKIO
{
    [Route("api/empleado/23")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class EmpleadoDemoTeckioAL23Controller : ControllerBase
    {
        private readonly IEmpleadoService<DemoTeckioAL23Context> _empleadoService;
        public EmpleadoDemoTeckioAL23Controller(
            IEmpleadoService<DemoTeckioAL23Context> empleadoService
            )
        {
            _empleadoService = empleadoService;
        }

        [HttpPost("CrearEmpleado")]
        public async Task<ActionResult<RespuestaDTO>> CrearEmpleado(EmpleadoDTO objeto) {
            return await _empleadoService.Crear(objeto);
        }

        [HttpPut("EditarEmpleado")]
        public async Task<ActionResult<RespuestaDTO>> EditarEmpleado(EmpleadoDTO objeto)
        {
            return await _empleadoService.Editar(objeto);
        }

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult<List<EmpleadoDTO>>> ObtenerTodos()
        {
            return await _empleadoService.ObtenerTodos();
        }

        [HttpGet("ObtenerXId/{IdEmpleado:int}")]
        public async Task<ActionResult<EmpleadoDTO>> ObtenerXId(int IdEmpleado)
        {
            return await _empleadoService.ObtenerXId(IdEmpleado);
        }

        [HttpGet("ObtenerXIdUser/{IdUser:int}")]
        public async Task<ActionResult<EmpleadoDTO>> ObtenerXIdUser(int IdUser)
        {
            return await _empleadoService.ObtenerXIdUser(IdUser);
        }
    }

}
