using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using System.Diagnostics;

namespace SistemaERP.API.Controllers.Alumno43
{
    [Route("api/cuentabancariaempresa/43")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno43Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno43Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno43Context> _proceso;

        public CuentaBancariaEmpresaAlumno43Controller(
            ICuentaBancariaEmpresaService<Alumno43Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno43Context> proceso
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
