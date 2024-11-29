using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Controllers
{
    [Route("api/cuentabancariaempresa/6")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuentaBancariaEmpresaAlumno06Controller : ControllerBase
    {
        private readonly ICuentaBancariaEmpresaService<Alumno06Context> _cuentaBancariaEmpresaService;
        private readonly CuentaBancariaProceso<Alumno06Context> _proceso;

        public CuentaBancariaEmpresaAlumno06Controller(
            ICuentaBancariaEmpresaService<Alumno06Context> cuentaBancariaEmpresaService,
            CuentaBancariaProceso<Alumno06Context> proceso
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
