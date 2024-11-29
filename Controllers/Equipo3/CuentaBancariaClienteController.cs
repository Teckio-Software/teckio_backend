using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;




namespace SistemaERP.API.Controllers.Alumno41
{
    [Route("api/cuentabancariacliente/41")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteAlumno41Controller : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<Alumno41Context> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<Alumno41Context> _proceso;

        public CuentaBancariaClienteAlumno41Controller(
            ICuentaBancariaClienteService<Alumno41Context> cuentaBancariaClienteService,
            CuentaBancariaProceso<Alumno41Context> proceso
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
