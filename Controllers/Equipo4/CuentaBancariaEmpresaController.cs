using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using System.Diagnostics;

namespace SistemaERP.API.Controllers.Alumno42
{
    [Route("api/cuentabancariaempresa/42")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno42Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno42Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno42Context> _proceso;

        public CuentaBancariaEmpresaAlumno42Controller(
            ICuentaBancariaEmpresaService<Alumno42Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno42Context> proceso
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
