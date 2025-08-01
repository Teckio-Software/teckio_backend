using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.DemoTeckio
{
    [Route("api/ordenventa/3")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdenVentaDemoTeckioController : ControllerBase
    {
        private readonly OrdenVentaProceso<DemoTeckioContext> _ordenVentaProceso;
        public OrdenVentaDemoTeckioController(
            OrdenVentaProceso<DemoTeckioContext> ordenVentaProceso
            ) { 
            _ordenVentaProceso = ordenVentaProceso;
        }

        [HttpPost("crearOrdenVenta")]
        public async Task<ActionResult<RespuestaDTO>> CrearOrdenVenta(OrdenVentaDTO ordenVenta) {
            var authen = HttpContext.User;
            var respuesta = await _ordenVentaProceso.CrearOrdenVenta(ordenVenta, authen.Claims.ToList());
            return respuesta;
        }
    }
}
