using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;




namespace ERP_TECKIO.API.Controllers.Alumno10
{
    [Route("api/cuentabancariacliente/10")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteAlumno10Controller : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<Alumno10Context> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<Alumno10Context> _proceso;

        public CuentaBancariaClienteAlumno10Controller(
            ICuentaBancariaClienteService<Alumno10Context> cuentaBancariaClienteService,
            CuentaBancariaProceso<Alumno10Context> proceso
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
