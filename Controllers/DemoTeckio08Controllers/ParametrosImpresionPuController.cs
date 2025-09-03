using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.DemoTeckioAL08
{
    [Route("api/parametrosImpresionPu/8")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ParametrosImpresionPuController : ControllerBase
    {

        private readonly ParametrosImpresionPuProceso<DemoTeckioAL08Context> _proceso;

        public ParametrosImpresionPuController(ParametrosImpresionPuProceso<DemoTeckioAL08Context> proceso)
        {
            _proceso = proceso;
        }

        //[HttpPost("crearConImagen")]
        //public async Task<ActionResult<RespuestaDTO>> CrearConImagen([FromForm]ParametrosIPuConArchivo conjunto)
        //{
        //    var resultado = await _proceso.Crear(conjunto.Modelo, conjunto.Archivo);
        //    return resultado;
        //}

        [HttpPost("crear")]
        public async Task<ActionResult<RespuestaDTO>> Crear(ParametrosImpresionPuDTO parametros)
        {
            var resultado = await _proceso.Crear(parametros);
            return resultado;
        }

        [HttpGet("todos")]

        public async Task<ActionResult<List<ParametrosImpresionPuDTO>>> ObtenerTodos()
        {
            var resultado = await _proceso.ObtenerTodos();
            return resultado;
        }

        //[HttpGet("obtenerXcliente/{idCliente:int}")]
        //public async Task<ActionResult<List<ParametrosImpresionPuDTO>>> ObtenerXCliente(int idCliente)
        //{
        //    var resultado = await _proceso.ObtenerXIdCliente(idCliente);
        //    return resultado;
        //}

        //[HttpPut("editarConImagen")]
        //public async Task<ActionResult<RespuestaDTO>> EditarConImagen([FromForm] ParametrosIPuConArchivo conjunto)
        //{
        //    var resultado = await _proceso.EditarParametrosConImagen(conjunto.Modelo, conjunto.Archivo);
        //    return resultado;
        //}

        [HttpPut("editar")]
        public async Task<ActionResult<RespuestaDTO>> Editar(ParametrosImpresionPuDTO parametros)
        {
            var resultado = await _proceso.EditarParametros(parametros);
            return resultado;
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult<RespuestaDTO>> Eliminar(int id)
        {
            var resultado = await _proceso.EliminarParametro(id);
            return resultado;
        }
    }
}
