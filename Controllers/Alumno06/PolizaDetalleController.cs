using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/polizadetalle/6")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa1")]
    public class PolizaDetalleAlumno06Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno06Context> _Service;
        private readonly ICuentaContableService<Alumno06Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno06Controller> _Logger;
        private readonly Alumno06Context _Context;
        public PolizaDetalleAlumno06Controller(
            ILogger<PolizaDetalleAlumno06Controller> logger
            , Alumno06Context context
            , IPolizaDetalleService<Alumno06Context> service
            , ICuentaContableService<Alumno06Context> cuentaContableService)
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
