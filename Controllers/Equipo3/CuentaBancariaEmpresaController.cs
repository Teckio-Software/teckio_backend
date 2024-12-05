using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using System.Diagnostics;

namespace SistemaERP.API.Controllers.Alumno41
{
    [Route("api/cuentabancariaempresa/41")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno41Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno41Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno41Context> _proceso;

        public CuentaBancariaEmpresaAlumno41Controller(
            ICuentaBancariaEmpresaService<Alumno41Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno41Context> proceso
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
