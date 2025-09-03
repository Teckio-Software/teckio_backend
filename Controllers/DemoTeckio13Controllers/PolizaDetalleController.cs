using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    [Route("api/polizadetalle/13")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa1")]
    public class PolizaDetalleDemoTeckioAL13Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<DemoTeckioAL13Context> _Service;
        private readonly ICuentaContableService<DemoTeckioAL13Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleDemoTeckioAL13Controller> _Logger;
        private readonly DemoTeckioAL13Context _Context;
        public PolizaDetalleDemoTeckioAL13Controller(
            ILogger<PolizaDetalleDemoTeckioAL13Controller> logger
            , DemoTeckioAL13Context context
            , IPolizaDetalleService<DemoTeckioAL13Context> service
            , ICuentaContableService<DemoTeckioAL13Context> cuentaContableService)
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
