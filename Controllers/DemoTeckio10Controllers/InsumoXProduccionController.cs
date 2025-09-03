using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ERP_TECKIO.Controllers.DemoTeckioAL10
{

    [Route("api/insumoXproduccion/10")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InsumoXProduccionController: ControllerBase
    {
        private readonly InsumoXProduccionProceso<DemoTeckioAL10Context> _proceso;

        public InsumoXProduccionController(InsumoXProduccionProceso<DemoTeckioAL10Context> proces)
        {
            _proceso = proces;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<RespuestaDTO>> Crear(InsumoXProduccionDTO insumo)
        {
            var resultado = await _proceso.Crear(insumo);
            return resultado;
        }

        [HttpPost("crearYObtener")]
        public async Task<ActionResult<InsumoXProduccionDTO>> CrearYObtener(InsumoXProduccionDTO insumo)
        {
            var resultado = await _proceso.CrearYObtener(insumo);
            return resultado;
        }

        [HttpPut("editar")]
        public async Task<ActionResult<RespuestaDTO>> Editar(InsumoXProduccionDTO insumo)
        {
            var resultado = await _proceso.Editar(insumo);
            return resultado;
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult<RespuestaDTO>> Eliminar(int id)
        {
            var resultado = await _proceso.Eliminar(id);
            return resultado;
        }

        [HttpGet("obtenerXProduccion/{id:int}")]
        public async Task<ActionResult<List<InsumoXProduccionDTO>>> ObtenerXPRoduccion(int id)
        {
            var lista = await _proceso.ObtenerXProduccion(id);
            return lista;
        }
    }
}
