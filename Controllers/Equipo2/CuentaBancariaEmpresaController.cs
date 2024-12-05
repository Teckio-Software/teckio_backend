using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using System.Diagnostics;

namespace SistemaERP.API.Controllers.Alumno40
{
    [Route("api/cuentabancariaempresa/40")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno40Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno40Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno40Context> _proceso;

        public CuentaBancariaEmpresaAlumno40Controller(
            ICuentaBancariaEmpresaService<Alumno40Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno40Context> proceso
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
