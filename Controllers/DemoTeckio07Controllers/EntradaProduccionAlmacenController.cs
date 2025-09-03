using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers
{
    [Route("api/entradaproduccionalmacen/7")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EntradaProduccionAlmacenDemoTeckioAL07Controller : ControllerBase
    {
        private readonly EntradaProduccionAlmacenProceso<DemoTeckioAL07Context> _proceso;

        public EntradaProduccionAlmacenDemoTeckioAL07Controller(EntradaProduccionAlmacenProceso<DemoTeckioAL07Context> proceso)
        {
            _proceso = proceso;
        }

        [HttpPost("crear")]
        public async Task<RespuestaDTO> Crear(EntradaProduccionAlmacenDTO parametro)
        {
            var resultado = await _proceso.Crear(parametro);
            return resultado;
        }

        [HttpPost("crearYObtener")]
        public async Task<EntradaProduccionAlmacenDTO> CrearYObtener(EntradaProduccionAlmacenDTO parametro)
        {
            var resultado = await _proceso.CrearYObtener(parametro);
            return resultado;
        }

        [HttpPut("editar")]
        public async Task<RespuestaDTO> Editar(EntradaProduccionAlmacenDTO parametro)
        {
            var resultado = await _proceso.Editar(parametro);
            return resultado;
        }

        [HttpDelete("eliminar")]
        public async Task<RespuestaDTO> Eliminar(int parametro)
        {
            var resultado = await _proceso.Eliminar(parametro);
            return resultado;
        }
    }
}
