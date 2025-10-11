using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Procesos;
using ERP_TECKIO.Procesos.Facturacion;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using static ERP_TECKIO.Procesos.ObtenFacturaProceso;

namespace ERP_TECKIO.Controllers.IyAToluca
{
    [Route("api/factura/1")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class FacturaIyATolucaController : ControllerBase
    {
        private readonly ObtenFacturaProceso<IyATolucaContext> _obtenFacturaProceso;
        private readonly ICategoriaProductoYServicioService<IyATolucaContext> _categoriaProductoYServicioService;
        public FacturaIyATolucaController(
            ObtenFacturaProceso<IyATolucaContext> obtenFacturaProceso,
            ICategoriaProductoYServicioService<IyATolucaContext> categoriaProductoYServicioService
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

        [HttpPost("CargarFactura")]
        public async Task<ActionResult<RespuestaDTO>> cargarFacturas(List<IFormFile> files) {
            var respuesta = await _obtenFacturaProceso.cargarFacturas(files);
            return respuesta;
        }
    }
}
