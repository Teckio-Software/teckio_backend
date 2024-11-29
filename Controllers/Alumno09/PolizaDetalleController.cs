using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP_TECKIO
{
    [Route("api/polizadetalle/9")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa9")]
    public class PolizaDetalleAlumno09Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno09Context> _Service;
        private readonly ICuentaContableService<Alumno09Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno09Controller> _Logger;
        private readonly Alumno09Context _Context;
        public PolizaDetalleAlumno09Controller(
            ILogger<PolizaDetalleAlumno09Controller> logger
            , Alumno09Context context
            , IPolizaDetalleService<Alumno09Context> service
            , ICuentaContableService<Alumno09Context> cuentaContableService)
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
