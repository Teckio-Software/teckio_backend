using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;




namespace SistemaERP.API.Controllers.Alumno43
{
    [Route("api/cuentabancariacliente/43")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteAlumno43Controller : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<Alumno43Context> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<Alumno43Context> _proceso;

        public CuentaBancariaClienteAlumno43Controller(
            ICuentaBancariaClienteService<Alumno43Context> cuentaBancariaClienteService,
            CuentaBancariaProceso<Alumno43Context> proceso
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
