using ERP_TECKIO.DTO.Documentacion;
using ERP_TECKIO.Servicios.Contratos.Documentacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.IyATolucaControllers
{

    [Route("api/glosario/1")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GlosarioController : ControllerBase
    {
        private readonly IGlosarioService<IyATolucaContext> _glosarioService;
        public GlosarioController(IGlosarioService<IyATolucaContext> glosarioService)
        {
            _glosarioService = glosarioService;
        }

        [HttpPost("CrearYObtener")]
        public async Task<ActionResult<GlosarioDTO>> CrearYObtener([FromBody] GlosarioDTO glosarioDTO)
        {
            var resultado = await _glosarioService.CrearYObtener(glosarioDTO);
            return resultado;
        }

        [HttpPut("Editar")]
        public async Task<ActionResult<RespuestaDTO>> Editar([FromBody] GlosarioDTO glosarioDTO)
        {
            var resultado = await _glosarioService.Editar(glosarioDTO);
            return resultado;
        }

        [HttpDelete("Eliminar/{IdGlosario:int}")]
        public async Task<ActionResult<RespuestaDTO>> Eliminar(int IdGlosario)
        {
            var resultado = await _glosarioService.Eliminar(IdGlosario);
            return resultado;
        }

        [HttpGet("ObtenerXId/{IdGlosario:int}")]
        public async Task<ActionResult<GlosarioDTO>> ObtenerXId(int IdGlosario)
        {
            var resultado = await _glosarioService.ObtenerXId(IdGlosario);
            return resultado;
        }

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult<List<GlosarioDTO>>> ObtenerTodos()
        {
            var resultado = await _glosarioService.ObtenerTodos();
            return resultado;
        }

    }
}
