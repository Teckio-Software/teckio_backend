using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.DemoTeckioAL14
{
    [Route("api/imagen/14")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImagenController: ControllerBase
    {
        private readonly ImagenProceso<DemoTeckioAL14Context> _proceso;

        public ImagenController(ImagenProceso<DemoTeckioAL14Context> proceso)
        {
            _proceso = proceso;
        }

        [HttpGet("obtenerXId/{id:int}")]
        public async Task<ActionResult<ImagenDTO>> ObtenerXId(int id)
        {
            var resultado = await _proceso.ObtenerXId(id);
            return resultado;
        }
    }
}
