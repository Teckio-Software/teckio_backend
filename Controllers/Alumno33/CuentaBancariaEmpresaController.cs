using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using System.Diagnostics;

namespace ERP_TECKIO.API.Controllers.Alumno33
{
    [Route("api/cuentabancariaempresa/33")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno33Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno33Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno33Context> _proceso;

        public CuentaBancariaEmpresaAlumno33Controller(
            ICuentaBancariaEmpresaService<Alumno33Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno33Context> proceso
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
