using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.GrupoTeckio
{
    [Route("api/impuestodetalleordencompra/2")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImpuestoDetalleOrdenVentaController: ControllerBase
    {
        private readonly ImpuestoDetalleOrdenVentaProceso<GrupoTeckioContext> _process;

        public ImpuestoDetalleOrdenVentaController(ImpuestoDetalleOrdenVentaProceso<GrupoTeckioContext> process)
        {
            _process = process;
        }

        [HttpGet("cbtenerXDetalle/{Id:int}")]
        public async Task<ActionResult<List<ImpuestoDetalleOrdenVentaDTO>>> ObtenerXDetalle(int Id)
        {
            var lista = await _process.ObtenerXIdDetalle(Id);
            return lista;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<RespuestaDTO>> Crear(ImpuestoDetalleOrdenVentaDTO impuesto)
        {
            var resultado = await _process.Crear(impuesto);
            return resultado;
        }

        [HttpPut("editar")]
        public async Task<ActionResult<RespuestaDTO>> Editar(ImpuestoDetalleOrdenVentaDTO impuesto)
        {
            var resultado = await _process.Editar(impuesto);
            return resultado;
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<RespuestaDTO>> Eliminar(ImpuestoDetalleOrdenVentaDTO impuesto)
        {
            var resultado = await _process.Eliminar(impuesto);
            return resultado;
        }
    }
}
