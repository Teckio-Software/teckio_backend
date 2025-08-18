using ERP_TECKIO.DTO;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Procesos.Facturacion;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.Alumno19
{
    /// <summary>
    /// Controlador de los Insumos por producto y servicio que hereda <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/insumoxproductoyservicio/19")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionInsumo-Empresa1")]
    public class InsumoXProductoYServicioController: ControllerBase
    {
        private readonly IInsumoXProductoYServicioService<Alumno19Context> _service;
        private readonly InsumoXProductoYServicioProceso<Alumno19Context> _process;

        public InsumoXProductoYServicioController(IInsumoXProductoYServicioService<Alumno19Context> service, InsumoXProductoYServicioProceso<Alumno19Context> process)
        {
            _service = service;
            _process = process;
        }

        [HttpGet("obtenerXIdProdYSer/{id:int}")]
        public async Task<ActionResult<List<InsumoXProductoYServicioDTO>>> ObtenerXIdProdYSer(int id)
        {
            var lista = await _service.ObtenerPorIdPrdYSer(id);
            return lista;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<RespuestaDTO>> Crear(InsumoXProductoYServicioDTO parametro)
        {
            var resultado = await _service.Crear(parametro);
            return resultado;
        }

        [HttpPut("editar")]
        public async Task<ActionResult<RespuestaDTO>> Editar(InsumoXProductoYServicioDTO parametro)
        {
            var resultado = await _service.Editar(parametro);
            return resultado;
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult<RespuestaDTO>> Eliminar(int id)
        {
            var resultado = await _service.Eliminar(id);
            return resultado;
        }

        [HttpGet("obtenerConjuntoXIdProdYSer/{id:int}")]
        public async Task<ActionResult<List<InsumoXProductoYServicioCompuestoDTO>>> ObtenerConjuntoXIdProdYSer(int id)
        {
            var lista = await _process.ObtenerLista(id);
            return lista;
        }
    }
}
