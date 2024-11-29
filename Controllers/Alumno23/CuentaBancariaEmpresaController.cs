using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


using ERP_TECKIO.DTO;
using System.Diagnostics;

namespace ERP_TECKIO.API.Controllers.Alumno23
{
    [Route("api/cuentabancariaempresa/23")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno23Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno23Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno23Context> _proceso;

        public CuentaBancariaEmpresaAlumno23Controller(
            ICuentaBancariaEmpresaService<Alumno23Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno23Context> proceso
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
