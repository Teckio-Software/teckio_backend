using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.Alumno08
{
    [Route("api/ordenventa/8")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdenVentaAlumno08Controller : ControllerBase
    {
        private readonly OrdenVentaProceso<Alumno08Context> _ordenVentaProceso;
        public OrdenVentaAlumno08Controller(
            OrdenVentaProceso<Alumno08Context> ordenVentaProceso
            ) { 
            _ordenVentaProceso = ordenVentaProceso;
        }

        [HttpPost("crearOrdenVenta")]
        public async Task<ActionResult<RespuestaDTO>> CrearOrdenVenta(OrdenVentaDTO ordenVenta) {
            var authen = HttpContext.User;
            var respuesta = await _ordenVentaProceso.CrearOrdenVenta(ordenVenta, authen.Claims.ToList());
            return respuesta;
        }

        [HttpPut("editarOrdenVenta")]
        public async Task<ActionResult<RespuestaDTO>> EditarOrdenVenta(OrdenVentaDTO ordenVenta)
        {
            var respuesta = await _ordenVentaProceso.EditarOrdenVenta(ordenVenta);
            return respuesta;
        }

        [HttpDelete("eliminarOrdenVenta")]
        public async Task<ActionResult<RespuestaDTO>> EliminarOrdenVenta(OrdenVentaDTO ordenVenta)
        {
            var respuesta = await _ordenVentaProceso.EliminarOrdenVenta(ordenVenta);
            return respuesta;
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<OrdenVentaDTO>>> ObtenerTodos()
        {
            var lista = await _ordenVentaProceso.ObtenerTodos();
            return lista;
        }

        [HttpPut("autorizar")]
        public async Task<ActionResult<RespuestaDTO>> Autorizar(OrdenVentaDTO ordenVenta)
        {
            var authen = HttpContext.User;
            var respuesta = await _ordenVentaProceso.Autorizar(ordenVenta, authen.Claims.ToList());
            return respuesta;
        }
    }
}
