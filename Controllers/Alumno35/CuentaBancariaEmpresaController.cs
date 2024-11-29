using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


using ERP_TECKIO.DTO;
using System.Diagnostics;

namespace ERP_TECKIO.API.Controllers.Alumno35
{
    [Route("api/cuentabancariaempresa/35")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno35Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno35Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno35Context> _proceso;

        public CuentaBancariaEmpresaAlumno35Controller(
            ICuentaBancariaEmpresaService<Alumno35Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno35Context> proceso
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
