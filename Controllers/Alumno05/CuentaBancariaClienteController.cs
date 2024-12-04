using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Controllers
{
    [Route("api/cuentabancariacliente/5")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteAlumno05Controller : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<Alumno05Context> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<Alumno05Context> _proceso;

        public CuentaBancariaClienteAlumno05Controller(
            ICuentaBancariaClienteService<Alumno05Context> cuentaBancariaClienteService,
            CuentaBancariaProceso<Alumno05Context> proceso
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
