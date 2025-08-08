using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.Alumno01
{
    [Route("api/productoyservicio/1")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductoYServicioAlumno01Controller : ControllerBase
    {

        private readonly IProductoYservicioService<Alumno01Context> _productoYServicioService;
        public ProductoYServicioAlumno01Controller(IProductoYservicioService<Alumno01Context> productoYServicioService)
        {
            _productoYServicioService = productoYServicioService;
        }

        [HttpGet("obtenerTodos")]
        public async Task<ActionResult<List<ProductoYservicioDTO>>> ObtenerTodos()
        {
            var lista = await _productoYServicioService.ObtenerTodos();
            return lista;
        }


    }
}
