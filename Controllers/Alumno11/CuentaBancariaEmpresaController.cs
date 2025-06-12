using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/cuentabancariaempresa/11")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno11Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno11Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno11Context> _proceso;

        public CuentaBancariaEmpresaAlumno11Controller(
            ICuentaBancariaEmpresaService<Alumno11Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno11Context> proceso
            ) {
            _cuentaBancariaEmpresaService = cuentaBancariaEmpresaService;
            _proceso = proceso;
        }

        [HttpPost]
        [Route("GuardarCuentaBancaria")]
        public async Task<ActionResult<bool>> GuardarCuentaBancaria([FromBody] CuentaBancariaEmpresasDTO cuentaBancaria)
        {
            return await _cuentaBancariaEmpresaService.Crear(cuentaBancaria);
        }

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult<List<CuentaBancariaEmpresasDTO>>> ObtenerTodos()
        {
            return await _proceso.ObtenerXEmpresa();
        }

        [HttpPost("AsignarCuentaContable")]
        public async Task<ActionResult<RespuestaDTO>> AsignarCuentaContable(CuentaBancariaEmpresasDTO cuentaBancariaEmpresa) {
            var respuesta =  await _proceso.AsignarCuentaContable(cuentaBancariaEmpresa);
            return respuesta;
        }
    }
}
