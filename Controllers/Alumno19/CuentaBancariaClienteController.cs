using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;




namespace ERP_TECKIO.API.Controllers.Alumno19
{
    [Route("api/cuentabancariacliente/19")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteAlumno19Controller : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<Alumno19Context> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<Alumno19Context> _proceso;

        public CuentaBancariaClienteAlumno19Controller(
            ICuentaBancariaClienteService<Alumno19Context> cuentaBancariaClienteService,
            CuentaBancariaProceso<Alumno19Context> proceso
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
