using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.IyAToluca
{
    [Route("api/ordenventa/1")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdenVentaIyATolucaController : ControllerBase
    {
        private readonly OrdenVentaProceso<IyATolucaContext> _ordenVentaProceso;
        public OrdenVentaIyATolucaController(
            OrdenVentaProceso<IyATolucaContext> ordenVentaProceso
            ) { 
            _ordenVentaProceso = ordenVentaProceso;
        }

        [HttpPost("crearOrdenVenta")]
        public async Task<ActionResult<RespuestaDTO>> CrearOrdenVenta(OrdenVentaDTO ordenVenta) {
            var authen = HttpContext.User;
            var respuesta = await _ordenVentaProceso.CrearOrdenVenta(ordenVenta, authen.Claims.ToList());
            return respuesta;
        }

        [HttpPut("editarOrdenVenta")]
        public async Task<ActionResult<RespuestaDTO>> EditarOrdenVenta(OrdenVentaDTO ordenVenta)
        {
            var respuesta = await _ordenVentaProceso.EditarOrdenVenta(ordenVenta);
            return respuesta;
        }

        [HttpDelete("eliminarOrdenVenta")]
        public async Task<ActionResult<RespuestaDTO>> EliminarOrdenVenta(OrdenVentaDTO ordenVenta)
        {
            var respuesta = await _ordenVentaProceso.EliminarOrdenVenta(ordenVenta);
            return respuesta;
        }

        [HttpGet("obtenerOrdenVenta/{IdOrdenVenta:int}")]
        public async Task<ActionResult<OrdenVentaDTO>> ObtenerTodos(int IdOrdenVenta)
        {
            var respuesta = await _ordenVentaProceso.obtenerOrdenVentaXId(IdOrdenVenta);
            return respuesta;
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<OrdenVentaDTO>>> ObtenerTodos()
        {
            var lista = await _ordenVentaProceso.ObtenerTodos();
            return lista;
        }

        //[HttpPut("autorizar")]
        //public async Task<ActionResult<RespuestaDTO>> Autorizar(OrdenVentaDTO ordenVenta)
        //{
        //    var authen = HttpContext.User;
        //    var respuesta = await _ordenVentaProceso.Autorizar(ordenVenta, authen.Claims.ToList());
        //    return respuesta;
        //}

        //Este endpoint ya realiza las validaciones de existencias, provisionalmente comente el otro
        [HttpPut("autorizar")]
        public async Task<ActionResult<RespuestaDTO>> Autorizar(SalidaProduccionAlmacenAutorizarOrdenVDTO ordenVenta)
        {
            var authen = HttpContext.User;
            var respuesta = await _ordenVentaProceso.Autorizar(ordenVenta, authen.Claims.ToList());
            return respuesta;
        }

        [HttpPut("cancelar")]
        public async Task<ActionResult<RespuestaDTO>> Cancelar(OrdenVentaCancelarDTO ordenVenta)
        {
            var respuesta = await _ordenVentaProceso.Cancelar(ordenVenta);
            return respuesta;
        }
    }
}
