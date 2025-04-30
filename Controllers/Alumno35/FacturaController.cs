using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Procesos.Facturacion;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.Alumno01
{
    [Route("api/factura/1042")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class FacturaAlumno35Controller : ControllerBase
    {
        private readonly ObtenFacturaProceso<Alumno35Context> _obtenFacturaProceso;
        private readonly ICategoriaProductoYServicioService<Alumno35Context> _categoriaProductoYServicioService;
        public FacturaAlumno35Controller(
            ObtenFacturaProceso<Alumno35Context> obtenFacturaProceso,
            ICategoriaProductoYServicioService<Alumno35Context> categoriaProductoYServicioService
            ) {
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

        [HttpGet("obtenerProductos")]
        public async Task<ActionResult<bool>> ObteneProductos()
        {
            var productos = await _obtenFacturaProceso.ObtenerProductos();
            return productos;
        }

        [HttpGet("leerFacturas")]
        public async Task<ActionResult<bool>> leerFacturas()
        {
            var productos = await _obtenFacturaProceso.leerFacturas();
            return productos;
        }
    }
}
