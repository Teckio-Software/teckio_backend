using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;




namespace SistemaERP.API.Controllers.Alumno40
{
    [Route("api/cuentabancariacliente/40")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteAlumno40Controller : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<Alumno40Context> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<Alumno40Context> _proceso;

        public CuentaBancariaClienteAlumno40Controller(
            ICuentaBancariaClienteService<Alumno40Context> cuentaBancariaClienteService,
            CuentaBancariaProceso<Alumno40Context> proceso
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
