using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using System.Diagnostics;

namespace SistemaERP.API.Controllers.Alumno44
{
    [Route("api/cuentabancariaempresa/44")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno44Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno44Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno44Context> _proceso;

        public CuentaBancariaEmpresaAlumno44Controller(
            ICuentaBancariaEmpresaService<Alumno44Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno44Context> proceso
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
