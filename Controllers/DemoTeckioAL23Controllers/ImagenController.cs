using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.DemoTeckioAL23
{
    [Route("api/imagen/23")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImagenController: ControllerBase
    {
        private readonly ImagenProceso<DemoTeckioAL23Context> _proceso;

        public ImagenController(ImagenProceso<DemoTeckioAL23Context> proceso)
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
