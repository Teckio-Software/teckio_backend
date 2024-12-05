using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;




namespace ERP_TECKIO.API.Controllers.Alumno26
{
    [Route("api/cuentabancariacliente/26")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteAlumno26Controller : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<Alumno26Context> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<Alumno26Context> _proceso;

        public CuentaBancariaClienteAlumno26Controller(
            ICuentaBancariaClienteService<Alumno26Context> cuentaBancariaClienteService,
            CuentaBancariaProceso<Alumno26Context> proceso
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
