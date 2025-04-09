using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.Alumno01
{
    [Route("api/factura/1042")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class FacturaAlumno35Controller : ControllerBase
    {
        private readonly ObtenFacturaProceso<Alumno35Context> _obtenFacturaProceso;
        public FacturaAlumno35Controller(
            ObtenFacturaProceso<Alumno35Context> obtenFacturaProceso
            ) {
            _obtenFacturaProceso = obtenFacturaProceso;
        }

        [HttpGet("obtenerProductos")]
        public async Task<ActionResult<bool>> ObteneProductos()
        {
            var productos = await _obtenFacturaProceso.ObtenerProductos();
            return productos;
        }
    }
}
