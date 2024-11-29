using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


using ERP_TECKIO.DTO;
using System.Diagnostics;

namespace ERP_TECKIO.API.Controllers.Alumno26
{
    [Route("api/cuentabancariaempresa/26")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno26Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno26Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno26Context> _proceso;

        public CuentaBancariaEmpresaAlumno26Controller(
            ICuentaBancariaEmpresaService<Alumno26Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno26Context> proceso
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
