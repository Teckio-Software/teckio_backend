using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.DemoTeckio
{
    [Route("api/productoyservicio/3")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductoYServicioDemoTeckioController : ControllerBase
    {
        private ProductoYServicioProceso<Alumno01Context> _process;

        public ProductoYServicioDemoTeckioController(ProductoYServicioProceso<Alumno01Context> process)
        {
            _process = process;
        }

        [HttpGet("obtenerTodos")]
        public async Task<ActionResult<List<ProductoYservicioDTO>>> ObtenerTodos()
        {
            var lista = await _process.ObtenerTodos();
            return lista;
        }
    }
}
