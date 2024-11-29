using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;











namespace ERP_TECKIO
{
    [Route("api/polizadetalle/37")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionPoliza-Empresa35")]
    public class PolizaDetalleAlumno37Controller : ControllerBase
    {
        private readonly IPolizaDetalleService<Alumno37Context> _Service;
        private readonly ICuentaContableService<Alumno37Context> _CuentaContableService;
        private readonly ILogger<PolizaDetalleAlumno37Controller> _Logger;
        private readonly Alumno37Context _Context;
        public PolizaDetalleAlumno37Controller(
            ILogger<PolizaDetalleAlumno37Controller> logger
            , Alumno37Context context
            , IPolizaDetalleService<Alumno37Context> service
            , ICuentaContableService<Alumno37Context> cuentaContableService)
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
