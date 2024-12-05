using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;


using ERP_TECKIO;


using ERP_TECKIO;





namespace SistemaERP.API.Alumno41Controllers.Procomi
{
    [Route("api/polizadetalle/41")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa35")]
    public class PolizaDetalleAlumno41Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno41Context> _Service;
        private readonly ICuentaContableService<Alumno41Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno41Controller> _Logger;
        private readonly Alumno41Context _Context;
        public PolizaDetalleAlumno41Controller(
            ILogger<PolizaDetalleAlumno41Controller> logger
            , Alumno41Context context
            , IPolizaDetalleService<Alumno41Context> service
            , ICuentaContableService<Alumno41Context> cuentaContableService)
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
