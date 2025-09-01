using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/polizadetalle/12")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa1")]
    public class PolizaDetalleAlumno12Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno12Context> _Service;
        private readonly ICuentaContableService<Alumno12Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno12Controller> _Logger;
        private readonly Alumno12Context _Context;
        public PolizaDetalleAlumno12Controller(
            ILogger<PolizaDetalleAlumno12Controller> logger
            , Alumno12Context context
            , IPolizaDetalleService<Alumno12Context> service
            , ICuentaContableService<Alumno12Context> cuentaContableService)
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
