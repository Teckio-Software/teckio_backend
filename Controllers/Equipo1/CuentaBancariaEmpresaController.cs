using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using System.Diagnostics;

namespace SistemaERP.API.Controllers.Alumno39
{
    [Route("api/cuentabancariaempresa/39")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno39Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno39Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno39Context> _proceso;

        public CuentaBancariaEmpresaAlumno39Controller(
            ICuentaBancariaEmpresaService<Alumno39Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno39Context> proceso
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
