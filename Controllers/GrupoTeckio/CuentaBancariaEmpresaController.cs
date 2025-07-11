using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/cuentabancariaempresa/2")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaGrupoTeckioController : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<GrupoTeckioContext> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<GrupoTeckioContext> _proceso;

        public CuentaBancariaEmpresaGrupoTeckioController(
            ICuentaBancariaEmpresaService<GrupoTeckioContext> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<GrupoTeckioContext> proceso
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
