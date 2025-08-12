using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.Alumno01
{

    [Route("api/productoYServicioSat/1")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductoYServicioSatController: ControllerBase
    {
        private readonly IProductoYServicioSatService<Alumno01Context> _service;

        public ProductoYServicioSatController(IProductoYServicioSatService<Alumno01Context> service)
        {
            _service = service;
        }

        [HttpGet("obtenerTodos")]
        public async Task<ActionResult<List<ProductoYServicioSatDTO>>> ObtenerTodos()
        {
            var lista = await _service.ObtenerTodos();
            return lista;
        }

        
    }
}
