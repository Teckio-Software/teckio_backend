using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.Alumno12
{

    [Route("api/unidadSat/12")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UnidadSatController: ControllerBase
    {
        private readonly IUnidadSatService<Alumno12Context> _service;

        public UnidadSatController(IUnidadSatService<Alumno12Context> service)
        {
            _service = service;
        }

        [HttpGet("obtenerTodos")]
        public async Task<ActionResult<List<UnidadSatDTO>>> ObtenerTodos()
        {
            var lista = await _service.ObtenerTodos();
            return lista;
        }
    }
}
