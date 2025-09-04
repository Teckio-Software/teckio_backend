using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.DemoTeckioAL20
{

    [Route("api/produccion/20")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProduccionController: ControllerBase
    {
        private readonly ProduccionProceso<DemoTeckioAL20Context> _proceso;

        public ProduccionController(ProduccionProceso<DemoTeckioAL20Context> proceso)
        {
            _proceso = proceso;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<RespuestaDTO>> Crear(ProduccionDTO produccion)
        {
            var resultado = await _proceso.Crear(produccion);
            return resultado;
        }

        [HttpPost("crearYObtener")]
        public async Task<ActionResult<ProduccionDTO>> CrearYObtener(ProduccionDTO produccion)
        {
            var resultado = await _proceso.CrearYObtener(produccion);
            return resultado;
        }

        [HttpPut("editar")]
        public async Task<ActionResult<RespuestaDTO>> Editar(ProduccionConAlmacenDTO produccion)
        {
            var resultado = await _proceso.Editar(produccion);
            return resultado;
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult<RespuestaDTO>> Eliminar(int id)
        {
            var resultado = await _proceso.Eliminar(id);
            return resultado;
        }

        [HttpPut("editarEstatus")]
        public async Task<ActionResult<RespuestaDTO>> EditarEstatus(ProduccionConAlmacenDTO produccion)
        {
            var authen = HttpContext.User;
            var resultado = await _proceso.CambiarEstatus(produccion, authen.Claims.ToList());
            return resultado;
        }
    }
}
