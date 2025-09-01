using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.Alumno12
{

    [Route("api/categoriaProductoServicio/12")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriaProductoYServicioController: ControllerBase
    {
        private readonly ICategoriaProductoYServicioService<Alumno12Context> _service;

        public CategoriaProductoYServicioController(ICategoriaProductoYServicioService<Alumno12Context> service)
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
