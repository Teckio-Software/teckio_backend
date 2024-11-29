using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


using ERP_TECKIO.DTO;

namespace ERP_TECKIO.API.Controllers.Alumno32
{
    [Route("api/cuentabancariacliente/32")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteAlumno32Controller : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<Alumno32Context> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<Alumno32Context> _proceso;

        public CuentaBancariaClienteAlumno32Controller(
            ICuentaBancariaClienteService<Alumno32Context> cuentaBancariaClienteService,
            CuentaBancariaProceso<Alumno32Context> proceso
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
