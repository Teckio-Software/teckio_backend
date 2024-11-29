using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


using ERP_TECKIO.DTO;

namespace ERP_TECKIO.API.Controllers.Alumno17
{
    [Route("api/cuentabancariacliente/17")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteAlumno17Controller : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<Alumno17Context> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<Alumno17Context> _proceso;

        public CuentaBancariaClienteAlumno17Controller(
            ICuentaBancariaClienteService<Alumno17Context> cuentaBancariaClienteService,
            CuentaBancariaProceso<Alumno17Context> proceso
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
