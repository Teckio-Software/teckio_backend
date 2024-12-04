using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Controllers
{
    [Route("api/cuentabancariaempresa/7")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno07Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno07Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno07Context> _proceso;

        public CuentaBancariaEmpresaAlumno07Controller(
            ICuentaBancariaEmpresaService<Alumno07Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno07Context> proceso
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
