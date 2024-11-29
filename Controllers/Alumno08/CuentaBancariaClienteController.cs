using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


using ERP_TECKIO.DTO;

namespace ERP_TECKIO.API.Controllers.Alumno08
{
    [Route("api/cuentabancariacliente/8")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteAlumno08Controller : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<Alumno08Context> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<Alumno08Context> _proceso;

        public CuentaBancariaClienteAlumno08Controller(
            ICuentaBancariaClienteService<Alumno08Context> cuentaBancariaClienteService,
            CuentaBancariaProceso<Alumno08Context> proceso
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
