using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/cuentabancariacliente/1")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteIyATolucaController : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<IyATolucaContext> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<IyATolucaContext> _proceso;

        public CuentaBancariaClienteIyATolucaController(
            ICuentaBancariaClienteService<IyATolucaContext> cuentaBancariaClienteService,
            CuentaBancariaProceso<IyATolucaContext> proceso
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
