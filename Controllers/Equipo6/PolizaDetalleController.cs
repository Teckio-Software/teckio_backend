using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;


using ERP_TECKIO;


using ERP_TECKIO;





namespace SistemaERP.API.Alumno44Controllers.Procomi
{
    [Route("api/polizadetalle/44")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa35")]
    public class PolizaDetalleAlumno44Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno44Context> _Service;
        private readonly ICuentaContableService<Alumno44Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno44Controller> _Logger;
        private readonly Alumno44Context _Context;
        public PolizaDetalleAlumno44Controller(
            ILogger<PolizaDetalleAlumno44Controller> logger
            , Alumno44Context context
            , IPolizaDetalleService<Alumno44Context> service
            , ICuentaContableService<Alumno44Context> cuentaContableService)
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
