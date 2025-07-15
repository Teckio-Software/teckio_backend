using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Procesos;
using ERP_TECKIO.Procesos.Facturacion;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using static ERP_TECKIO.Procesos.ObtenFacturaProceso;

namespace ERP_TECKIO.Controllers.Alumno02
{
    [Route("api/factura/2")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class FacturaAlumno02Controller : ControllerBase
    {
        private readonly ObtenFacturaProceso<Alumno02Context> _obtenFacturaProceso;
        private readonly ICategoriaProductoYServicioService<Alumno02Context> _categoriaProductoYServicioService;
        public FacturaAlumno02Controller(
            ObtenFacturaProceso<Alumno02Context> obtenFacturaProceso,
            ICategoriaProductoYServicioService<Alumno02Context> categoriaProductoYServicioService
            )
        {
            _obtenFacturaProceso = obtenFacturaProceso;
            _categoriaProductoYServicioService = categoriaProductoYServicioService;
        }
        [HttpGet("ObtenFacturas")]
        public async Task<ActionResult<List<FacturaDTO>>> ObtenerFacturas()
        {
            var facturas = await _obtenFacturaProceso.ObtenerFacturas();
            return facturas;
        }

        [HttpGet("ObtenFacturaDetalleXIdFactura/{IdFactura:int}")]
        public async Task<ActionResult<List<FacturaDetalleDTO>>> ObtenFacturaDetalleXIdFactura(int IdFactura)
        {
            var facturaDetalles = await _obtenFacturaProceso.ObtenFacturaDetalleXIdFactura(IdFactura);
            return facturaDetalles;
        }

        [HttpGet("ObtenComplementoPagoXIdFactura/{IdFactura:int}")]
        public async Task<ActionResult<List<FacturaComplementoPagoDTO>>> ObtenComplementoPagoXIdFactura(int IdFactura)
        {
            var complemntoPagos = await _obtenFacturaProceso.ObtenComplementoPagoXIdFactura(IdFactura);
            return complemntoPagos;
        }

        [HttpGet("ObtenerCategorias")]
        public async Task<ActionResult<List<CategoriaProductoYServicioDTO>>> ObtenerCategorias()
        {
            var categorias = await _categoriaProductoYServicioService.ObtenerTodos();
            return categorias;
        }
    }
}
