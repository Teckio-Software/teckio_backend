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

        [HttpGet("obtenerseleccionada")]
        public async Task<ActionResult<ImagenDTO>> ObtenerXId()
        {
            var authen = HttpContext.User;
            var resultado = await _proceso.ObtenerSeleccionada(authen.Claims.ToList());
            return resultado;
        }

        [HttpPost("CargarImagen")]
        public async Task<ActionResult<RespuestaDTO>> cargarImagen(IFormFile file)
        {
            var authen = HttpContext.User;
            var resultado = await _proceso.GuardarImagen(file, authen.Claims.ToList());
            return resultado;
        }

        [HttpGet("ObtenerImagenes")]
        public async Task<ActionResult<List<ImagenDTO>>> obtenerImagenes()
        {
            var authen = HttpContext.User;
            var resultado = await _proceso.ObtenerImagenes(authen.Claims.ToList());
            return resultado;
        }
    }
}
