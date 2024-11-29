using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


using ERP_TECKIO.DTO;
using System.Diagnostics;

namespace ERP_TECKIO.API.Controllers.Alumno10
{
    [Route("api/cuentabancariaempresa/10")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno10Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno10Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno10Context> _proceso;

        public CuentaBancariaEmpresaAlumno10Controller(
            ICuentaBancariaEmpresaService<Alumno10Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno10Context> proceso
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
