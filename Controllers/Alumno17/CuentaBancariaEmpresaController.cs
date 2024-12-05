using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using System.Diagnostics;

namespace ERP_TECKIO.API.Controllers.Alumno17
{
    [Route("api/cuentabancariaempresa/17")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno17Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno17Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno17Context> _proceso;

        public CuentaBancariaEmpresaAlumno17Controller(
            ICuentaBancariaEmpresaService<Alumno17Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno17Context> proceso
            ) {
            _cuentaBancariaEmpresaService = cuentaBancariaEmpresaService;
            _proceso = proceso;
        }

        [HttpPost]
        [Route("GuardarCuentaBancaria")]
        public async Task<ActionResult<bool>> GuardarCuentaBancaria([FromBody] CuentaBancariaEmpresasDTO cuentaBancaria)
        {
            return await _cuentaBancariaEmpresaService.Crear(cuentaBancaria);
        }

        [HttpGet("ObtenerTodos")]
        public async Task<ActionResult<List<CuentaBancariaEmpresasDTO>>> ObtenerTodos()
        {
            return await _proceso.ObtenerXEmpresa();
        }
    }
}
