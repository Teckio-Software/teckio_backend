using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/polizadetalle/4")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa1")]
    public class PolizaDetalleDemoTeckioAL04Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<DemoTeckioAL04Context> _Service;
        private readonly ICuentaContableService<DemoTeckioAL04Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleDemoTeckioAL04Controller> _Logger;
        private readonly DemoTeckioAL04Context _Context;
        public PolizaDetalleDemoTeckioAL04Controller(
            ILogger<PolizaDetalleDemoTeckioAL04Controller> logger
            , DemoTeckioAL04Context context
            , IPolizaDetalleService<DemoTeckioAL04Context> service
            , ICuentaContableService<DemoTeckioAL04Context> cuentaContableService)
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
