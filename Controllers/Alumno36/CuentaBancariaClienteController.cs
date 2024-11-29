using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


using ERP_TECKIO.DTO;

namespace ERP_TECKIO.API.Controllers.Alumno36
{
    [Route("api/cuentabancariacliente/36")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaClienteAlumno36Controller : ControllerBase
    {
        private readonly ICuentaBancariaClienteService<Alumno36Context> _cuentaBancariaClienteService;
        private readonly CuentaBancariaProceso<Alumno36Context> _proceso;

        public CuentaBancariaClienteAlumno36Controller(
            ICuentaBancariaClienteService<Alumno36Context> cuentaBancariaClienteService,
            CuentaBancariaProceso<Alumno36Context> proceso
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
