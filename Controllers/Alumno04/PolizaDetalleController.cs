using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;

namespace ERP_TECKIO.Controllers
{
    [Route("api/polizadetalle/4")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa4")]
    public class PolizaDetalleAlumno04Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno04Context> _Service;
        private readonly ICuentaContableService<Alumno04Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno04Controller> _Logger;
        private readonly Alumno04Context _Context;
        public PolizaDetalleAlumno04Controller(
            ILogger<PolizaDetalleAlumno04Controller> logger
            , Alumno04Context context
            , IPolizaDetalleService<Alumno04Context> service
            , ICuentaContableService<Alumno04Context> cuentaContableService)
        {
            _Service = service;
            _Logger = logger;
            _Context = context;
            _CuentaContableService = cuentaContableService;
        }


        [HttpGet("{IdPoliza:int}")]
        public async Task<List<PolizaDetalleDTO>> Get(int IdPoliza)
        {
            try
            {
                var resultado = await _Service.ObtenTodosXIdPoliza(IdPoliza);
                if (resultado.Count > 0)
                {
                    for (int i = 0; i < resultado.Count; i++)
                    {
                        var cuentaContable = await _CuentaContableService.ObtenXId(resultado[i].IdCuentaContable);
                        resultado[i].CuentaContableCodigo = cuentaContable.Codigo;
                    }
                    return resultado;
                }
                else
                {
                    return new List<PolizaDetalleDTO>();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return new List<PolizaDetalleDTO>();
        }
    }
}
