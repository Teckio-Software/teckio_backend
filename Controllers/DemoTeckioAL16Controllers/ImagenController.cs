using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.DemoTeckioAL16
{
    [Route("api/imagen/16")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImagenController: ControllerBase
    {
        private readonly ImagenProceso<DemoTeckioAL16Context> _proceso;

        public ImagenController(ImagenProceso<DemoTeckioAL16Context> proceso)
        {
            _proceso = proceso;
        }

        [HttpGet("obtenerseleccionada")]
        public async Task<ActionResult<ImagenDTO>> ObtenerXId()
        {
            var authen = HttpContext.User;
            var resultado = await _proceso.ObtenerSeleccionada(authen.Claims.ToList());
            return resultado;
        }
    }
}
