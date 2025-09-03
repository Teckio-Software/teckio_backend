using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.DemoTeckioAL11
{
    [Route("api/imagen/11")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImagenController: ControllerBase
    {
        private readonly ImagenProceso<DemoTeckioAL11Context> _proceso;

        public ImagenController(ImagenProceso<DemoTeckioAL11Context> proceso)
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
