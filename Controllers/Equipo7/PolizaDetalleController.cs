using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;


using ERP_TECKIO;


using ERP_TECKIO;





namespace SistemaERP.API.Alumno45Controllers.Procomi
{
    [Route("api/polizadetalle/45")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa35")]
    public class PolizaDetalleAlumno45Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno38Context> _Service;
        private readonly ICuentaContableService<Alumno38Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno45Controller> _Logger;
        private readonly Alumno38Context _Context;
        public PolizaDetalleAlumno45Controller(
            ILogger<PolizaDetalleAlumno45Controller> logger
            , Alumno38Context context
            , IPolizaDetalleService<Alumno38Context> service
            , ICuentaContableService<Alumno38Context> cuentaContableService)
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
