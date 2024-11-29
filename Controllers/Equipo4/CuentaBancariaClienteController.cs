using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;




namespace SistemaERP.API.Controllers.Alumno42
{
    [Route("api/cuentabancariacliente/42")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteAlumno42Controller : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<Alumno42Context> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<Alumno42Context> _proceso;

        public CuentaBancariaClienteAlumno42Controller(
            ICuentaBancariaClienteService<Alumno42Context> cuentaBancariaClienteService,
            CuentaBancariaProceso<Alumno42Context> proceso
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
