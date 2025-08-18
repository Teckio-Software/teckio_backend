using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.Alumno05
{

    [Route("api/categoriaProductoServicio/5")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriaProductoYServicioController: ControllerBase
    {
        private readonly ICategoriaProductoYServicioService<Alumno05Context> _service;

        public CategoriaProductoYServicioController(ICategoriaProductoYServicioService<Alumno05Context> service)
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
