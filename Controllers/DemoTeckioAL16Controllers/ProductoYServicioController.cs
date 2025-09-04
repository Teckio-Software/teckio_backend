using AutoMapper.Configuration.Annotations;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Procesos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ERP_TECKIO.Controllers.DemoTeckioAL16
{
    [Route("api/productoyservicio/16")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductoYServicioDemoTeckioAL16Controller : ControllerBase
    {

        private readonly IProductoYservicioService<DemoTeckioAL16Context> _productoYServicioService;

        private readonly ProductoYServicioProceso<DemoTeckioAL16Context> _process;

        public ProductoYServicioDemoTeckioAL16Controller(IProductoYservicioService<DemoTeckioAL16Context> productoYServicioService,
            ProductoYServicioProceso<DemoTeckioAL16Context> process
            )
        {
            _productoYServicioService = productoYServicioService;
            _process = process;
        }

        [HttpGet("obtenerTodos")]
        public async Task<ActionResult<List<ProductoYservicioDTO>>> ObtenerTodos()
        {
            var lista = await _productoYServicioService.ObtenerTodos();
            return lista;
        }


        [HttpGet("obtenerConjunto")]
        public async Task<ActionResult<List<ProductoYServicioConjuntoDTO>>> ObtenerConjuntos()
        {
            var lista = await _process.ObtenerProductosYServicios();
            return lista;
        }

        [HttpPost("crearYObtener")]
        public async Task<ActionResult<ProductoYservicioDTO>> CrearYObtener(ProductoYservicioDTO productoyservicio)
        {
            var resultado = await _process.CrearYObtener(productoyservicio);
            return resultado;
        }

        [HttpPut("editar")]
        public async Task<ActionResult<RespuestaDTO>> Editar(ProductoYservicioDTO productoyservicio)
        {
            var resultado = await _process.Editar(productoyservicio);
            return resultado;
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<RespuestaDTO>> Eliminar(ProductoYservicioDTO productoyservicio)
        {
            var resultado = await _process.Eliminar(productoyservicio);
            return resultado;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<RespuestaDTO>> Crear(ProductoYservicioDTO productoyservicio)
        {
            var respuesta = await _process.Crear(productoyservicio);
            return respuesta;
        }



    }
}
