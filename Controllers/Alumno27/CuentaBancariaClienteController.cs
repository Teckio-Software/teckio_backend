using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;




namespace ERP_TECKIO.API.Controllers.Alumno27
{
    [Route("api/cuentabancariacliente/27")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteAlumno27Controller : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<Alumno27Context> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<Alumno27Context> _proceso;

        public CuentaBancariaClienteAlumno27Controller(
            ICuentaBancariaClienteService<Alumno27Context> cuentaBancariaClienteService,
            CuentaBancariaProceso<Alumno27Context> proceso
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
