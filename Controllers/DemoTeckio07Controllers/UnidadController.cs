using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers.DemoTeckioAL07
{
    [Route("api/unidad/7")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UnidadController: ControllerBase
    {
        private readonly IUnidadService<DemoTeckioAL07Context> _service;

        public UnidadController(IUnidadService<DemoTeckioAL07Context> service)
        {
            _service = service;
        }

        [HttpGet("obtenerTodos")]
        public async Task<ActionResult<List<UnidadDTO>>> ObtenerTodos()
        {
            var lista = await _service.ObtenerTodos();
            return lista;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<RespuestaDTO>> Crear(UnidadDTO unidad)
        {
            var resultado = await _service.Crear(unidad);
            if (resultado)
            {
                return new RespuestaDTO
                {
                    Estatus = true,
                    Descripcion = "Unidad creada exitosamente"
                };
            }
            else
            {
                return new RespuestaDTO
                {
                    Estatus = false,
                    Descripcion = "Ocurrio un error al intentar crear la unidad"
                };
            }
        }
    }
}
