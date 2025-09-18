using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/cuentabancariacliente/26")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteDemoTeckioAL26Controller : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<DemoTeckioAL26Context> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<DemoTeckioAL26Context> _proceso;

        public CuentaBancariaClienteDemoTeckioAL26Controller(
            ICuentaBancariaClienteService<DemoTeckioAL26Context> cuentaBancariaClienteService,
            CuentaBancariaProceso<DemoTeckioAL26Context> proceso
            )
        {
            _cuentaBancariaClienteService = cuentaBancariaClienteService;
            _proceso = proceso;
        }

        [HttpPost]
        [Route("GuardarCuentaBancaria")]
        public async Task<ActionResult<bool>> GuardarCuentaBancaria([FromBody] CuentaBancariaClienteDTO cuentaBancaria)
        {
            return await _cuentaBancariaClienteService.Crear(cuentaBancaria);
        }

        [HttpGet("ObtenerXIdCliente/{IdCliente:int}")]
        public async Task<ActionResult<List<CuentaBancariaClienteDTO>>> ObtenerTodos(int IdCliente)
        {
            return await _proceso.ObtenerXidCliente(IdCliente);
        }

    }
}
