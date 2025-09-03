using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.DemoTeckioAL07
{

    [Route("api/subcategoriaProductoServicio/7")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SubcategoriaProductoServicioController: ControllerBase
    {
        private readonly ISubcategoriaProdutoYServicio<DemoTeckioAL07Context> _service;

        public SubcategoriaProductoServicioController(ISubcategoriaProdutoYServicio<DemoTeckioAL07Context> service)
        {
            _service = service;
        }

        [HttpGet("obtenerSubcategorias")]
        public async Task<ActionResult<List<SubcategoriaProductoYServicioDTO>>> ObtenerTodos()
        {
            var lista = await _service.ObtenerTodos();
            return lista;
        }

        [HttpPost("crearYObtener")]
        public async Task<ActionResult<SubcategoriaProductoYServicioDTO>> CrearYObtener(SubcategoriaProductoYServicioDTO subcategoria)
        {
            var resultado = await _service.CrearYObtener(subcategoria);
            return resultado;
        }
    }
}
