using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using System.Diagnostics;

namespace ERP_TECKIO.API.Controllers.Alumno29
{
    [Route("api/cuentabancariaempresa/29")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno29Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno29Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno29Context> _proceso;

        public CuentaBancariaEmpresaAlumno29Controller(
            ICuentaBancariaEmpresaService<Alumno29Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno29Context> proceso
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
