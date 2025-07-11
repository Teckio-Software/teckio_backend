using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.GrupoTeckio
{
    [Route("api/ordenventa/2")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdenVentaGrupoTeckioController : ControllerBase
    {
        private readonly OrdenVentaProceso<GrupoTeckioContext> _ordenVentaProceso;
        public OrdenVentaGrupoTeckioController(
            OrdenVentaProceso<GrupoTeckioContext> ordenVentaProceso
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
