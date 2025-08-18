using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.Alumno20
{

    [Route("api/categoriaProductoServicio/20")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriaProductoYServicioController: ControllerBase
    {
        private readonly ICategoriaProductoYServicioService<Alumno20Context> _service;

        public CategoriaProductoYServicioController(ICategoriaProductoYServicioService<Alumno20Context> service)
        {
            _service = service;
        }

        [HttpGet("obtenerCategorias")]
        public async Task<ActionResult<List<CategoriaProductoYServicioDTO>>> ObtenerTodos()
        {
            var lista = await _service.ObtenerTodos();
            return lista;
        }

        [HttpPost("crearYObtener")]
        public async Task<ActionResult<CategoriaProductoYServicioDTO>> CrearYObtener(CategoriaProductoYServicioDTO categoria)
        {
            var resultado = await _service.CrearYObtener(categoria);
            return resultado;
        }
    }
}
