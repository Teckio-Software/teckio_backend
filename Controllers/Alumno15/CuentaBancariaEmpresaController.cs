using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


using ERP_TECKIO.DTO;
using System.Diagnostics;

namespace ERP_TECKIO.API.Controllers.Alumno15
{
    [Route("api/cuentabancariaempresa/15")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno15Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno15Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno15Context> _proceso;

        public CuentaBancariaEmpresaAlumno15Controller(
            ICuentaBancariaEmpresaService<Alumno15Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno15Context> proceso
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
