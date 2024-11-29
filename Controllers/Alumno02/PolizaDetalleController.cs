using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/polizadetalle/2")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa2")]
    public class PolizaDetalleAlumno02Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno02Context> _Service;
        private readonly ICuentaContableService<Alumno02Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno02Controller> _Logger;
        private readonly Alumno02Context _Context;
        public PolizaDetalleAlumno02Controller(
            ILogger<PolizaDetalleAlumno02Controller> logger
            , Alumno02Context context
            , IPolizaDetalleService<Alumno02Context> service
            , ICuentaContableService<Alumno02Context> cuentaContableService)
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
