using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.DemoTeckioAL11
{
    [Route("api/detalleordencompra/11")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DetalleOrdenVentaController: ControllerBase
    {
        private readonly DetalleOrdenVentaProceso<DemoTeckioAL11Context> _Proceso;

        public DetalleOrdenVentaController(DetalleOrdenVentaProceso<DemoTeckioAL11Context> proceso)
        {
            _Proceso = proceso;
        }

        [HttpGet("obtenerPorOrdenVenta/{Id:int}")]
        public async Task<ActionResult<List<DetalleOrdenVentaDTO>>> ObtenerXOrdenVenta(int Id)
        {
            var lista = await _Proceso.ObtenerXOrdenVenta(Id);
            return lista;
        }

        [HttpPut("editar")]
        public async Task<ActionResult<RespuestaDTO>> EditarDetalle(DetalleOrdenVentaDTO detalle)
        {
            var resultado = await _Proceso.EditarDetalle(detalle);
            return resultado;
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<RespuestaDTO>> EliminarDetalle(DetalleOrdenVentaDTO detalle)
        {
            var resultado = await _Proceso.EliminarDetalle(detalle);
            return resultado;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<RespuestaDTO>> Crear(DetalleOrdenVentaDTO detalle)
        {
            var resultado = await _Proceso.Crear(detalle);
            return resultado;
        }
    }
}
