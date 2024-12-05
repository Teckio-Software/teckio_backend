using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using System.Diagnostics;

namespace ERP_TECKIO.API.Controllers.Alumno22
{
    [Route("api/cuentabancariaempresa/22")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno22Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno22Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno22Context> _proceso;

        public CuentaBancariaEmpresaAlumno22Controller(
            ICuentaBancariaEmpresaService<Alumno22Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno22Context> proceso
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
