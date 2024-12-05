using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using System.Diagnostics;

namespace ERP_TECKIO.API.Controllers.Alumno14
{
    [Route("api/cuentabancariaempresa/14")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno14Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno14Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno14Context> _proceso;

        public CuentaBancariaEmpresaAlumno14Controller(
            ICuentaBancariaEmpresaService<Alumno14Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno14Context> proceso
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
