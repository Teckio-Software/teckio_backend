using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;




namespace ERP_TECKIO.API.Controllers.Alumno37
{
    [Route("api/cuentabancariacliente/37")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteAlumno37Controller : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<Alumno37Context> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<Alumno37Context> _proceso;

        public CuentaBancariaClienteAlumno37Controller(
            ICuentaBancariaClienteService<Alumno37Context> cuentaBancariaClienteService,
            CuentaBancariaProceso<Alumno37Context> proceso
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
