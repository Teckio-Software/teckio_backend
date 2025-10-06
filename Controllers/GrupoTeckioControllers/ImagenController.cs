using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.GrupoTeckio
{
    [Route("api/imagen/2")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImagenController: ControllerBase
    {
        private readonly ImagenProceso<GrupoTeckioContext> _proceso;

        public ImagenController(ImagenProceso<GrupoTeckioContext> proceso)
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
