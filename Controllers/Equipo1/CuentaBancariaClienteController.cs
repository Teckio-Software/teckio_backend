using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;




namespace SistemaERP.API.Controllers.Alumno39
{
    [Route("api/cuentabancariacliente/39")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteAlumno39Controller : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<Alumno39Context> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<Alumno39Context> _proceso;

        public CuentaBancariaClienteAlumno39Controller(
            ICuentaBancariaClienteService<Alumno39Context> cuentaBancariaClienteService,
            CuentaBancariaProceso<Alumno39Context> proceso
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
