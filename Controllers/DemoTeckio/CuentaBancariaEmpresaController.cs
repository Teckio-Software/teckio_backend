using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/cuentabancariaempresa/3")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaDemoTeckioController : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<DemoTeckioContext> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<DemoTeckioContext> _proceso;

        public CuentaBancariaEmpresaDemoTeckioController(
            ICuentaBancariaEmpresaService<DemoTeckioContext> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<DemoTeckioContext> proceso
            ) {
            _cuentaBancariaEmpresaService = cuentaBancariaEmpresaService;
            _proceso = proceso;
        }

        [HttpPost]
        [Route("GuardarCuentaBancaria")]
        public async Task<ActionResult<bool>> GuardarCuentaBancaria([FromBody] CuentaBancariaEmpresasDTO cuentaBancaria)
        {
            cuentaBancaria.IdCuentaContable = null;
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
