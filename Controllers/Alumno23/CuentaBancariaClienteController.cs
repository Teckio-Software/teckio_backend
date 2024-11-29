using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


using ERP_TECKIO.DTO;

namespace ERP_TECKIO.API.Controllers.Alumno23
{
    [Route("api/cuentabancariacliente/23")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteAlumno23Controller : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<Alumno23Context> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<Alumno23Context> _proceso;

        public CuentaBancariaClienteAlumno23Controller(
            ICuentaBancariaClienteService<Alumno23Context> cuentaBancariaClienteService,
            CuentaBancariaProceso<Alumno23Context> proceso
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
