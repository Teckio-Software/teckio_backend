using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Controllers
{
    [Route("api/polizadetalle/14")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa14")]
    public class PolizaDetalleAlumno14Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno14Context> _Service;
        private readonly ICuentaContableService<Alumno14Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno14Controller> _Logger;
        private readonly Alumno14Context _Context;
        public PolizaDetalleAlumno14Controller(
            ILogger<PolizaDetalleAlumno14Controller> logger
            , Alumno14Context context
            , IPolizaDetalleService<Alumno14Context> service
            , ICuentaContableService<Alumno14Context> cuentaContableService)
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
