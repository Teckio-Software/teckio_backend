using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using System.Diagnostics;

namespace SistemaERP.API.Controllers.Alumno44
{
    [Route("api/cuentabancariaempresa/45")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno45Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno38Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno38Context> _proceso;

        public CuentaBancariaEmpresaAlumno45Controller(
            ICuentaBancariaEmpresaService<Alumno38Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno38Context> proceso
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
