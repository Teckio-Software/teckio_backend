using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using static ERP_TECKIO.Procesos.ObtenFacturaProceso;

namespace ERP_TECKIO.Controllers.Alumno01
{
    [Route("api/factura/1")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class FacturaAlumno01Controller : ControllerBase
    {
        //private readonly ObtenFacturaProceso _obtenFacturaProceso;
        //public FacturaAlumno01Controller(
        //    ObtenFacturaProceso obtenFacturaProceso
        //    ) {
        //    _obtenFacturaProceso = obtenFacturaProceso;
        //}

        //[HttpGet("obtenerProductos")]
        //public async Task<ActionResult<List<ProductoYServicioDTO>>> ObteneProductos()
        //{
        //    var productos = await _obtenFacturaProceso.ObtenerProductos();
        //    return productos;
        //}
    }
}
