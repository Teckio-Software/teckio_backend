using ERP_TECKIO.DTO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO.Controllers
{
    [Route("api/entradaproduccionalmacen/8")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EntradaProduccionAlmacenAlumno08Controller : ControllerBase
    {
        private readonly EntradaProduccionAlmacenProceso<Alumno08Context> _proceso;

        public EntradaProduccionAlmacenAlumno08Controller(EntradaProduccionAlmacenProceso<Alumno08Context> proceso)
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
