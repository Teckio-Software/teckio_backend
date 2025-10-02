using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.IyAToluca
{
    [Route("api/imagen/1")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImagenController: ControllerBase
    {
        private readonly ImagenProceso<IyATolucaContext> _proceso;

        public ImagenController(ImagenProceso<IyATolucaContext> proceso)
        {
            _proceso = proceso;
        }

        [HttpGet("obtenerXId/{id:int}")]
        public async Task<ActionResult<ImagenDTO>> ObtenerXId(int id)
        {
            var resultado = await _proceso.ObtenerXId(id);
            return resultado;
        }

        [HttpPost("CargarImagen")]
        public async Task<ActionResult<int>> cargarImagen(IFormFile file)
        {
            var resultado = await _proceso.GuardarImagen(file);
            return resultado;
        }
    }
}
